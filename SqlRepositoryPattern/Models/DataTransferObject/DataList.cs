using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataTransferObject
{
    public class DataList<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
