using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axolotl.Domain.Entities
{
    public class Products : BaseEntity
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public long? CategoryId { get; set; }
        public bool Active { get; set; } = true;
    }
}
