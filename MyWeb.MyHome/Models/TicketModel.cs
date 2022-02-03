using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.MyHome.Models
{
    public class TicketModel
    {
        public int Ticket_Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public static List<TicketModel> GetList(string status)
        {
            using (var conn = new MySqlConnection("Server=localhost;Database=myweb;Uid=root;Pwd=maria;"))
            //using (var conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=maria;"))
            {
                conn.Open();

                string sql = @"
                SELECT
                	A.ticket_id
                	,A.title
                	,A.status
                FROM
                	t_ticket A
                WHERE
                	A.status = @status
                ";

                return Dapper.SqlMapper.Query<TicketModel>(conn, sql, new { status = status }).ToList();
            }
        }

        public int Update()
        {
            string sql = @"
                UPDATE t_ticket
                SET
	                title = @title
                WHERE
	                ticket_id = @ticket_id
                ";

            using (var conn = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=maria;"))
            {
                conn.Open();

                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }
    }
}
