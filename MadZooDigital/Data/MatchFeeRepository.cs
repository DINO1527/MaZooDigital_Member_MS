// Data/MatchFeeRepository.cs
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MadZooDigital.Models;

namespace MadZooDigital.Data
{
    public class MatchFeeRepository
    {
        public List<Member> GetActiveMembers()
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
                  AND e.Plan_Status = 'Active'";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Member
                            {
                                MemberID = Convert.ToInt32(reader["MemberID"]),
                                FullName = reader["FullName"].ToString(),
                                FamilyID = Convert.ToInt32(reader["FamilyID"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void SaveMatchFee(Matchfee fee)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO MatchFee(MemberID, FamilyID, MatchesPlayed, Date, SubTotal)
                               VALUES(@MemberID, @FamilyID, @MatchesPlayed, @Date, @SubTotal)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", fee.MemberID);
                    cmd.Parameters.AddWithValue("@FamilyID", fee.FamilyID);
                    cmd.Parameters.AddWithValue("@MatchesPlayed", fee.MatchesPlayed);
                    cmd.Parameters.AddWithValue("@Date", fee.Date);
                    cmd.Parameters.AddWithValue("@SubTotal", fee.SubTotal);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
