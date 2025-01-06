namespace Lilab.Data.ViewModel;

public class UserParamsViewModel
{
    public string? SearchText { get; set; }
    public bool? IsActive { get; set; }
    public long? RoleId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}