namespace Netcorewebapi.Common
{
    public class ResourceParameters
    {
        public const int MaxPageSize = 50;
        public int Page { get; set; } = 1;
      
        private int _pageSize = 10;
        public int PerPage
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}