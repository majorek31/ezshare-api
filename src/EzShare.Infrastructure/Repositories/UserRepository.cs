﻿using EzShare.Application.Contracts.Repositories;
using EzShare.Domain.Entities;
using EzShare.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EzShare.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;

    public new async Task<User?> GetByIdAsync(int id)
    {
        return await _context
            .Users
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public new async Task<List<User>> GetAllAsync()
    {
        return await _context
            .Users
            .Include(x => x.Role)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context
            .Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var user = await GetByEmailAsync(email);
        if (user is null) return null;
        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return await GetByEmailAsync(email) is null;
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context
            .Users
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}