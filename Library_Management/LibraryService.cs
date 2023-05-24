using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    public class LibraryService:ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITransactionRepository _transactionRepository;
        public LibraryService(IBookRepository bookRepository, IStudentRepository studentRepository, ITransactionRepository transactionRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _transactionRepository = transactionRepository;
        }
        
        public int AddBook(string title, string author, int year, int quantity)
        {
            return _bookRepository.AddBook(title, author, year, quantity);
        }
        public bool ViewBook(string author)
        {
            return _bookRepository.ViewBook(author);
        }
        public bool ViewAllBooks()
        {
            return _bookRepository.ViewAllBooks();
        }
        public int  UpdateBook(int id, string title, string author, int year, int quantity)
        {
            return _bookRepository.UpdateBook(id, title, author, year, quantity);
        }
        public int RemoveBook(int id)
        {
            return _bookRepository.RemoveBook(id);
        }
        public bool IsLogin(string username, string password)
        {
            return _studentRepository.IsLogin(username, password);
        }
        public int AddStudent(string name, string dept)
        {
            return _studentRepository.AddStudent(name, dept);
        }
        public bool ViewStudent(int roll)
        {
            return _studentRepository.ViewStudent(roll);
        }
        public bool ViewAllStudents()
        {
            return _studentRepository.ViewAllStudents();
        }
        public int UpdateStudent(int roll, string name, string dept)
        {
            return _studentRepository.UpdateStudent(roll, name, dept);
        }
        public int RemoveStudent(int roll)
        {
            return _studentRepository.RemoveStudent(roll);
        }
        public int Issuebooks(int bookid, int studentroll)
        {
            return _transactionRepository.Issuebooks(bookid, studentroll);
        }
        public int Returnbooks(int rbookid, int rstudentroll)
        {
            return _transactionRepository.Returnbooks(rbookid, rstudentroll);
        }
        public bool AllIssues()
        {
            return _transactionRepository.AllIssues();
        }
    }
}
