using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    public interface IStudentRepository
    {
        bool IsLogin(string username, string password);
        int AddStudent(string name, string dept);
        bool ViewStudent(int roll);
        bool ViewAllStudents();
        int UpdateStudent(int roll, string name, string dept);
        int RemoveStudent(int roll);        
    }
}
