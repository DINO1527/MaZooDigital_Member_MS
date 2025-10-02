using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadZooDigital.Models
{
    class CoachingFee
    {
        public int CoachingFeeID { get; set; }
        public int MemberID { get; set; }
        public int CoachingHours { get; set; }
        public decimal FeePerHour { get; set; }
        public string MonthYear { get; set; }
        public decimal SubTotal { get; set; }
    }
}
