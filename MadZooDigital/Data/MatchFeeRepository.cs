using System;
using System.Data.SqlClient;
using MadZooDigital.Models;

namespace MadZooDigital.Data
{
    public class MatchFeeRepository
    {
        public void Add(Matchfee fee)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                INSERT INTO MatchFee (MemberID, MatchesPlayed, FeePerMatch, MonthYear, SubTotal)
                VALUES (@MemberID, @MatchesPlayed, @FeePerMatch, @MonthYear, @SubTotal)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", fee.MemberID);
                    cmd.Parameters.AddWithValue("@MatchesPlayed", fee.MatchesPlayed);
                    cmd.Parameters.AddWithValue("@FeePerMatch", fee.FeePerMatch);
                    cmd.Parameters.AddWithValue("@MonthYear", fee.MonthYear);
                    cmd.Parameters.AddWithValue("@SubTotal", fee.SubTotal);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Matchfee GetById(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT MatchFeeID, MemberID, MatchesPlayed, FeePerMatch, MonthYear, SubTotal 
                               FROM MatchFee WHERE MatchFeeID=@id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var fee = new Matchfee();
                            fee.MatchFeeID = reader.GetInt32(0);
                            fee.MemberID = reader.GetInt32(1);
                            fee.MatchesPlayed = reader.GetInt32(2);
                            fee.FeePerMatch = reader.GetDecimal(3);
                            fee.MonthYear = reader.GetString(4);
                            fee.SubTotal = reader.GetDecimal(5);
                            return fee;
                        }
                    }
                }
            }
            return null;
        }

        public void Update(Matchfee fee)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = @"
                UPDATE MatchFee
                SET MemberID=@MemberID, MatchesPlayed=@MatchesPlayed, 
                    FeePerMatch=@FeePerMatch, MonthYear=@MonthYear, SubTotal=@SubTotal
                WHERE MatchFeeID=@MatchFeeID";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", fee.MemberID);
                    cmd.Parameters.AddWithValue("@MatchesPlayed", fee.MatchesPlayed);
                    cmd.Parameters.AddWithValue("@FeePerMatch", fee.FeePerMatch);
                    cmd.Parameters.AddWithValue("@MonthYear", fee.MonthYear);
                    cmd.Parameters.AddWithValue("@SubTotal", fee.SubTotal);
                    cmd.Parameters.AddWithValue("@MatchFeeID", fee.MatchFeeID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM MatchFee WHERE MatchFeeID=@id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
