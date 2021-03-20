namespace Accounting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dealings
    {
        [Key]
        public int IDDeal { get; set; }

        public int? IDRealtor { get; set; }

        public int? IDApartamemt { get; set; }
    }
}
