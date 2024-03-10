using System.Globalization;
using System.Linq.Expressions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.StoreDepartments;
using SharedKernel;

namespace Application.StoreDepartments.GetStoreDepartments;

internal sealed class GetStoreDepartmentsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetStoreDepartmentsQuery, PagedList<StoreDepartmentResponse>>
{
    public async Task<Result<PagedList<StoreDepartmentResponse>>> Handle(
        GetStoreDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<StoreDepartment> storeDepartmentsQuery = context.StoreDepartments;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            storeDepartmentsQuery = storeDepartmentsQuery
                .Where(p => p.Name.Contains(request.SearchTerm));
        }

        storeDepartmentsQuery = request.SortOrder?.ToLower(CultureInfo.DefaultThreadCurrentCulture) == "desc"
            ? storeDepartmentsQuery.OrderByDescending(GetSortProperty(request))
            : storeDepartmentsQuery.OrderBy(GetSortProperty(request));
        
        IQueryable<StoreDepartmentResponse> storeDepartmentResponsesQuery = storeDepartmentsQuery
            .Select(s => new StoreDepartmentResponse(
                s.Id,
                s.Name,
                s.Active));

        Result<PagedList<StoreDepartmentResponse>> storeDepartments = await PagedList<StoreDepartmentResponse>.CreateAsync(
            storeDepartmentResponsesQuery,
            request.Page,
            request.PageSize);

        return storeDepartments;
    }

    private static Expression<Func<StoreDepartment, object>> GetSortProperty(GetStoreDepartmentsQuery request)
    {
        return request.SortColumn?.ToLower(CultureInfo.CurrentCulture) switch
        {
            _ => product => product.Name
        };
    }
}
