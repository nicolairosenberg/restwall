﻿namespace RestLib.Infrastructure.Helpers
{
    public class BoardsParams
    {
        const int maxPageSize = 25;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
