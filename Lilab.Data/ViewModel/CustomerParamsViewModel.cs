namespace Lilab.Data.ViewModel;

public abstract record CustomerParamsViewModel(string SearchText, long? RoleId, int Page, int PageSize);