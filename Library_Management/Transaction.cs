using System;
using System.Data.SqlClient;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Transactions;
using System.Data.Common;

namespace Library_Management
{
    public class Transaction:ITransactionRepository
    { 
        SqlConnection con = new SqlConnection("server=IN-8JRQ8S3;database=library;Integrated Security=true");
        public int Issuebooks(int bookid,int studentroll)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                SqlCommand checkbookcommand = new SqlCommand($"select Quantity from Book where Id={bookid}", con, tran);
                int bookQuantity= Convert.ToInt32(checkbookcommand.ExecuteScalar());
                if (bookQuantity <= 0)
                {
                    AnsiConsole.MarkupLine("[red]Book not available[/]");
                    tran.Rollback();
                    con.Close();
                    return 0;
                }
                SqlCommand checkstudentcommand = new SqlCommand($"SELECT COUNT(*) FROM Issues WHERE Student_roll_no = {studentroll} AND Issued_book_id = {bookid} AND Returned_Date IS NULL", con, tran);
                int existingIssuesCount = Convert.ToInt32(checkstudentcommand.ExecuteScalar());

                if (existingIssuesCount > 0)
                {
                    AnsiConsole.MarkupLine("[red]The same book cannot be issued to the same student[/]");
                    tran.Rollback();
                    con.Close();
                    return 0;
                }
                SqlCommand updatebookcommand = new SqlCommand($"Update Book set Quantity=Quantity-1 where Id={bookid}", con, tran);
                updatebookcommand.ExecuteNonQuery();
                
                SqlCommand updateTransaction = new SqlCommand($"insert into Issues values(@Student_roll_no,@Issued_book_id,GETDATE(),@Returned_Date)", con, tran);
                updateTransaction.Parameters.AddWithValue("@Student_roll_no", studentroll);
                updateTransaction.Parameters.AddWithValue("@Issued_book_id", bookid);
                updateTransaction.Parameters.AddWithValue("@Returned_date", DBNull.Value);
                int issued=updateTransaction.ExecuteNonQuery();

                tran.Commit();
                AnsiConsole.MarkupLine("[green]Book issued successfully![/]");
                con.Close();
                return issued;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine("An error occurred while issuing the book:" + ex.Message);
                con.Close();
                return 0;
            }
        }
        public int Returnbooks(int rbookid,int rstudentroll)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {                
                SqlCommand checkstudentcommand = new SqlCommand($"select Issued_book_id from Issues where Student_roll_no={rstudentroll} and Issued_book_id = {rbookid} ", con, tran);
                int bookid = Convert.ToInt32((checkstudentcommand.ExecuteScalar()));
                if (bookid!=rbookid)
                {
                    AnsiConsole.MarkupLine("[red]Student doesnot have book with entered book id to return[/]");
                    tran.Rollback();
                    con.Close();
                    return 0;
                }
                SqlCommand updatebookcommand = new SqlCommand($"Update Book set Quantity=Quantity+1 where Id={rbookid}", con,tran);
                updatebookcommand.ExecuteNonQuery();
                SqlCommand updateTransaction = new SqlCommand($"UPDATE Issues SET Returned_Date = GETDATE() WHERE Student_roll_no = @Student_roll_no AND Issued_book_id = @BookId", con, tran);
                updateTransaction.Parameters.AddWithValue("@Student_roll_no", rstudentroll);
                updateTransaction.Parameters.AddWithValue("@BookId", rbookid);
                int returned=updateTransaction.ExecuteNonQuery();
                tran.Commit();
                AnsiConsole.MarkupLine("[green]Book returned successfully![/]");
                con.Close();
                return returned;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine("An error occurred while returning the book"+ex.Message);
                con.Close();
                return 0;
            }
        }
        public bool AllIssues()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Student.Roll_no,Student.Student_Name,Book.Id,Book.Title,Book.Author,Issues.Issued_Date,Issues.Returned_Date from Issues join Student on Issues.Student_roll_no = Student.Roll_no join Book on Issues.Issued_book_id = Book.Id",con);
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
    }
}

