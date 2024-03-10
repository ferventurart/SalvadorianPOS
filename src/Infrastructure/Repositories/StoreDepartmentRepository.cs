using Domain.StoreDepartments;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public sealed class StoreDepartmentRepository(ApplicationWriteDbContext context) : IStoreDepartmentRepository
{
    public void Add(StoreDepartment storeDepartment)
    {
        context.StoreDepartments.Add(storeDepartment);
    }
}
