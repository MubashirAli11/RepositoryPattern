using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        [MaxLength(200)]
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
