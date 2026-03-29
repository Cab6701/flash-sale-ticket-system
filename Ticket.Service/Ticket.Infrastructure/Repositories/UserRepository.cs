using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entities;
using Ticket.Infrastructure.Persistence;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace Ticket.Infrastructure.Repositories;

public class UserRepository
{
    private readonly TicketDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserRepository(TicketDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task RegisterAsync(User user)
    {
        var exists = await _context.Users.AnyAsync(u => u.Username == user.Username);
        if (exists)
            throw new InvalidOperationException("Username already exists.");

        user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> LoginAsync(string userName, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName);
        if (user is null) return false;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        switch (result)
        {
            case PasswordVerificationResult.Success:
                return true;

            case PasswordVerificationResult.SuccessRehashNeeded:
                user.PasswordHash = _passwordHasher.HashPassword(user, password);
                await _context.SaveChangesAsync();
                return true;

            case PasswordVerificationResult.Failed:
            default:
                return false;
        }    
    }
}