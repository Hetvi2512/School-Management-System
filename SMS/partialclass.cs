using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SMS
{
    partial class teacher : Person
    {
   public void selfregistration()
        {

            try
            {

                SqlCommand cmd1 = new SqlCommand("insert into teacher(p_name,p_pass,p_city,p_email,p_contact)values(@p_name,@p_pass,@p_city,@p_email,@p_contact)", con);
                cmd1.Parameters.AddWithValue("@p_name", p_name);
                cmd1.Parameters.AddWithValue("@p_pass", p_pass);
                cmd1.Parameters.AddWithValue("@p_city", p_city);
                cmd1.Parameters.AddWithValue("@p_email", p_email);
                cmd1.Parameters.AddWithValue("@p_contact", p_contact);
                con.Open();
                Console.WriteLine("***");
                int i = cmd1.ExecuteNonQuery();
                Console.WriteLine("Registered Successfully!");
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

    }
}