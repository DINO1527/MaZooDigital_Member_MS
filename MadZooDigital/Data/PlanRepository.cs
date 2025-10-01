using System.Collections.Generic;
using MadZooDigital.Models;
using System.Data.SqlClient;

namespace MadZooDigital.Data
{
    public class PlanRepository
    {
        public List<MembershipPlan> GetAll()
        {
            var list = new List<MembershipPlan>();
            using (var conn = DbHelper.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT PlanId, PlanName, Type FROM MembershipPlan";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new MembershipPlan
                    {
                        PlanID = reader.GetInt32(0),
                        //PlanName = reader.GetString(1),
                        //PlanTperrrpeype = reader.GetString(2)
                    });
                }
            }
            return list;
        }
    }
}
