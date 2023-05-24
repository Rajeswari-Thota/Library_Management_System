using Library_Management;
using Moq;
namespace Library_Tests
{
    public class LibraryTests
    {
        [Test]
        public void IsLogin_SuccessfullWhenTrueCredentialsAreGiven_ReturnsTrue()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.IsLogin("raji","raji@123")).Returns(true);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.IsLogin("raji","raji@123");
            ////Assert
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void IsLogin_SuccessfullWhenFalseCredentialsAreGiven_ReturnsFalse()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.IsLogin("raji", "raji")).Returns(false);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.IsLogin("raji","raji");
            ////Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddStudent_WhenRecordInserted_ReturnsNoOfRowsAffected()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.AddStudent("Harry Potter", "EEE")).Returns(1);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.AddStudent("Harry Potter", "EEE");
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void ViewStudent_WhenGetRecord_ReturnsTrue()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.ViewStudent(101)).Returns(true);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            var result = service.ViewStudent(101);
            Assert.That(result, Is.True);
        }
        [Test]
        public void ViewStudent_WhenDoesNotGetRecord_ReturnsFalse()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.ViewStudent(1)).Returns(false);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            var result = service.ViewStudent(1);
            Assert.That(result, Is.False);

        }
        [Test]
        public void ViewAllStudents_WhenCalled_ReturnsTrue()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.ViewAllStudents()).Returns(true);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            var result = service.ViewAllStudents();
            Assert.That(result, Is.True);
        }
        [Test]  
        public void UpdateStudent_WhenRecordUpdated_ReturnsNoOfRowsAffected()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.UpdateStudent(1,"Harry Potter", "EEE")).Returns(1);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.UpdateStudent(1, "Harry Potter","EEE");
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void RemoveStudent_WhenCalled_ReturnsNoOfRowsAffected()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            studentrepo.Setup(x => x.RemoveStudent(103)).Returns(1);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.RemoveStudent(103);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddBook_WhenRecordInserted_ReturnsNoOfRowsAffected()
        {
            ////Arrange
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            bookrepo.Setup(x => x.AddBook("Harry Potter", "Author", 1998,3)).Returns(1);
            var service = new LibraryService(bookrepo.Object,studentrepo.Object,transactionrepo.Object);
            ////Act
            var result = service.AddBook("Harry Potter", "Author", 1998, 3);
            ////Assert
            Assert.That(result, Is.EqualTo(1));

        }
        [Test]
        public void ViewBook_WhenCalled_IfFoundReturnsTrue()
        {
            ////Arrange
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            bookrepo.Setup(x => x.ViewBook("martin")).Returns(true);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.ViewBook("martin");
            ////Assert
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void ViewAllBooks_WhenCalled_ReturnsTrue()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            bookrepo.Setup(x => x.ViewAllBooks()).Returns(true);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            var result = service.ViewAllBooks();
            Assert.That(result, Is.True);
        }
        [Test]
        public void UpdateBook_WhenRecordUpdated_ReturnsNoOfRowsAffected()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            bookrepo.Setup(x => x.UpdateBook(1,"Python", "Author", 2003, 3)).Returns(1);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.UpdateBook(1, "Python", "Author", 2003, 3);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void RemoveBook_WhenCalled_ReturnsNoOfRowsAffected()
        {
            var bookrepo = new Mock<IBookRepository>();
            var studentrepo = new Mock<IStudentRepository>();
            var transactionrepo = new Mock<ITransactionRepository>();
            bookrepo.Setup(x => x.RemoveBook(1)).Returns(1);
            var service = new LibraryService(bookrepo.Object, studentrepo.Object, transactionrepo.Object);
            ////Act
            var result = service.RemoveBook(1);
            ////Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}