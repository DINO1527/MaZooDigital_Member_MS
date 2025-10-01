using System;
namespace MadZooDigital.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public int PlanID { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime? DOB { get; set; }
        public decimal Weight { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? StartDate { get; set; }
        public string Status { get; set; }
        public string PersonType { get; set; }
        public int FamilyID { get; set; }
        //public DateTime? EndDate { get; internal set; }
        public string PlanStatus { get;  set; }
    }
}

