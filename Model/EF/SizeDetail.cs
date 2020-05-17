namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SizeDetail")]
    public partial class SizeDetail
    {
       
        public long ID { get; set; }

        public long? Quantity { get; set; }

        [StringLength(50)]
        public string Size { get; set; }

        public long DetailID { get; set; }
    }
}
