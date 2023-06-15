using Divena_CMS.Infrastructure;
using System;
using System.Collections.Generic;

namespace Divena_CMS.Models.ViewModels
{
    public class OrderList
    {
        public IEnumerable<Order> order { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string validatedBy { get; set; }
    }
}
