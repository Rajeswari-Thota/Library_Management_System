using System.Data.SqlClient;
using System.Net;
using System.Xml.Linq;
using Spectre.Console;
using Unity;

namespace Library_Management
{   
    public class Program
    {
        private readonly ILibraryService _libraryService;
        public Program(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        static void Main(string[] args)
        {
            var container=new UnityContainer();
            container.RegisterType<IBookRepository,Book>();
            container.RegisterType<IStudentRepository,Student>();
            container.RegisterType<ITransactionRepository,Transaction>();
            container.RegisterType<ILibraryService,LibraryService>();
            ILibraryService libraryService = container.Resolve<ILibraryService>();
            Program program = new Program(libraryService);
            string username = null;
            string password = null;
            AnsiConsole.Write(new FigletText("Library").Centered().Color(Color.Blue));
            AnsiConsole.MarkupLine("[red]Please Login to Avail Services![/]");
            if (program._libraryService.IsLogin(username,password))
            {
                AnsiConsole.MarkupLine("[DarkMagenta] WELCOME TO LIBRARY-MANAGEMENT-APP[/]");
                while (true)
                {                    
                    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("[green]Select your option[/]?")
                            .AddChoices(new[] { "Student Management", "Book Management", "Issue Book", "Return Book","View All Issues" ,"Exit" }));
                    if (choice == "Exit")
                        break;

                    switch (choice)
                    {
                        case "Student Management":
                            AnsiConsole.MarkupLine("[DarkCyan] STUDENT-MANAGEMENT[/]");
                            while (true)
                            {
                                var schoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                        .Title("[green]Select your option[/]?")
                                        .AddChoices(new[] { "Add Student", "View Student by Roll Number", "View All Students", "Update Student", "Remove Student", "Go Back" }));

                                if (schoice == "Go Back")
                                    break;

                                switch (schoice)
                                {
                                    case "Add Student":
                                        string name = AnsiConsole.Ask<string>("[yellow] Enter Student Name:[/] ");
                                        string dept = AnsiConsole.Ask<string>("[yellow] Enter Department:[/] ");
                                        program._libraryService.AddStudent(name, dept);
                                        break;
                                    case "View Student by Roll Number":
                                        int roll = AnsiConsole.Ask<int>("[yellow] Enter Student Roll Number: [/]");
                                        program._libraryService.ViewStudent(roll);
                                        break;
                                    case "View All Students":
                                        program._libraryService.ViewAllStudents();
                                        break;
                                    case "Update Student":
                                        roll = AnsiConsole.Ask<int>("[Yellow]Enter Roll_No of the Student to view Details:[/] ");
                                        name = AnsiConsole.Ask<string>("[yellow] Enter Student Name:[/] ");
                                        dept = AnsiConsole.Ask<string>("[yellow] Enter Department:[/] ");                                        
                                        program._libraryService.UpdateStudent(roll, name, dept);
                                        break;
                                    case "Remove Student":
                                        roll = AnsiConsole.Ask<int>("[Yellow]Enter Roll Number to Delete Student:[/] ");
                                        program._libraryService.RemoveStudent(roll);
                                        break;
                                }
                            }
                            break;
                        case "Book Management":
                            AnsiConsole.MarkupLine("[DarkCyan] BOOK-MANAGEMENT[/]");
                            while (true)
                            {                                
                                var bchoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                        .Title("[green]Select your option[/]?")
                                        .AddChoices(new[] { "Add Book", "View Book by Author Name", "View All Books", "Update Book", "Remove Book", "Go Back" }));

                                if (bchoice == "Go Back")
                                    break;

                                switch (bchoice)
                                {
                                    case "Add Book":
                                        string title = AnsiConsole.Ask<string>("[yellow] Enter Book Title: [/]");
                                        string author = AnsiConsole.Ask<string>("[yellow] Enter Author of the Book:[/] ");
                                        int year = AnsiConsole.Ask<int>("[yellow] Enter Published Year:[/] ");
                                        int quantity = AnsiConsole.Ask<int>("[yellow] Enter Quantity of the Book:[/] ");
                                        program._libraryService.AddBook(title,author,year,quantity);
                                        break;
                                    case "View Book by Author Name":
                                        author = AnsiConsole.Ask<string>("[Yellow]Enter an Author Name to View Book:[/]");
                                        program._libraryService.ViewBook(author);
                                        break;
                                    case "View All Books":
                                        program._libraryService.ViewAllBooks();
                                        break;
                                    case "Update Book":
                                        int id = AnsiConsole.Ask<int>("[Yellow]Enter an Id to Update Book:[/] ");
                                        title = AnsiConsole.Ask<string>("[yellow] Enter Book Title: [/]");
                                        author = AnsiConsole.Ask<string>("[yellow] Enter Author of the Book:[/] ");
                                        year = AnsiConsole.Ask<int>("[yellow] Enter Published Year:[/] ");
                                        quantity = AnsiConsole.Ask<int>("[yellow] Enter Quantity of the Book:[/] ");
                                        if (quantity < 0)
                                        {
                                            throw new Exception("Invalid Quantity. Roll Number should be greater than or equal to 0.");
                                        }
                                        program._libraryService.UpdateBook(id,title,author,year,quantity);
                                        break;
                                    case "Remove Book":
                                        id = AnsiConsole.Ask<int>("[Yellow]Enter an Id to Delete Book:[/] ");
                                        program._libraryService.RemoveBook(id);
                                        break;
                                }
                            }
                            break;
                        case "Issue Book":
                            
                            int studentroll = AnsiConsole.Ask<int>("[yellow] Enter Student Roll Number: [/]");
                            int bookid = AnsiConsole.Ask<int>("[Yellow]Enter Book Id :[/] ");
                            program._libraryService.Issuebooks(bookid, studentroll);
                            break;
                        case "Return Book":
                            
                            int rstudentroll = AnsiConsole.Ask<int>("[yellow] Enter Student Roll Number: [/]");
                            int rbookid = AnsiConsole.Ask<int>("[Yellow]Enter Book Id :[/] ");
                            program._libraryService.Returnbooks(rbookid, rstudentroll);
                            break;
                        case "View All Issues":
                            {
                                program._libraryService.AllIssues();
                                break;
                            }
                    }
                }
            }
        }    
    }
}