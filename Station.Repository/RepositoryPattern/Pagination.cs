namespace Station.Repository.RepositoryPattern
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        public Pagination()
        {
            this.PageIndex = 0;
            this.PageSize = 20;
        }
        /// <summary>
        /// 当前页，索引从0开始。
        /// </summary>
        public int PageIndex { get; set; }
        public int PageIndexReal => PageIndex + 1;
        int _pageSize;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                if (_pageSize <= 0)
                {
                    _pageSize = 20;
                }
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int AllPage
        {
            get
            {
                long num = RecordCount / (long)PageSize;
                if (RecordCount % PageSize > 0)
                {
                    num++;
                }
                return (int)num;
            }
        }
        /// <summary>
        /// 总数据量
        /// </summary>
        public int RecordCount { get; set; }
        public string OrderBy { get; set; }
        public string OrderByDescending { get; set; }
        public string ThenBy { get; set; }
        public string ThenByDescending { get; set; }
    }
}