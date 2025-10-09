using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadZooDigital.Models
{
    public class MemberStatement
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<StatementItem> Items { get; set; } = new List<StatementItem>();

        public decimal Total
        {
            get
            {
                decimal sum = 0;
                foreach (var it in Items) sum += it.Amount;
                return sum;
            }
        }
    }
}
