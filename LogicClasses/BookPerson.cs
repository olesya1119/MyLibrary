using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.LogicClasses
{
    public class BookPerson
    {
        private string personLogin;
        private int bookId;
        private DateTime dateOfIssue;
        private DateTime deadline;
        
       
        public string PersonLogin { get { return personLogin; } set { personLogin = value; } }
        public int BookId { get { return bookId; } set {  bookId = value; } }
        public DateTime DateOfIssue { get {  return dateOfIssue; } set {  dateOfIssue = value; } }
        public DateTime Deadline { get { return deadline; } set {  deadline = value; } }

        public BookPerson(string personLogin, int bookId, DateTime dateOfIssue, DateTime deadline)
        {
            
            this.personLogin = personLogin;
            BookId = bookId;
            DateOfIssue = dateOfIssue;
            Deadline = deadline;
        }

        public BookPerson() { }

        public BookPerson( string personLogin, int bookId, DateTime dateOfIssue, int days)
        {
           
            this.personLogin = personLogin;
            BookId = bookId;
            DateOfIssue = dateOfIssue;
            Deadline = dateOfIssue.AddDays(days);
        }

        public override bool Equals(object obj)
        {
            BookPerson bookPerson = obj as BookPerson;
            if (personLogin == bookPerson.personLogin && bookId == bookPerson.bookId)
            {
                return true;
            }
            return false;
        }

    }
}
