﻿using  Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles
{
    public class PaginatedList<T> : List<T>
    {
        public int pageIndex { get; private set; }
        public int totalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIdx, int pageSize)
        {
            pageIndex = pageIdx;
            totalPages = (int)Math.Ceiling(count/(double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (pageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (pageIndex < totalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> src, int pageIdx, int pageSize)
        {
            var count = await src.CountAsync();
            var items = await src.Skip((pageIdx-1)*pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIdx, pageSize);
        }

    
    }
}
