using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    public interface ILibraryService
    {
        bool IsLogin(string username, string password);
        int AddBook(string title, string author, int year, int quantity);
        bool ViewBook(string author);
        bool ViewAllBooks();
        int UpdateBook(int id, string title, string author, int year, int quantity);
        int RemoveBook(int id);
        int AddStudent(string name, string dept);
        bool ViewStudent(int roll);
        bool ViewAllStudents();
        int UpdateStudent(int roll, string name, string dept);
        int RemoveStudent(int roll);
        int Issuebooks(int bookid, int studentroll);
        int Returnbooks(int rbookid, int rstudentroll);
        bool AllIssues();
    }
}
