using Application.Abstractions.Messaging;

namespace Application.StoreDepartments.CreateStoreDepartment;

public record CreateStoreDepartmentCommand(string Name) : ICommand<Guid>;
