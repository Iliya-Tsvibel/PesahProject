using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketProject
{
    class Program
    {
        static void Main(string[] args)
        {
        
            UserProcessDAO online = new UserProcessDAO();
            SupplierProcessDAO onlinemarket = new SupplierProcessDAO();
            ActionListDAO track = new ActionListDAO();
            Console.WriteLine("********************************************************************************\n" +
                              "WOW!!! You hit the page of the 'Iliya Tsvibel's market' - world's largest store!\n" +
                              "********************************************************************************\n");

            Console.WriteLine("\n\n\n\nChoose one of the options below:\n" +
                   "______________________________________\n" +
                   "Choose '1' If you an existing customer.\n" +
                   "Choose '2' To join us creating new customer account.\n" +
                   "Choose '3' If you an existing vendor.\n" +
                   "Choose '4' To join us creating new vendor account.\n" +
                   "Choose '5' To see the history of all actions.\n" +
                   "Choose 0 For exit.\n");


            int choice = Convert.ToInt32(Console.ReadLine());

            while (true)
            {

                switch (choice)
                {

                    case 1:
                        {
                            Console.WriteLine("\nLog your NikName and password:\n" +
                                 "*******************");
                            Console.Write("NikName: "); string nikName = Console.ReadLine();
                            Console.Write("Password: "); string password = Console.ReadLine();
                            online.Login(nikName, password);
                            return;
                        }
                    case 2:
                        online.AddNewUser();
                        return;
                    case 3:
                        {
                            Console.WriteLine("\nEnter Your Details:\n" +
                                "********************");
                            Console.Write("UserName: "); string userName = Console.ReadLine();
                            Console.Write("Password: "); string password = Console.ReadLine();
                            onlinemarket.LoginToVendor(userName, password);
                            return;
                        }
                    case 4:
                        onlinemarket.AddNewVendor();
                        return;
                    case 5:
                        track.ShowActionsList();
                        return;
                    case 0:
                        return;
                    default:
                        throw new WrongChoiceException("\n**Wrong choice! You can't choose this number. Please, exit and next time try one of the options listed in the menu**\n");
                        
                }
                

            }

        }
    }
}
