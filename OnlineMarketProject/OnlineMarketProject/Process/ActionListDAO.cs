using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketProject
{
    class ActionListDAO
    {
        static SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=CustomersDB; Integrated Security=true;");

        public ActionListDAO()
        {
            conn.Open();
        }
        //Action list
        public void ShowActionsList()
        {
            int x = 1;
            using (SqlCommand cmd = new SqlCommand($"Select * From Actions", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        Console.WriteLine($"{x++}) \n___ \n|#|Date: {reader["Date"]}. |#|The Act: {reader["Type"]}. |#|It's Suceed? {reader["ItsSucceed"]}.\n");
                    }
                }
            }
        }
    }
}
