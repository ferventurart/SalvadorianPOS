using SharedKernel;

namespace Web.Api.Features.StoreDepartments;

public sealed class StoreDepartment : Entity
{
    private StoreDepartment(
        Guid id, 
        string name,
        bool active) : base(id)
    {
        Name = name;
        Active = active;
    }

    public string Name { get; private set; }
    public bool Active { get; private set; }

    public static StoreDepartment Create(
        string name)
    {
        return new StoreDepartment(
            Guid.NewGuid(),
            name,
            true);
    }

    public static StoreDepartment Update(
        Guid id,
        string name,
        bool active)
    {
        return new StoreDepartment(
            id,
            name,
            active);
    }

}
