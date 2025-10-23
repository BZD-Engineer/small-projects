using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Threading;

namespace Robot2
{
    internal class Program
    {
        static int read(string path)
        {
            int db = 0;
            StreamReader sr = new StreamReader(path);
            while(!sr.EndOfStream)
            {
                sr.ReadLine();
                db++;
            }
            sr.Close();
            return db; 
        }
        static Mobilrobot[] objectify(string path)
        {
            int n = read(path);
            Mobilrobot[] bots = new Mobilrobot[n];
            StreamReader sr = new StreamReader(path);
            for (int i = 0; i < n; i++)
            {
                bots[i] = new Mobilrobot(sr.ReadLine());
            }
            sr.Close();
            return bots;
        }

        static void list(Mobilrobot[] bots)
        {
            for (int i = 0; i < bots.Length; i++)
            {
                Console.WriteLine(bots[i].Display());
            }
        }

        static int search(Mobilrobot[] bots, string index)
        {
            for (int i = 0; i < bots.Length; i++)
            {
                if (bots[i].Id == index)
                    return i;
            }
            return -1; 
        }

        static void Write(Mobilrobot[] bots, string path)
        {
            StreamWriter sr = new StreamWriter(path,false);
            for (int i = 0; i < bots.Length; i++)
            {
                sr.WriteLine($"{bots[i].Id}#{bots[i].Mode}#{bots[i].Speed}#{bots[i].Mode}#");
            }
            sr.Close();
        }

        static void Title(string title)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine($"   {title}");
            Console.WriteLine("=====================================\n");
        }

        static void Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║        ROBOT MANAGER 2.0     ║");
            Console.WriteLine("╠══════════════════════════════╣");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║  [1] Lits Robots             ║");   
            Console.WriteLine("║  [2] Change Position         ║");
            Console.WriteLine("║  [3] Modify Charge           ║");
            Console.WriteLine("║  [4] Low-Battery Robots      ║");
            Console.WriteLine("║  [5] New Robot               ║");
            Console.WriteLine("║  [0] Quit                    ║");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════╝");

        
            Console.ResetColor();
            Console.Write("Pick an option: ");
        }
        static void Main(string[] args)
        {
            string path = "robotlist.txt";
            Mobilrobot[] bots = objectify(path);
            int choice;
          
            do
            {
                Menu();
                
               choice = int.Parse(Console.ReadLine());

                switch(choice)

                {

                    case 0:
                        Title("Bye-Bye");
                        Console.Write("Quitting");
                        for (int i = 0; i < 5; i++)
                        {
                            Thread.Sleep(300);
                            Console.Write(" .");
                        }
                        Console.ResetColor();
                        break;


                    case 1:
                        Title("List Robots ");
                        
                        list(bots);
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    case 2:
                        Title("Change Position");
                        Console.WriteLine("Please provide the robot's ID");
                        string id = Console.ReadLine();
                        int index = search(bots, id);
                        if( index == -1)
                        {
                            Console.WriteLine("Robot not found (No such ID)");
                        }
                        else
                        {
                            Console.WriteLine("Move X?");
                            int deltax = int.Parse(Console.ReadLine());
                            Console.WriteLine("Move Y");
                            int deltay = int.Parse(Console.ReadLine());
                            bots[index].changePos(deltax, deltay);
                            list(bots);
                            Console.ReadKey();
                        }
                        break;

                    case 3:
                       Title("Modify Charge");
                        Console.WriteLine("Please provide the robot's ID");
                          id = Console.ReadLine();
                         index = search(bots, id);
                        if (index == -1)
                        {
                            Console.WriteLine("Robot not found (No such ID)");
                        }
                        else
                        {
                            Console.WriteLine("Please provide the new value of charge");
                            int new_charge = int.Parse(Console.ReadLine());
                            bots[index].Charge = new_charge;
                            list(bots);
                            Console.ReadKey();
                        }
                        break;

                    case 4:
                        Title(" Low-Battery Robots  ");
                        for (int i = 0; i < bots.Length; i++)
                        {
                            if (bots[i].Charge < 20)
                            {
                                Console.WriteLine(bots[i].Display());
                                Console.WriteLine($"{bots[i].Id} - {bots[i].Model}" +
                                    $" new mode? ");
                                string new_Mode = Console.ReadLine();
                                bots[i].Mode = new_Mode;
                                Console.WriteLine($"{bots[i].Id} - {bots[i].Model}" +
                                   $" new mode is {new_Mode}");
                                Console.ReadKey();
                                Write(bots, path);
                                
                            }
                           
                        }
                        break;
                    case 5:
                        Title(" New Robot ");
                        Console.WriteLine("New Robot's ID?");
                        string new_ID = Console.ReadLine();
                        Console.WriteLine("New Robot's Model?");
                        string new_model = Console.ReadLine();
                        Console.WriteLine("New Robot's Speed?");
                        double new_speed = double.Parse(Console.ReadLine());
                        Console.WriteLine("New Robot's default mode?");
                        string new_mode = Console.ReadLine();






                        StreamWriter sw = new StreamWriter(path,true);
                        sw.WriteLine($"{new_ID}#{new_model}#{new_speed}#{new_mode}");
                        sw.Close();
                        bots = objectify(path);
                        Console.WriteLine("New Robot added!");
                        break;
                    default:
                        Console.WriteLine("Theres no such option");

                        break;

                        

                }

            } while (choice != 0);
           
        }
    }
}
