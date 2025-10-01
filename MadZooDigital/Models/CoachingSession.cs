using System;

namespace MadZooDigital.Models
{
    public class CoachingSession
    {
        public int SessionID { get; set; }
        public int MemberID { get; set; }
        public DateTime SessionDate { get; set; }
        public int Hours { get; set; }             // 1–4 hours
        public decimal HourlyRateLKR { get; set; } // default 1000
    }
}
