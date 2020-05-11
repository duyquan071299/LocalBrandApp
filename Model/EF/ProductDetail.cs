namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Size { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Phải là số dương")]
        public int? Quantity { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public long ProductID { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }
    }
}
