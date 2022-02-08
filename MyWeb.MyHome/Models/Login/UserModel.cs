using MySql.Data.MySqlClient;
using System;

namespace MyWeb.MyHome.Models.Login
{
    public class UserModel
    {
        public uint User_Seq { get; set; }

        public string User_Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public void ConvertPassword()
        {
            var sha = new System.Security.Cryptography.HMACSHA512();
            sha.Key = System.Text.Encoding.UTF8.GetBytes(this.Password.Length.ToString());

            var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(this.Password));

            this.Password = System.Convert.ToBase64String(hash);
        }

        internal int Register()
        {
            // 중복 user_name이 있는지?
            // 중복 email이 있는지? 

            string sql = "INSERT INTO t_user (user_name,email,password) SELECT @user_name,@email,@password";

            using (var conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=maria;"))
            {
                conn.Open();

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }

        internal UserModel GetLoginUser()
        {
            // this.User_Name
            // this.Password

            string sql = "SELECT user_seq,user_name,email,password FROM t_user WHERE user_name=@user_name";

            UserModel user;

            using (var conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=maria;"))
            {
                conn.Open();

                user = Dapper.SqlMapper.QuerySingleOrDefault<UserModel>(conn, sql, this);
            }

            if(user == null)
            {
                throw new Exception("사용자가 존재하지 않습니다.");
            }

            if(user.Password!= this.Password)
            {
                throw new Exception("비밀번호가 틀립니다.");
                // 틀린 횟수, ...
            }

            return user;
        }
    }
}
