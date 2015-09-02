using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BayEquityTest.Models
{
    public class PipelineResult
    {
        public int OriginatedCount { get; set; }
        public decimal OriginatedAmount { get; set; }
        public int FundedCount { get; set; }
        public decimal FundedAmount { get; set; }
        public int SubmittedCount { get; set; }
        public decimal SubmittedAmount { get; set; }
        public decimal PurchasePct { get; set; }
    }
}