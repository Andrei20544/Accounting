namespace Accounting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Apartaments
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string StreetName { get; set; }

        [StringLength(50)]
        public string HouseNumber { get; set; }

        public int? ApartamentNumber { get; set; }

        public double? SquareApartament { get; set; }

        public int? AmountRoom { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
    }
}
