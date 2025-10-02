using MadZooDigital.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadZooDigital.Data
{
    class CoachingFeeRepository
    {
        public void Add(CoachingFee fee)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                INSERT INTO CoachingFee (MemberID, CoachingHours, FeePerHour, MonthYear, SubTotal)
                VALUES (@MemberID, @Hours, @FeePerHour, @MonthYear, @SubTotal)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", fee.MemberID);
                    cmd.Parameters.AddWithValue("@Hours", fee.CoachingHours);
                    cmd.Parameters.AddWithValue("@FeePerHour", fee.FeePerHour);
                    cmd.Parameters.AddWithValue("@MonthYear", fee.MonthYear);
                    cmd.Parameters.AddWithValue("@SubTotal", fee.SubTotal);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Member> SearchActiveMembers(string nameFilter)
        {
            var list = new List<Member>();

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                SELECT DISTINCT m.MemberID, m.FullName, m.Age, m.Phone, m.Weight, m.DOB, m.Status
                FROM Member m
                INNER JOIN EnrollPlan e ON m.MemberID = e.MemberID
                WHERE m.Status = 'Active'
                  AND e.Plan_Status = 'Active'
                  AND m.FullName LIKE @name + '%'";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", nameFilter);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var m = new Member();
                            m.MemberID = reader.GetInt32(0);
                            m.FullName = reader.GetString(1);
                            m.Age = reader.GetInt32(2);
                            m.Phone = reader.GetString(3);
                            m.Weight = reader.GetDecimal(4);
                            m.DOB = reader.GetDateTime(5);
                            m.Status = reader.GetString(6);
                            list.Add(m);
                        }
                    }
                }
            }
            return list;
        }

    }
}
