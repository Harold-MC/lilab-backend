namespace Lilab.Data.Repository
{
    public class LilabRepository<TEntity>: BaseRepository<TEntity, LilabContext> where TEntity : class, new()
    {
        public LilabRepository(LilabContext context) : base(context)
        {
        }
    }
}