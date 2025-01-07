namespace Lilab.Data.ViewModel;

public class CustomerParamsViewModel
{
    public string? SearchText { get; set; }
    public long? RoleId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}