using Lilab.Data.Entity;

namespace Lilab.Data.ViewModel;

public record LoginResponseViewModel(User User, string Token);