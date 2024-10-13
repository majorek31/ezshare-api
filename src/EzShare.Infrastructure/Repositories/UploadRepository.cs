using EzShare.Application.Contracts.Repositories;
using EzShare.Domain.Entities;
using EzShare.Infrastructure.Database;

namespace EzShare.Infrastructure.Repositories;

public class UploadRepository(AppDbContext context) : GenericRepository<Upload>(context), IUploadRepository
{
    private readonly AppDbContext _context = context;
}