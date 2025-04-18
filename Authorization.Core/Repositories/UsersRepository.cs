using Authorization.Core.DataAccess;
using Authorization.Core.DataAccess.Entities;
using Authorization.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AuthorizationDatabaseContext _context;

    public UsersRepository(AuthorizationDatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<UserEntity> Create(UserEntity user, CancellationToken cancellationToken)
    {
        var created = await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return created.Entity;
    }

    public async Task<UserEntity?> FindById(long id, CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<UserEntity> Update(long id, UserEntity user, CancellationToken cancellationToken)
    {
        var candidate = await FindById(id, cancellationToken);
        if(candidate is null) return null;
        _context.Users.Attach(candidate);
        if(!string.IsNullOrWhiteSpace(candidate.Password)) candidate.Password = user.Password;
        if(!string.IsNullOrWhiteSpace(candidate.Email)) candidate.Email = user.Email;
        if(!string.IsNullOrWhiteSpace(candidate.FirstName)) candidate.FirstName = user.FirstName;
        if(!string.IsNullOrWhiteSpace(candidate.LastName)) candidate.LastName = user.LastName;
        await _context.SaveChangesAsync(cancellationToken);
        return candidate;
    }

    public async Task<long> Delete(long id, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}