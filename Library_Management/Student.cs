using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    public class Student:IStudentRepository
    {
        SqlConnection con = new SqlConnection("server=IN-8JRQ8S3;database=library;Integrated Security=true");
        public bool IsLogin(string username,string password)
        {
            while (true)
            {
                con.Open();
                username = AnsiConsole.Ask<string>("[yellow]Enter username:[/]");
                password = AnsiConsole.Ask<string>("[yellow]Enter password:[/]");
                SqlCommand logincommand = new SqlCommand($"select * from Login_Details where username='{username}' and password='{password}'", con);
                SqlDataReader dr = logincommand.ExecuteReader();
                if (dr.Read())
                {
                    AnsiConsole.MarkupLine("[Green]Login Successful[/]");
                    con.Close();
                    return true;
                }
                else
                {
                    AnsiConsole.MarkupLine("[Red]Login Failed[/]");
                    var retry = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("[yellow]Retry login?[/]")
                            .AddChoices(new[] { "Yes", "No" }));
                    if (retry == "No")
                        
                        break;
                } 
                con.Close();
            }            
            return false;
        }
        public int AddStudent(string name,string dept)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Student values(@Student_Name,@Department)",con);
                cmd.Parameters.AddWithValue("@Student_Name", name);
                cmd.Parameters.AddWithValue("@Department", dept);
                int res = cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[Green] Student Added Successfully[/]");
                con.Close();
                return res;
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[Red]An error occurred while Adding a student [/]");
                con.Close();
                return 0;
            }           
        }
        public bool ViewStudent(int roll)
        {
            try
            {
                con.Open();
                if(roll <= 0) 
                {
                    throw new Exception("Entered roll no should be greater than 0");
                }
                SqlCommand cmd = new SqlCommand($"select * from Student where Roll_no={roll}",con);
                SqlDataReader dr = cmd.ExecuteReader();
                var table = new Table();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    table.AddColumn(dr.GetName(i));
                }
                string[] arr = new string[dr.FieldCount];
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        arr[i] = dr.GetValue(i).ToString();
                    }
                    table.AddRow(arr);
                }
                AnsiConsole.Write(table);
                dr.Close();
                con.Close();
                return table.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[Red]An error occurred while fetching  a student: " + ex.Message + "[/]");
                con.Close();
                return false;
            }
        }
        public bool ViewAllStudents()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"select * from Student",con);
            SqlDataReader dr = cmd.ExecuteReader();
            var table = new Table();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                table.AddColumn(dr.GetName(i));
            }
            string[] arr = new string[dr.FieldCount];
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    arr[i] = dr.GetValue(i).ToString();
                }
                table.AddRow(arr);
            }
            AnsiConsole.Write(table);
            dr.Close();
            con.Close();
            return table.Rows.Count > 0;
        }
        public int UpdateStudent(int roll, string name, string dept)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"update Student set Student_Name=@Student_Name,Department=@Department where Roll_no='{roll}'", con);
                cmd.Parameters.AddWithValue("@Student_Name", name);
                cmd.Parameters.AddWithValue("@Department", dept);
                int updated = cmd.ExecuteNonQuery();
                if (updated > 0)
                {
                    AnsiConsole.MarkupLine("[Green] Student Updated Successfully[/]");
                    con.Close();
                    return updated;
                }
                else 
                { 
                    AnsiConsole.MarkupLine("[red] No Student was found with an entered Roll Number[/]");
                    con.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[Red]An error occurred while Updating a student: " + ex.Message + "[/]");
                con.Close();
                return 0;
            }
        }
        public int RemoveStudent(int roll)
        {
            try              
            {
                con.Open() ;
                SqlCommand cmd = new SqlCommand($"delete from Student where Roll_no='{roll}'",con);
                int deleted = cmd.ExecuteNonQuery();
                if (deleted > 0)
                {
                    AnsiConsole.MarkupLine("[Green] Student Deleted Successfully[/]");
                    con.Close();
                    return deleted;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red] No Student was found with an entered roll number [/]");
                    con.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[Red]An error occurred while Deleting a student: " + ex.Message + "[/]");
                con.Close();
                return 0;
            }

        }
    }
}

