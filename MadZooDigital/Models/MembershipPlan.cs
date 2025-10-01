using System;

namespace MadZooDigital.Models
{
    public class MembershipPlan
    {
        public int PlanID { get; set; }
        public string PlanType { get; set; }    // e.g., "Annual", "6 months"
        public string Category { get; set; }    // "Individual" or "Family"
        public decimal Fee { get; set; }
        public string PersonMode { get; set; }  // "Adult", "Student", "Both"
    }
}
