﻿using Microsoft.EntityFrameworkCore;
using Vini.ModularMonolith.Example.Users.Domain;
using Vini.ModularMonolith.Example.Users.Interfaces;

namespace Vini.ModularMonolith.Example.Users.Infrastructure.Data;

internal class EFApplicationUserRepository : IApplicationUserRepository
{
  private readonly UsersDbContext _dbContext;

  public EFApplicationUserRepository(UsersDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
  {
    var user = await _dbContext.ApplicationUsers
      .SingleAsync(u => u.Id == userId.ToString());

    return user;
  }

  public async Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
  {
    var user = await _dbContext.ApplicationUsers
      .Include(u => u.Addresses)
      .SingleAsync(u => u.Email == email);

    return user;
  }

  public async Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
  {
    var user = await _dbContext.ApplicationUsers
      .Include(u => u.CartItems)
      .SingleAsync(u => u.Email == email);

    return user;
  }

  public Task SaveChangesAsync()
  {
    return _dbContext.SaveChangesAsync();
  }
}
