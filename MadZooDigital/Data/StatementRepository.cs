using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MadZooDigital.Models;

namespace MadZooDigital.Data
{
    public class StatementRepository
    {
        /// <summary>
        /// Get coaching fee items for a member for given month/year
        /// </summary>
        public List<StatementItem> GetCoachingItems(int memberId, int month, int year)
        {
            var items = new List<StatementItem>();
            string sql = @"
SELECT CoachingFeeID, MemberID, FamilyID, CoachingHours, Date, SubTotal
FROM CoachingFee
WHERE MemberID = @MemberID
  AND YEAR([Date]) = @Year
  AND MONTH([Date]) = @Month
ORDER BY [Date]";

            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MemberID", memberId);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Month", month);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var date = Convert.ToDateTime(rdr["Date"]);
                        var hours = rdr["CoachingHours"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["CoachingHours"]);
                        var subtotal = rdr["SubTotal"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["SubTotal"]);
                        string desc = $"Coaching - {hours} hrs (ID {rdr["CoachingFeeID"]})";
                        items.Add(new StatementItem
                        {
                            Date = date,
                            Description = desc,
                            Amount = subtotal,
                            ItemType = "Coaching"
                        });
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Get match fee items for a member for given month/year
        /// </summary>
        public List<StatementItem> GetMatchItems(int memberId, int month, int year)
        {
            var items = new List<StatementItem>();
            string sql = @"
SELECT MatchFeeID, MemberID, FamilyID, MatchesPlayed, Date, SubTotal
FROM MatchFee
WHERE MemberID = @MemberID
  AND YEAR([Date]) = @Year
  AND MONTH([Date]) = @Month
ORDER BY [Date]";

            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MemberID", memberId);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Month", month);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var date = Convert.ToDateTime(rdr["Date"]);
                        var played = rdr["MatchesPlayed"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["MatchesPlayed"]);
                        var subtotal = rdr["SubTotal"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["SubTotal"]);
                        string desc = $"Match - {played} match(es) (ID {rdr["MatchFeeID"]})";
                        items.Add(new StatementItem
                        {
                            Date = date,
                            Description = desc,
                            Amount = subtotal,
                            ItemType = "Match"
                        });
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Build the full statement for a member and month/year
        /// </summary>
        public MemberStatement GetMemberStatement(int memberId, int month, int year)
        {
            var stmt = new MemberStatement
            {
                MemberID = memberId,
                Month = month,
                Year = year
            };

            // get member name
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand("SELECT MemberID, FullName FROM Member WHERE MemberID = @MemberID", conn))
            {
                cmd.Parameters.AddWithValue("@MemberID", memberId);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        stmt.MemberName = Convert.ToString(rdr["FullName"]);
                    }
                }
            }

            // fetch items
            var coaching = GetCoachingItems(memberId, month, year);
            var matches = GetMatchItems(memberId, month, year);

            stmt.Items.AddRange(coaching);
            stmt.Items.AddRange(matches);

            // Sort by date asc
            stmt.Items.Sort((a, b) => a.Date.CompareTo(b.Date));

            return stmt;
        }
    }
}
