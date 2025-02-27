﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Domain.Entities
{
    public class BaseEntity<T> : BaseEntity
    {
        [Key]
        public T Id { get; set; }
    }

    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public virtual void DeleteRecord()
        {
            IsDeleted = true;
        }
    }   
}
