using Divena_CMS.Infrastructure;
using System.Collections.Generic;

namespace Divena_CMS.Models.ViewModels
{
    public class MenuList
    {
        public IEnumerable<Menu> menu { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public int allTotal { get; set; }
        public int activeTotal { get; set; }
        public int inactiveTotal { get; set; }
        public string searchText { get; set; }
        public int? status { get; set; }
    }
}
