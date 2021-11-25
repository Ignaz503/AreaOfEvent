using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Model.Events.Categories
{
    public class Category
    {
        public long ID { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
