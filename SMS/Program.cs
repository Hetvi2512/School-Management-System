using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SMS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Student Management System");
            mainprogram:
            Console.Clear();
            Console.WriteLine("Select from the given options :");
            Console.WriteLine("1.Teacher Registration   2.Teacher login  3.Student login");
            int c = Convert.ToInt32(Console.ReadLine());

            try
            {


                switch (c)
                {

                    case 1:
                        try
                        {

                            Console.WriteLine("Enter your Name");
                            string p_name = Console.ReadLine();

                            Console.WriteLine("Enter your Password");
                            string p_pass = TurnToStars();
                            if (p_pass.Length < 8)
                            {
                                throw new PassLengthNotValidException("Password should be more than 8 character");

                            }

                            Console.WriteLine("Enter your City");
                            string p_city = Console.ReadLine();
                            Console.WriteLine("Enter your Email");
                            string p_email = Console.ReadLine();
                            Console.WriteLine("Enter your Contact Number");
                            string p_contact = Console.ReadLine();
                            teacher t1 = new teacher(p_name, p_pass, p_city, p_email, p_contact);
                            t1.selfregistration();

                        }


                        catch (PassLengthNotValidException e)
                        {
                            Console.WriteLine(e.Message);


                        }
                        break;



                    case 2:

                        Console.WriteLine("enter user Id and Password to login");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter password");
                        string pass = TurnToStars();
                        teacher t = new teacher();

                        int j = t.login(id, pass);
                        if (j == 3)
                        {
                            Console.WriteLine("Logged in successfully!");
                            Console.WriteLine("Enter :");

                            Console.WriteLine("1.To Register new student   2.Update the existing Student    3.Update Your Details    4.Delete Student    5.Delete your ID");
                            int x = int.Parse(Console.ReadLine());
                            if (x == 1)
                            {
                                t.registration();
                            }
                            else if (x == 2)
                            {
                                Console.WriteLine("Enter student ID to edit details");
                                int stid = int.Parse(Console.ReadLine());

                                t.UpdateStudent(stid);
                            }
                            else if (x == 3)
                            {
                                t.Update_teacher(id);
                            }
                            else if (x == 4)
                            {
                        Console.WriteLine("Enter Student ID to delete: ");
                        int did = int.Parse(Console.ReadLine());
                                t.DeleteStudent(did);
                }
                            else
                            {
                                t.DeleteTeacher(id);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed");

                        }
                        break;


                    case 3:
                        Console.WriteLine("enter user Id and Password to login");
                        int sid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter password");
                        string spass = TurnToStars();
                        student o = new student();
                        int i = o.login(sid, spass);
                        if (i == 1)
                        {
                            Console.WriteLine("Logged in successfully!");

                        }
                        else
                        {
                            Console.WriteLine("Incorrect ID/Password!");

                        }
                        break;
                    

                }
            }
            catch (SqlException e)
            {

                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press Y to continue or any other key to exit");
            char inp = char.Parse(Console.ReadLine());
            if (inp == 'Y' || inp == 'y')
                goto mainprogram;




        }

        public static string TurnToStars()
        {
            string pass = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    pass += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(pass))
                    {
                        // remove one character from the list of password characters
                        pass = pass.Substring(0, pass.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            return pass;
        }
    }
}