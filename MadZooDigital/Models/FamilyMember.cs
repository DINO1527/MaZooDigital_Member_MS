using System;

namespace MadZooDigital.Models
{
    public class FamilyMember
    {
        public int FamilyMemberID { get; set; }
        public int ParentMemberID { get; set; } // links to Member.MemberID
        public int PlanID { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string PersonType { get; set; } // Adult / Student
        public string Status { get; set; } // Active / Inactive
    }
}
