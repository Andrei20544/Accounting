namespace Accounting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Realtors
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string FIO { get; set; }

        public int? Percent { get; set; }
    }
}
