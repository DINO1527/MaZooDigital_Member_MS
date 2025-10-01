using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MadZooDigital.Models;

namespace MadZooDigital.Data
{
    public class MemberRepository
    {
        public List<Member> GetAll(string search = null)
        {
            var list = new List<Member>();
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())

                {
                    cmd.CommandText = @"SELECT m.MemberID, m.FullName, m.Age, m.Phone, m.Weight, m.DOB, m.Status,
           e.Enroll_ID, e.PlanID, e.FamilyID, e.PersonType, e.StartDate, e.EndDate, e.Plan_Status,
           p.PlanType, p.Category, p.Fee, p.PersonMode
    FROM Member m
    INNER JOIN EnrollPlan e ON m.MemberID = e.MemberID
    INNER JOIN MembershipPlan p ON e.PlanID = p.PlanID
    WHERE m.Status = 'active'";
                    if (!string.IsNullOrEmpty(search))
                    {
                        cmd.CommandText += " AND m.FullName LIKE @search";
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    }
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Member
                            {
                                MemberID = reader["MemberID"] != DBNull.Value ? Convert.ToInt32(reader["MemberID"]) : 0,
                                PlanID = reader["PlanID"] != DBNull.Value ? Convert.ToInt32(reader["PlanID"]) : 0,
                                FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : string.Empty,
                                Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : 0,
                                DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : (DateTime?)null,
                                StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : string.Empty,
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty,
                                PersonType = reader["PersonType"] != DBNull.Value ? reader["PersonType"].ToString() : string.Empty,
                                FamilyID = reader["FamilyID"] != DBNull.Value ? Convert.ToInt32(reader["FamilyID"]) : 0
                            });

                        }
                    }
                }
                return list;
            }
        }


        //   member detail form
        // 1. Get distinct plan types
        public List<string> GetPlanTypes()
        {
            var list = new List<string>();
            using (var conn = DbHelper.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT DISTINCT PlanType FROM MembershipPlan WHERE PlanType IS NOT NULL ORDER BY PlanType";
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        list.Add(rdr.GetString(0));
                }
            }
            return list;
        }

        // 2. Get distinct categories for a plan type (duplicates removed by DISTINCT)
        public List<string> GetCategoriesByPlanType(string planType)
        {
            var list = new List<string>();
            using (var conn = DbHelper.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT DISTINCT Category FROM MembershipPlan WHERE PlanType = @planType AND Category IS NOT NULL ORDER BY Category";
                cmd.Parameters.AddWithValue("@planType", planType ?? (object)DBNull.Value);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        list.Add(rdr.GetString(0));
                }
            }
            return list;
        }

        // 3. Get the plan details (fee + personMode) for selected planType+category
        public MembershipPlan GetPlanByTypeAndCategory(string planType, string category)
        {
            MembershipPlan plan = null;
            using (var conn = DbHelper.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT TOP 1 PlanID, PlanType, Category, Fee, PersonMode
                    FROM MembershipPlan
                    WHERE PlanType = @planType AND Category = @category";
                cmd.Parameters.AddWithValue("@planType", planType ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@category", category ?? (object)DBNull.Value);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        plan = new MembershipPlan
                        {
                            PlanID = rdr["PlanID"] != DBNull.Value ? Convert.ToInt32(rdr["PlanID"]) : 0,
                            PlanType = rdr["PlanType"].ToString(),
                            Category = rdr["Category"].ToString(),
                            Fee = rdr["Fee"] != DBNull.Value ? Convert.ToDecimal(rdr["Fee"]) : 0m,
                            PersonMode = rdr["PersonMode"] != DBNull.Value ? rdr["PersonMode"].ToString() : string.Empty,

                 


                    };
                        
                       
                    }
                }
            }
            return plan;
        }

     

            // --- Get next FamilyID ---
            private int GetNextFamilyId(SqlConnection conn, SqlTransaction tx)
            {
                using (var cmd = new SqlCommand("SELECT ISNULL(MAX(FamilyID), 0) + 1 FROM EnrollPlan", conn, tx))
                {
                cmd.CommandTimeout = 120;
                return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

        // --- Save Member + Family ---
        public void SaveMemberWithFamily(Member mainMember, List<Member> familyMembers)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    
                    try
                    {
                        // Get new family id
                        int familyId = GetNextFamilyId(conn, tx);

                        // ───── Main Member ─────
                      

                        AddEnrollPlanForMember(conn, tx, mainMember,
                                               mainMember.PlanID, familyId,
                                               mainMember.PersonType,
                                               mainMember.StartDate ?? DateTime.Now,
                                               null,
                                               mainMember.PlanStatus ?? "Active");

                        // ───── Family Members ─────
                        foreach (var fm in familyMembers)
                        {
                           

                            AddEnrollPlanForMember(conn, tx, fm,
                                                   fm.PlanID, familyId,
                                                   fm.PersonType,
                                                   fm.StartDate ?? DateTime.Now,
                                                   null,
                                                   fm.PlanStatus ?? "Active");
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }


 


        public Member GetMemberByFullName(string fullName)
        {
            const string sql = @"
    SELECT TOP 1 
        m.MemberID, 
        m.FullName, 
        m.Age, 
        m.DOB, 
        m.Weight, 
        m.Phone, 
        m.Status,
        ep.PlanId,
        ep.FamilyID,
        ep.PersonType,
        ep.StartDate,
        ep.Plan_Status
    FROM Member m
    INNER JOIN EnrollPlan ep ON m.MemberID = ep.MemberID
    WHERE m.FullName = @FullName";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@FullName", fullName.Trim());
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return null;
                    return new Member
                    {
                        MemberID = Convert.ToInt32(rdr["MemberID"]),
                        FullName = rdr["FullName"].ToString(),
                        Age = rdr["Age"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Age"]),
                        DOB = rdr["DOB"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["DOB"]),
                        Weight = rdr["Weight"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["Weight"]),
                        Phone = rdr["Phone"]?.ToString(),
                        Status = rdr["Status"]?.ToString(),
                        PersonType = rdr["PersonType"]?.ToString(),
                        FamilyID = rdr["FamilyID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["FamilyID"]),
                        PlanID = rdr["PlanID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["PlanID"]),
                        PlanStatus = rdr["Plan_Status"]?.ToString()
                    };
                }
            }
        }

        public bool MemberHasActiveEnrollPlan(int memberId)
        {
            const string sql = @"
            SELECT COUNT(1) FROM EnrollPlan
            WHERE MemberID = @MemberID AND Plan_Status = 'Active'";
            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MemberID", memberId);
                conn.Open();
                int c = Convert.ToInt32(cmd.ExecuteScalar());
                return c > 0;
            }
        }

        // Insert a new EnrollPlan record for an existing member (no member insert)
        public void AddEnrollPlanForMember(SqlConnection conn, SqlTransaction tx, Member member,
                                     int planId, int familyId, string personType,
                                     DateTime startDate, DateTime? endDate, string planStatus)
        {
            int memberId = member.MemberID;

            if (memberId > 0)
            {
                // Existing member → update details
                const string updateSql = @"
            UPDATE Member
            SET FullName = @FullName,
                Age = @Age,
                DOB = @DOB,
                Weight = @Weight,
                Phone = @Phone,
                Status = @Status
            WHERE MemberID = @MemberID";

                using (var cmd = new SqlCommand(updateSql, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@FullName", member.FullName ?? "");
                    cmd.Parameters.AddWithValue("@Age", member.Age);
                    cmd.Parameters.AddWithValue("@DOB", (object)member.DOB ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Weight", member.Weight);
                    cmd.Parameters.AddWithValue("@Phone", member.Phone ?? "");
                    cmd.Parameters.AddWithValue("@Status", member.Status ?? "Active");
                    cmd.Parameters.AddWithValue("@MemberID", memberId);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                // New member → insert first and fetch new MemberID
                const string insertSql = @"
            INSERT INTO Member (FullName, Age, DOB, Weight, Phone,  Status)
            VALUES (@FullName, @Age, @DOB, @Weight, @Phone,  @Status);
            SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(insertSql, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@FullName", member.FullName ?? "");
                    cmd.Parameters.AddWithValue("@Age", member.Age);
                    cmd.Parameters.AddWithValue("@DOB", (object)member.DOB ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Weight", member.Weight);
                    cmd.Parameters.AddWithValue("@Phone", member.Phone ?? "");
                    cmd.Parameters.AddWithValue("@Status", member.Status ?? "Active");

                    memberId = Convert.ToInt32(cmd.ExecuteScalar());
                    member.MemberID = memberId; // keep in sync
                }
            }

            // In both cases → add a new EnrollPlan row
            const string enrollSql = @"
        INSERT INTO EnrollPlan (MemberID, PlanID, FamilyID, PersonType, StartDate, EndDate, Plan_Status)
        VALUES (@MemberID, @PlanID, @FamilyID, @PersonType, @StartDate, @EndDate, @PlanStatus)";
            using (var cmd = new SqlCommand(enrollSql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@MemberID", memberId);
                cmd.Parameters.AddWithValue("@PlanID", planId);
                cmd.Parameters.AddWithValue("@FamilyID", familyId);
                cmd.Parameters.AddWithValue("@PersonType", personType ?? "");
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", (object)endDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PlanStatus", planStatus ?? "Active");
                cmd.ExecuteNonQuery();
            }
        }








        //member id get
        public List<Member> GetMembersByFamilyOrMemberId(int memberId)
        {
            var list = new List<Member>();

            using (var conn = DbHelper.GetConnection())
            { conn.Open(); 
            using (var cmd = conn.CreateCommand())
            {
                // Step 1: find FamilyID of the selected member
                cmd.CommandText =@" SELECT TOP 1 FamilyID FROM EnrollPlan 
                                 WHERE MemberID = @MemberID ORDER BY StartDate DESC";

                    cmd.Parameters.AddWithValue("@MemberID", memberId);
                    

                    object result = cmd.ExecuteScalar();
                int familyId = (result == DBNull.Value) ? 0 : Convert.ToInt32(result);

                // Step 2: if FamilyID > 0 → load all with same FamilyID, else just load this member
                cmd.Parameters.Clear();
                if (familyId > 0)
                {
                    cmd.CommandText = @"SELECT m.MemberID, m.FullName, m.Age, m.Phone, m.Weight, m.DOB, m.Status,
           e.Enroll_ID, e.PlanID, e.FamilyID, e.PersonType, e.StartDate, e.EndDate, e.Plan_Status,
           p.PlanType, p.Category, p.Fee, p.PersonMode
    FROM Member m
    INNER JOIN EnrollPlan e ON m.MemberID = e.MemberID
    INNER JOIN MembershipPlan p ON e.PlanID = p.PlanID
    WHERE e.FamilyID = @FamilyID AND e.Plan_Status=@status";
                    cmd.Parameters.AddWithValue("@FamilyID", familyId);
                        cmd.Parameters.AddWithValue("@status", "Active");
                    }
                else
                {
                        cmd.CommandText = @"SELECT m.MemberID, m.FullName, m.Age, m.Phone, m.Weight, m.DOB, m.Status,
           e.Enroll_ID, e.PlanID, e.FamilyID, e.PersonType, e.StartDate, e.EndDate, e.Plan_Status,
           p.PlanType, p.Category, p.Fee, p.PersonMode
    FROM Member m
    INNER JOIN EnrollPlan e ON m.MemberID = e.MemberID
    INNER JOIN MembershipPlan p ON e.PlanID = p.PlanID WHERE MemberID = @MemberID AND e.Plan_Status=@status";
                    cmd.Parameters.AddWithValue("@MemberID", memberId);
                        cmd.Parameters.AddWithValue("@status", "Active");
                    }

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Member
                        {
                            MemberID = Convert.ToInt32(rdr["MemberID"]),
                            PlanID = Convert.ToInt32(rdr["PlanID"]),
                            FullName = rdr["FullName"].ToString(),
                            Age = Convert.ToInt32(rdr["Age"]),
                            DOB = Convert.ToDateTime(rdr["DOB"]),
                            Weight = rdr["Weight"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["Weight"]),
                            Phone = rdr["Phone"].ToString(),
                            StartDate = Convert.ToDateTime(rdr["StartDate"]),
                            Status = rdr["Status"].ToString(),
                            PersonType = rdr["PersonType"].ToString(),
                            FamilyID = rdr["FamilyID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["FamilyID"])
                        });
                    }
                }
            }
        }

            return list;
        }
        public MembershipPlan GetPlanById(int planId)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT PlanID, PlanType, Category, Fee FROM MembershipPlan WHERE PlanID = @PlanID";
                    cmd.Parameters.AddWithValue("@PlanID", planId);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            return new MembershipPlan
                            {
                                PlanID = Convert.ToInt32(rdr["PlanID"]),
                                PlanType = rdr["PlanType"].ToString(),
                                Category = rdr["Category"].ToString(),
                                Fee = Convert.ToDecimal(rdr["Fee"])
                            };
                        }
                    }
                }
                return null;
            }
        }

        // Deactivate single member
        public void DeactivateMember(int memberId , int familyId)
        {
            using (var conn = DbHelper.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
            UPDATE Member 
            SET Status = 'Inactive' 
            WHERE MemberID = @MemberID;

            UPDATE EnrollPlan 
            SET Plan_Status = 'Inactive', 
                EndDate = @CurrentDate 
            WHERE FamilyID = @FamilyID AND MemberID=@MemberID;";
                cmd.Parameters.AddWithValue("@FamilyID", familyId);
                cmd.Parameters.AddWithValue("@MemberID", memberId);
                cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Deactivate entire family plan
        public void DeactivateEnrollPlan(int familyId, int memberId)
        {
            using (var conn = DbHelper.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
            UPDATE Member 
            SET Status = 'Inactive' 
            WHERE MemberID = @MemberID;

            UPDATE EnrollPlan 
            SET Plan_Status = 'Inactive', 
                EndDate = @CurrentDate 
            WHERE FamilyID = @MemberID;";

                cmd.Parameters.AddWithValue("@MemberID", memberId);
                cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Update existing member
        public void UpdateMemberWithFamily(Member mainMember, List<Member> familyMembers)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        



                        // 2. Update family members
                        if (familyMembers != null && familyMembers.Count > 0)

                        {
                            foreach (var f in familyMembers)
                                if (!string.Equals(f.Status, "Inactive", StringComparison.OrdinalIgnoreCase))
                                {
                                    UpdateMember(conn, tx, f);
                                }
                        }
                        else
                        { // 1. Update main member
                            UpdateMember(conn, tx, mainMember);
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        private void UpdateMember(SqlConnection conn, SqlTransaction tx, Member member)
        {
            string sql = @"
        UPDATE Member
        SET FullName = @FullName, 
            Age       = @Age,
            DOB       = @DOB,
            Weight    = @Weight,
            Phone     = @Phone,
            Status    = @Status
        WHERE MemberID = @MemberID
          AND Status <> 'Inactive';";

            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@FullName", member.FullName);
                cmd.Parameters.AddWithValue("@Age", member.Age);
                cmd.Parameters.AddWithValue("@DOB", (object)member.DOB ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Weight", member.Weight);
                cmd.Parameters.AddWithValue("@Phone", (object)member.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", member.Status);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberID);

                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteFamilyMembers(SqlConnection conn, SqlTransaction tx, int familyId, int mainMemberId)
        {
            string sql = "DELETE FROM Member WHERE FamilyID = @FamilyID AND MemberID <> @MainMemberId";
            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@FamilyID", familyId);
                cmd.Parameters.AddWithValue("@MainMemberId", mainMemberId);
                cmd.ExecuteNonQuery();
            }
        }


    }
}