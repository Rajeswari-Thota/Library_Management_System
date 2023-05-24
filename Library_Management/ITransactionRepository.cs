using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    public interface ITransactionRepository
    {
        int Issuebooks(int bookid, int studentroll);
        int Returnbooks(int rbookid, int rstudentroll);
        bool AllIssues();
    }
}
