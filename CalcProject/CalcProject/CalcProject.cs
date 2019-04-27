using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject
{
    class CalcProject
    {
        static SqlConnection con;
        String sqlPath = @"Data Source=.;Initial Catalog=Calc Project;Integrated Security=true;";

        public CalcProject()
        {
            con = new SqlConnection($"{sqlPath}");
            con.Open();
        }
        public void AddNumbers(int x, int y)
        {
            using (SqlCommand cmd = new SqlCommand($"Insert Into XTable Values ({x}); Insert Into YTable Values ({y});", con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void PrintTheCalculator()
        {
            using (SqlCommand cmd = new SqlCommand("C_Result", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read() == true)
                    {
                        Console.WriteLine($" {reader["X"]} {reader["Operation"]} {reader["Y"]} {reader["Result"]}");
                    }
                }
            }
        }
    }
}
