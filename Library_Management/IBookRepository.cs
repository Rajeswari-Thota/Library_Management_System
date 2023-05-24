using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    public interface IBookRepository
    {
        int AddBook(string title, string author, int year, int quantity);
        bool ViewBook(string author);
        bool ViewAllBooks();
        int UpdateBook(int id, string title, string author, int year, int quantity);
        int RemoveBook(int id);
    }
}
