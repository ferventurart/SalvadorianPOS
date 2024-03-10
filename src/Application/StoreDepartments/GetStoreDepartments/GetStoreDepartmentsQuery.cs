using Application.Abstractions.Messaging;


namespace Application.StoreDepartments.GetStoreDepartments;

public record GetStoreDepartmentsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<StoreDepartmentResponse>>;
