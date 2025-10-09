using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadZooDigital.Models
{
    public class StatementItem
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } // e.g. "Coaching - 2 hrs" or "Match - 1 match"
        public decimal Amount { get; set; }
        public string ItemType { get; set; } // "Coaching" or "Match"

    }
}
