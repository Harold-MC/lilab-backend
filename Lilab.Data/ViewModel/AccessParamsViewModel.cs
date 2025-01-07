using Lilab.Data.Entity;

namespace Lilab.Data.ViewModel;

public class AccessParamsViewModel
{
    public DateTime? Since { get; set; }
    public DateTime? Until { get; set; }
    public Guid? CustomerId { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
}