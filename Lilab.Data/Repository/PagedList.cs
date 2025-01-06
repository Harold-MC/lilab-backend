using Lilab.Service.Contract;

namespace Lilab.Data.Repository
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList()
        {
        }
        
        public PagedList(IEnumerable<T> source, int currentPage, int pageSize)
            : this(source == null ? new List<T>().AsQueryable() : source.AsQueryable(), currentPage, pageSize)
        {

        }

        private PagedList(IQueryable<T> source, int currentPage, int pageSize)
        {
            TotalItemCount = source.Count();
            CurrentPage = currentPage < 1 ? 1 : currentPage;

            if(TotalItemCount <= 0)
                return;
            
            if (pageSize == -1)
                Data = source.ToArray();
            else 
                Data = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToArray();

            PageCount = (int)Math.Ceiling(TotalItemCount / (decimal) Data.Length);

            Count = Data.Length;
        }

        public PagedList(IEnumerable<T> source, int currentPage, int pageCount, int totalItemCount)
        {
            var enumerable = source.ToList();
            Count = enumerable.Count;

            if (Count > pageCount)
                enumerable = enumerable.Skip((currentPage - 1) * pageCount).ToList();
            
            enumerable = enumerable.Take(pageCount).ToList();

            // add items to internal list
            if (Count <= 0) 
                return;
            
            TotalItemCount = totalItemCount;
            PageCount = pageCount;
            CurrentPage = currentPage;
            Data = enumerable.ToArray();
        }

        public IPagedList<TResult> TransformData<TResult>(Func<IEnumerable<T>, IEnumerable<TResult>> transform)
        {
            var transformedData = transform(Data);

            return new PagedList<TResult>(transformedData, CurrentPage, PageCount, TotalItemCount);
        }

        T[] _data;

        public T[] Data
        {
            get {
                if (_data == null)
                    _data = new T[0];

                return _data;
            }
            set {
                _data = value;
            }
        }

        public int TotalItemCount { get; set; }

        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public int PageCount { get; set; }
    }
}