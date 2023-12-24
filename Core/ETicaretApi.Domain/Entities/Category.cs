﻿using ETicaretApi.Domain.Common;
using ETicaretApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Domain.Entities
{
    public class Category:EntityBase 
    {
        public Category() 
        {
        }
        public Category(int parentId, string name, int priorty)
        {
            ParentId = parentId;
            Name = name;
            Priorty = priorty;
        }

        public required int ParentId { get; init; }
        public required string Name { get; init; }
        public required int Priorty { get; init; }

        public ICollection<Detail> Details { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
