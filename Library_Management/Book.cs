using System;
using System.Data.SqlClient;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    public class Book:IBookRepository
    {
        
        SqlConnection con = new SqlConnection("server=IN-8JRQ8S3;database=library;Integrated Security=true");    
        
        public  int AddBook(string title,string author, int year,int quantity)
        {
            try
            { 
                con.Open();
                if (quantity < 0)
                {
                    throw new Exception(" Quantity should be greater than or equal to 0");
                }
                SqlCommand cmd = new SqlCommand("insert into Book values(@Title,@Author,@Published_year,@Quantity)", con);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Published_year", year);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                int res=cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[Green] Book Added Successfully[/]");
                con.Close();
                return res;

            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[Red]An error occurred while adding a book: " + ex.Message + "[/]");
                con.Close() ;
                return 0;
            }
        }
        public bool ViewBook(string author)
        {
            con.Open();
            SqlCommand cmd=new SqlCommand($"select * from Book where Author='{author}'", con);
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
        public bool ViewAllBooks()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"select * from Book", con);
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
        public  int UpdateBook(int id,string title, string author, int year, int quantity)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"update Book set Title=@Title,Author=@Author,Published_year=@Published_year,Quantity=@Quantity where Id='{id}'", con);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Published_year", year);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                if (quantity < 0)
                {
                    throw new Exception(" Quantity should be greater than or equal to 0");
                }
                int updated=cmd.ExecuteNonQuery();
                if (updated > 0)
                {
                    AnsiConsole.MarkupLine("[Green] Book Updated Successfully[/]");
                    con.Close();
                    return updated;
                }
                else 
                { 
                    AnsiConsole.MarkupLine("[red] No Book was found with an entered Id[/]");
                    con.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[Red]An error occurred while updating a book: " + ex.Message + "[/]");
                con.Close() ;
                return 0;
            }

        }
        public  int RemoveBook(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"delete from book where Id='{id}'", con);
            int deleted=cmd.ExecuteNonQuery();
            if (deleted > 0)
            {
                AnsiConsole.MarkupLine("[Green] Book Deleted Successfully[/]");
                con.Close();
                return deleted;
            }
            else
            {
                AnsiConsole.MarkupLine("[red] No Book was found with an entered Id[/]");
                con.Close ();
                return 0;
            }
        }

    }
}
