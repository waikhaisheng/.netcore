using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models.DatabaseModels;
using Common.Utils;
using Common.Helpers;
using Database.Users.Interfaces;

namespace Database.Users
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy:
    /// Updated:
    /// </summary>
    public class UserDbContext : IUserDbContext
    {
        private readonly string _dbConnectionString;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy:
        /// Updated:
        /// </summary>
        /// <param name="dbConnectionString"></param>
        public UserDbContext(string dbConnectionString) 
        {
            _dbConnectionString = dbConnectionString;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy:
        /// Updated:
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string email, string password)
        {
            var user = new User();
            var passwordHash = CryptographyUtil.HashString(password);
            using (SqlConnection conn = new System.Data.SqlClient.SqlConnection(_dbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = passwordHash;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Id = reader["Id"].ObjectOrDefaultDBNull<Guid>();
                        user.Email = reader["Email"].ObjectOrDefaultDBNull<string>();
                        user.Username = reader["Username"].ObjectOrDefaultDBNull<string>();
                        user.Created = reader["Created"].ObjectOrDefaultDBNull<DateTime>();
                        user.Updated = reader["Updated"].ObjectOrDefaultDBNull<DateTime>();
                    }
                }
            }
            return user;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy:
        /// Updated:
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(User user)
        {
            var addUsr = new User();
            addUsr.Id = user.Id;
            addUsr.Email = user.Email;
            addUsr.Password = CryptographyUtil.HashString(user.Password);
            addUsr.Username = user.Username;
            addUsr.Created = DateTime.Now;
            addUsr.Updated = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspAddUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = addUsr.Id;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = addUsr.Email;
                    cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = addUsr.Password;
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = addUsr.Username;
                    cmd.Parameters.Add("@Created", SqlDbType.DateTime).Value = addUsr.Created;
                    cmd.Parameters.Add("@Updated", SqlDbType.DateTime).Value = addUsr.Updated;
                    cmd.Parameters.Add("@Deleted", SqlDbType.Bit).Value = addUsr.Deleted;
                    conn.Open();
                    var ret = cmd.ExecuteNonQuery();
                    if (ret > 0)
                        return addUsr;
                }
            }
            return null;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy:
        /// Updated:
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UpdateUser(User user)
        {
            var updateUsr = new User();
            updateUsr.Id = user.Id;
            updateUsr.Email = user.Email;
            updateUsr.Password = CryptographyUtil.HashString(user.Password);
            updateUsr.Username = user.Username;
            updateUsr.Created = user.Created;
            updateUsr.Updated = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspUpdateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = updateUsr.Id;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = updateUsr.Email;
                    cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = updateUsr.Password;
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = updateUsr.Username;
                    cmd.Parameters.Add("@Updated", SqlDbType.DateTime).Value = updateUsr.Updated;
                    conn.Open();
                    var ret = cmd.ExecuteNonQuery();
                    if (ret > 0)
                        return updateUsr;
                }
            }
            return null;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy:
        /// Updated:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveUser(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(_dbConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspRemoveUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
                    conn.Open();
                    var ret = cmd.ExecuteNonQuery();
                    if (ret > 0)
                        return true;
                }
            }
            return false;
        }
    }
}
