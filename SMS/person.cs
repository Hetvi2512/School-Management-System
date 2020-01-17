using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SMS
{
    abstract class Person
    {
        protected string p_name;
        protected string p_pass;
        protected string p_contact;
        protected string p_city;
        protected string p_email;
       public abstract int login(int id, string pass);

      public virtual void showdetails(int shid)
        {
        }

    }

    interface Iregis
    {
        void registration();

    }

   sealed class student : Person
    {
        public SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\Desktop\Sem 6\.Net Framework\Extra\SMS\SMS\school.mdf;Integrated Security=True");

 
        
        int s_enroll;
      
        public int Senroll
        {
            get { return s_enroll; }
            set { s_enroll = value; }

        }

        public string Spass
        {
            get { return p_pass; }
            set { p_pass = value; }

        }



        public override void showdetails(int shid)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE s_enroll = @s_enroll", con);
            con.Open();
            command.Parameters.Add(new SqlParameter("@s_enroll", shid));
            SqlDataReader dr0 = command.ExecuteReader();
            using (dr0)
            {

                while (dr0.Read())
                {
                    Console.WriteLine("ID : " + dr0["s_enroll"].ToString());
                    Console.WriteLine(dr0["p_name"].ToString());
                    Console.WriteLine(dr0["p_email"].ToString());
                    Console.WriteLine(dr0["p_contact"].ToString());
                    Console.WriteLine(dr0["p_city"].ToString());
                }
                con.Close();
            }
        }
        public override int login(int id, string pass)
        {

            SqlCommand cmd = new SqlCommand("select * from student where s_enroll=@s_enroll and p_pass=@p_pass", con);
            cmd.Parameters.AddWithValue("@s_enroll", id);
            cmd.Parameters.AddWithValue("@p_pass", pass);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Console.WriteLine("Logged in successfully!");
                con.Close();
                Console.WriteLine("Press 1 to view your details or any other key to go to main menu");
                int inp = int.Parse(Console.ReadLine());
                if (inp == 1)
                {
                    showdetails(id);  
                    Console.WriteLine("\nPress any key to continue ");
                    Console.ReadKey();

                    con.Close();
                    return 1;
                }

                else
                {
                    return 1;
                }
            }
            else
            {
                con.Close();
                return 0;
            }


            // con.Close();   
        }

       
    }

  partial  class teacher : Person, Iregis
    {
        public SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\Desktop\Sem 6\.Net Framework\Extra\SMS\SMS\school.mdf;Integrated Security=True");
        public teacher()
        {
        }
 
        int t_enroll;
      
        public int Tenroll
        {
            get { return t_enroll; }
            set { t_enroll = value; }

        }

        public string Spass
        {
            get { return p_pass; }
            set { p_pass = value; }

        }
    // partial void selfregistration();
    
        public void registration()
        {
            
            
            Console.WriteLine("Enter Student Name");
            string p_name = Console.ReadLine();
            try
            {
                Console.WriteLine("Enter Student Password");
                string p_pass = Console.ReadLine();
                if (p_pass.Length < 8)
                {
                    throw new PassLengthNotValidException("Password should be more than 8 character");

                }

                Console.WriteLine("Enter Student City");
                string p_city = Console.ReadLine();
                Console.WriteLine("Enter Student Email");
                string p_email = Console.ReadLine();
                Console.WriteLine("Enter Student Contact Number");
                string p_contact = Console.ReadLine();

                SqlCommand cmd1 = new SqlCommand("insert into student(p_name,p_pass,p_city,p_email,p_contact)values(@p_name,@p_pass,@p_city,@p_email,@p_contact)", con);
                cmd1.Parameters.AddWithValue("@p_name", p_name);
                cmd1.Parameters.AddWithValue("@p_pass", p_pass);
                cmd1.Parameters.AddWithValue("@p_city", p_city);
                cmd1.Parameters.AddWithValue("@p_email", p_email);
                cmd1.Parameters.AddWithValue("@p_contact", p_contact);
                con.Open();
                Console.WriteLine("***");
                int i = cmd1.ExecuteNonQuery();
                Console.WriteLine("Student Registered Successfully!");
                Console.WriteLine("New ID for registered user : ");
                SqlCommand command = new SqlCommand("SELECT MAX(s_enroll) FROM Student", con);

                SqlDataReader dr1= command.ExecuteReader();
                using (dr1)
                {

                    while (dr1.Read())
                    {
                        Console.WriteLine("ID : " + dr1[0].ToString());
                    
                    }
                }
                Console.WriteLine("Press 1 to login or any other key to exit.");
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    Console.WriteLine("enter user Id and Password to login");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("enter password");
                string pass = Console.ReadLine();
                teacher t = new teacher();

                int j = t.login(id, pass);
                if (j == 3)
                {
                    Console.WriteLine("Successfull Logged In");
                    t.selfregistration();


                }
                else
                {
                    Console.WriteLine("Failed");

                }
            }


            }
            catch (PassLengthNotValidException e)
            {
                Console.WriteLine(e.Message);


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();

            }

        }
      public teacher(string p_name,string p_pass, string p_city,string p_email,string p_contact)
                {
                    this.p_name = p_name;
                    this.p_pass = p_pass;
                    this.p_city = p_city;
                    this.p_email = p_email;
                    this.p_contact = p_contact;
                }
        public override int login(int id, string pass)
        {

            SqlCommand cmd = new SqlCommand("select * from teacher where t_enroll=@t_enroll and p_pass=@p_pass", con);
            cmd.Parameters.AddWithValue("@t_enroll",id);
            cmd.Parameters.AddWithValue("@p_pass", pass);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Close(); 
                return 3;
            }
            else
            {
                con.Close(); 
                return 0;
            }

           
        }

        public void UpdateStudent(int upid)
        {
            studentswitch:
            Console.WriteLine("Select the update category : ");
            Console.WriteLine("1.Name 2.City 3.Email 4.Contact");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter updated name : ");
                    string Name = Console.ReadLine();

                    SqlCommand cmd = new SqlCommand("update student set p_name=@p_name where s_enroll=@s_enroll", con);
                    cmd.Parameters.AddWithValue("@p_name", Name);
                    cmd.Parameters.AddWithValue("@s_enroll", upid);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();

                    break;
                case 2:
                    Console.WriteLine("Enter updated city : ");
                    string City = Console.ReadLine();

                    SqlCommand cmd1 = new SqlCommand("update student set p_city=@p_city where s_enroll=@s_enroll", con);
                    cmd1.Parameters.AddWithValue("@p_city", City);
                    cmd1.Parameters.AddWithValue("@s_enroll", upid);
                    con.Open();
                    i = cmd1.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();

                    break;
                case 3:
                    Console.WriteLine("Enter updated Email : ");
                    string Email = Console.ReadLine();

                    SqlCommand cmd2 = new SqlCommand("update student set p_email=@p_email where s_enroll=@s_enroll", con);
                    cmd2.Parameters.AddWithValue("@p_email", Email);
                    cmd2.Parameters.AddWithValue("@s_enroll", upid);
                    con.Open();
                    i = cmd2.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();

                    break;
                case 4:
                    Console.WriteLine("Enter updated Contact : ");

                    string Contact = Console.ReadLine();

                    SqlCommand cmd3 = new SqlCommand("update student set p_contact=@p_contact where s_enroll=@s_enroll", con);
                    cmd3.Parameters.AddWithValue("@p_contact", Contact);
                    cmd3.Parameters.AddWithValue("@s_enroll", upid);
                    con.Open();
                    i = cmd3.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();
                    break;
              
                default:
                    Console.WriteLine("Update Failed");
                    break;


            }
            Console.WriteLine("Press Y to update other details or any other key to exit");
            char studswit = char.Parse(Console.ReadLine());
            if (studswit == 'Y' || studswit == 'y')
                goto studentswitch;
        }

        public void DeleteStudent(int did)
        {
            SqlCommand cmd4 = new SqlCommand("DELETE FROM student where s_enroll=@s_enroll", con);
            cmd4.Parameters.AddWithValue("@s_enroll", did);
            con.Open();
            int di = cmd4.ExecuteNonQuery();
            if (di != 0)
            {
                Console.WriteLine("Student Deleted Successfully");

            }
            con.Close();
        }

        public void DeleteTeacher(int tid)
        {
            SqlCommand cmd5 = new SqlCommand("DELETE FROM teacher where t_enroll=@t_enroll", con);
            cmd5.Parameters.AddWithValue("@t_enroll", tid);
            con.Open();
            int ti = cmd5.ExecuteNonQuery();
            if (ti != 0)
            {
                Console.WriteLine("Teacher Deleted Successfully");

            }
            con.Close();
        }

        public void Update_teacher(int id)
        {
            startswitch:
            Console.WriteLine("Select the update category : ");
            Console.WriteLine("1.Name 2.City 3.Email 4.Contact");
            int input = int.Parse(Console.ReadLine());
        
            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter updated name : ");
                    string Name = Console.ReadLine();

                    SqlCommand cmd = new SqlCommand("update teacher set p_name=@p_name where t_enroll=@t_enroll", con);
                    cmd.Parameters.AddWithValue("@p_name", Name);
                    cmd.Parameters.AddWithValue("@t_enroll", id);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();

                    break;
                case 2:
                    Console.WriteLine("Enter updated city : ");
                    string City = Console.ReadLine();

                    SqlCommand cmd1 = new SqlCommand("update teacher set p_city=@p_city where t_enroll=@t_enroll", con);
                    cmd1.Parameters.AddWithValue("@p_city", City);
                    cmd1.Parameters.AddWithValue("@t_enroll", id);
                    con.Open();
                    i = cmd1.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();

                    break;
                case 3:
                    Console.WriteLine("Enter updated Email : ");
                    string Email = Console.ReadLine();

                    SqlCommand cmd2 = new SqlCommand("update teacher set p_email=@p_email where t_enroll=@t_enroll", con);
                    cmd2.Parameters.AddWithValue("@p_email", Email);
                    cmd2.Parameters.AddWithValue("@t_enroll", id);
                    con.Open();
                    i = cmd2.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();

                    break;
                case 4:
                    Console.WriteLine("Enter updated Contact : ");

                    string Contact = Console.ReadLine();

                    SqlCommand cmd3 = new SqlCommand("update teacher set p_contact=@p_contact where t_enroll=@t_enroll", con);
                    cmd3.Parameters.AddWithValue("@p_contact", Contact);
                    cmd3.Parameters.AddWithValue("@t_enroll", id);
                    con.Open();
                    i = cmd3.ExecuteNonQuery();
                    if (i != 0)
                    {
                        Console.WriteLine("Data Updated Successfully");

                    }
                    con.Close();

                    break;
                default:
                    Console.WriteLine("Update Failed");
                    break;


            }
            Console.WriteLine("Press Y to update other details or any other key to exit");
            char swit = char.Parse(Console.ReadLine());
            if (swit == 'Y' || swit == 'y')
                goto startswitch;



        }

        
    }
}
