using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.StoreDepartments;
using SharedKernel;

namespace Application.StoreDepartments.CreateStoreDepartment;

internal sealed class CreateStoreCommandHandler(
    IStoreDepartmentRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateStoreDepartmentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateStoreDepartmentCommand request, CancellationToken cancellationToken)
    {
        var storeDepartment = StoreDepartment.Create(request.Name);

        repository.Add(storeDepartment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return storeDepartment.Id;
    }
}
