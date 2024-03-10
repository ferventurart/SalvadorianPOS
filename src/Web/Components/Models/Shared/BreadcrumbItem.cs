namespace Web.Components.Models.Shared;

public record BreadcrumbItem
{
    public string Text { get; set; }
    public string Href { get; set; }
    public bool IsCurrentPage { get; set; }
}
