using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketProject
{
    class SupplierProcessDAO
    {
        static SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=CustomersDB; Integrated Security=true;");

        public SupplierProcessDAO()
        {
            conn.Open();
        }
        //Login as a supplier
        public void LoginToVendor(string nikName, string password)
        {
            Dictionary<int, dynamic> tempVenById = ReadAllVendors();
            foreach (KeyValuePair<int, dynamic> ven in tempVenById)
            {
                if (nikName == ven.Value.NikName && password == ven.Value.Password)
                {
                    //This part created for the actions history view

                    using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{"Login To Vendor Account"}', '{true}')", conn)) { cmd.ExecuteNonQuery(); }
                    Console.WriteLine("\nLogin successfully!\n");
                    while (true)
                    {
                        Console.WriteLine("\n-- Choose '1' To Add or edit your items.\n" +
                               "-- Choose '2' To see list of your items.\n" +
                               "-- Choose '0' To Log Out.\n");


                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {

                            case 1:
                                {
                                    Console.WriteLine("\n\nEnter information about the item:\n" +
                                        "********************************");
                                    AddOrEditProduct(ven.Value.VenId);
                                }

                                break;
                            case 2:
                                {
                                    int x = 1;
                                    Console.WriteLine("\n\nThis is the list of your items:\n" +
                                        "************************************");
                                    //This part created for the actions history view
                                    using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{$"{ven.Value.NikName} Getting item list"}', '{true}');" +
                                        $"Select ProName,Price,Stock from Products Where Vendor_Id={ven.Value.VenId}", conn))
                                    {
                                        using (SqlDataReader reader = cmd.ExecuteReader())
                                        {
                                            while (reader.Read() == true)
                                            {
                                                Console.WriteLine($"{x++}) Product Name: {reader["ProName"]}. Price: {reader["Price"]}$. Ammount At Stock: {reader["Stock"]}.");
                                            }
                                            Console.WriteLine();
                                        }
                                    }
                                }
                                break;


                            case 0:

                                {
                                    //This part created for the actions history view
                                    using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{$"{ven.Value.NikName} Log Out From His Account"}', '{true}')", conn))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                    Console.WriteLine("\nYou Log Out From Your Account\n");
                                    return;
                                }


                            default:
                                throw new WrongChoiceException("\n**Wrong choice! You can't choose this number. Please, exit and next time try one of the options listed in the menu**\n");

                        }


                    }
                }
            }
        }

        //Add new supplier
        public void AddNewVendor()
        {
            Console.WriteLine("\nStart selling in 3 simple steps:\n" +
                 "***************************");
            Console.Write("1 of 3. Nik Name: "); string NikName = Console.ReadLine();
            Console.Write("2 of 3. Password: "); string Password = Console.ReadLine();
            Console.Write("3 of 3. You company name (optionally): "); string CompanyName = Console.ReadLine();
            Dictionary<int, dynamic> tempvenById = ReadAllVendors();
            foreach (KeyValuePair<int, dynamic> ven in tempvenById)
            {
                while (ven.Value.NikName == NikName)
                {
                    //This part created for the actions history view
                    using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{"Creating account with taken nikname"}', '{false}')", conn)) { cmd.ExecuteNonQuery(); }
                    Console.WriteLine($"\nThe Nik Name '{NikName}' is taken. Try Another Nik Name:");
                    NikName = Console.ReadLine();
                }
            }
            //This part created for the actions history view
            using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{"Creating new seller account"}', '{true}');" +
                $"Insert Into Vendors(UserName, Password, CompanyName) values('{NikName}','{Password}','{CompanyName}')", conn))
            {
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("\nYour seller account successfully created !");
        }
        //Insert suppliers to dictionary by ID
        public Dictionary<int, dynamic> ReadAllVendors()
        {
            Dictionary<int, dynamic> venById = new Dictionary<int, dynamic>();
            using (SqlCommand cmd = new SqlCommand($"Select * from Vendors", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        dynamic dv = new
                        {
                            VenId = (int)reader["venId"],
                            NikName = (string)reader["UserName"],
                            Password = (string)reader["Password"],
                            CompanyName = (string)reader["CompanyName"],
                        };
                        venById.Add(dv.VenId, dv);
                    }
                }
            }
            return venById;
        }

        //Add/Edit Product
        public void AddOrEditProduct(int venId)
        {
            bool UpdateProduct = false;
            int ammount;
            int price;
            //string optionForProduct = "3";
            Dictionary<int, dynamic> tempProById = ReadAllProducts();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            foreach (KeyValuePair<int, dynamic> pro in tempProById)
            {
                if (pro.Value.ProName == name)
                {
                    if (pro.Value.Vendor_Id == venId)
                    {
                        Console.Write("\nYou Already Promoted This Product.");
                        while (true)
                        {
                            Console.Write("\n\nPress '1' To Change The Amount At The Stock.\n" +
                                "Press '2' To Change The price.\n" +
                                "Press '0' To Exit.\n");


                            int choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {

                                case 1:
                                    {
                                        Console.WriteLine("\nEnter New Ammount To Stock For This Item: ");
                                        ammount = Convert.ToInt32(Console.ReadLine());
                                        using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{$"Vendor Change Ammount Of The Stock For His Product"}', '{true}');" +
                                            $"Update Products Set Price={ammount} Where Vendor_Id={venId} And ProName Like '{name}'", conn))
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.WriteLine($"The Stock Update To {ammount} Units.");
                                    }

                                    break;

                                case 2:
                                    {
                                        Console.WriteLine("\nEnter New Price For This Item: ");
                                        price = Convert.ToInt32(Console.ReadLine());
                                        using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{$"Vendor Change The Price For His Product"}', '{true}');" +
                                            $"Update Products Set Price={price} Where Vendor_Id={venId} And ProName Like '{name}'", conn))
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        Console.WriteLine($"Now, The Price For This Product Is {price}$ .");
                                    }
                                    break;


                                case 0:


                                    {
                                        UpdateProduct = true;
                                        {


                                            Console.WriteLine("\nReturned to previous menu\n");
                                            return;
                                        }
                                    }


                                default:
                                    throw new WrongChoiceException("\n**Wrong choice! You can't choose this number. Please, exit and next time try one of the options listed in the menu**\n");

                            }


                        }

                    }
                    if (pro.Value.Vendor_Id != venId)
                    {
                        //This part created for the actions history view
                        using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{$"Vendor Try To Add\\Edit Product Of Another Vendor"}', '{false}')", conn)) { cmd.ExecuteNonQuery(); }
                        Console.WriteLine("Sorry, But This Product Already Promoted By Another Vendor!\n");
                        UpdateProduct = true;
                    }
                }
            }
            if (UpdateProduct == false)
            {
                Console.Write("Price: "); price = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ammount: "); ammount = Convert.ToInt32(Console.ReadLine());
                using (SqlCommand cmd = new SqlCommand($"Insert Into Actions(Date,Type,ItsSucceed) Values('{DateTime.Now}','{$"Vendor Add New Product"}', '{true}')" +
                    $"Insert Into Products(ProName,Vendor_Id, Price, Stock) Values('{name}',{venId}, {price}, {ammount})", conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("\nGood! Your New Product Is Add To Your Products List!\n");
            }
        }


        //Insert items to dictionary by ID
        public Dictionary<int, object> ReadAllProducts()
        {
            Dictionary<int, dynamic> proById = new Dictionary<int, dynamic>();
            using (SqlCommand cmd = new SqlCommand($"Select * from products", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read() == true)
                    {
                        dynamic pd = new
                        {
                            ProId = (int)reader["ProId"],
                            ProName = (string)reader["ProName"],
                            Vendor_Id = (int)reader["Vendor_Id"],
                            Price = (int)reader["Price"],
                            Stock = (int)reader["Stock"]

                        };
                        proById.Add(pd.ProId, pd);
                    }
                }
            }
            return proById;
        }

    }
}
