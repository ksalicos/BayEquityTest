namespace BayEquityTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pipeline")]
    public partial class Pipeline
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string LoanOfficerKey { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string LoanNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string BorrowerFirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string BorrowerLastName { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public int PurposeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Originated { get; set; }

        [Column(TypeName = "date")]
        public DateTime SubmittedToUW { get; set; }

        [Column(TypeName = "date")]
        public DateTime Funded { get; set; }

        public virtual Purpose Purpose { get; set; }
    }
}
