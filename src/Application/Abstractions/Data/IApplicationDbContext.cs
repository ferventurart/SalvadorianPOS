using Domain.StoreDepartments;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<StoreDepartment> StoreDepartments { get; set; }
}
