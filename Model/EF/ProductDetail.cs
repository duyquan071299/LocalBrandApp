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

        public long ProductID { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [StringLength(50)]
        public string Color { get; set; }
    }
}
