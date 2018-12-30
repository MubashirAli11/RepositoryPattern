using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ReponseModels
{
    public class DataTransfer<TEntity>
    {
        public bool IsSuccess { get; set; }
        public TEntity Response { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public DataTransfer()
        {
            this.IsSuccess = true;
        }
    }
}
