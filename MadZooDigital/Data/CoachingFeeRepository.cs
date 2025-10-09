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
        // 🔹 Get total hours already booked for this member in the selected week
        public int GetWeeklyHours(int memberId, DateTime selectedDate)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();

                DateTime weekStart = selectedDate.AddDays(-(int)selectedDate.DayOfWeek); // Sunday start
                DateTime weekEnd = weekStart.AddDays(7);

                string sql = @"SELECT ISNULL(SUM(CoachingHours), 0)
                               FROM CoachingFee
                               WHERE MemberID = @MemberID
                               AND Date >= @WeekStart AND Date < @WeekEnd";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberId);
                    cmd.Parameters.AddWithValue("@WeekStart", weekStart);
                    cmd.Parameters.AddWithValue("@WeekEnd", weekEnd);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // 🔹 Add coaching fee record
        public void Add(CoachingFee fee)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO CoachingFee 
                               (MemberID, FamilyID, CoachingHours, Date, SubTotal)
                               VALUES (@MemberID, @FamilyID, @CoachingHours, @Date, @SubTotal)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", fee.MemberID);
                    cmd.Parameters.AddWithValue("@FamilyID", (object)fee.FamilyID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CoachingHours", fee.CoachingHours);
                    cmd.Parameters.AddWithValue("@Date", fee.Date);
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
                SELECT DISTINCT m.MemberID, m.FullName, m.Age, m.Phone, m.Weight, m.DOB, m.Status,e.FamilyID
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
                            m.FamilyID = reader.GetInt32(7);
                            list.Add(m);
                        }
                    }
                }
            }
            return list;
        }

    }
}
