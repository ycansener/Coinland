using System;

namespace Coinland.Core.Infrastructure.Data.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
