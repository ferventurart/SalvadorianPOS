using SharedKernel;

namespace Domain.StoreDepartments;

public interface IStoreDepartmentRepository
{
    void Add(StoreDepartment storeDepartment);
}
