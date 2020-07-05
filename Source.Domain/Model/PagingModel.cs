namespace Source.Domain.Model
{
    public class PagingModel
    {
        const int maxPageSize = 10;

        public int pageNumber { get; set; } = 1;
        
        public int _pageSize { get; set; } = 6;

        public int pageSize
        {
            get { return _pageSize; }
            set 
            {
                _pageSize  = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}