using MyWeb.Lib.DataBase;
using System.Collections.Generic;

namespace MyWeb.MyHome.Models
{
    public class TicketModel
    {
        public int Ticket_Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public static List<TicketModel> GetList(string _status)
        {
            using (var db = new MySqlDapperHelper())
            {
                string sql = "SELECT * FROM t_ticket ORDER BY ticket_id ASC";

                return db.Query<TicketModel>(sql, new { status = _status });
            }
        }

        public int Update()
        {
            string sql = "UPDATE t_ticket SET title = @title WHERE ticket_id = @ticket_id";

            using (var db = new MySqlDapperHelper())
            {

                return db.Execute(sql, this);


                //    db.BeginTransaction();
                //    try
                //    {
                //        int r = 0;
                //        string sql = "UPDATE t_ticket SET title = @title WHERE ticket_id = @ticket_id";
                //        r += db.Execute(sql, this);

                //        //sql = "UPDATE t_ticket SET title = @title WHERE ticket_id = @ticket_id"; //다른 쿼리
                //        //r += db.Execute(sql, this);

                //        db.Commit();

                //        return r;
                //    }
                //    catch(Exception ex)
                //    {
                //        db.Rollback();
                //        throw ex;
                //    }
            }
        }
    }
}
