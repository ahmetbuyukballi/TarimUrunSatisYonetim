﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class EntityBase
    {
        public Guid Id { get; set; }    
        public DateTime CreatedTime { get; set; }=DateTime.Now;
        public bool IsDeleted { get; set; }=false;
    }
}
