using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MyLibrary.LogicClasses
{
    internal class DataBaseController
    {

        public static Person Login(string login, string heshPassword)
        {
            login = login.Trim();
            heshPassword = heshPassword.Trim();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));
            const string path = @"..\..\Data\Persons.xml";
            List<Person> persons;

            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                persons = xmlSerializer.Deserialize(writer) as List<Person>;
            }

            var resultPersons = from p in persons where login == p.Login && p.Password == heshPassword select p;  
            if (resultPersons.Any())
            {
                return resultPersons.FirstOrDefault();
            }
            else
            {
                throw new Exception("Логин или пароль введины неверно");
            }    
        }

        public static void RegistrationReader(Person p)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));
            const string path = @"..\..\Data\Persons.xml";
            List<Person> persons;

            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                persons = xmlSerializer.Deserialize(writer) as List<Person>;
            }
            persons.Add(p);
            
            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {

                xmlSerializer.Serialize(writer, persons);
            } 
        }

        public static void AddBook(string name, string autors, string tags, int quantityInLibrary, int quantityOfReaders)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            const string path = @"..\..\Data\Books.xml";
            List<Book> books;
            

            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                books = xmlSerializer.Deserialize(writer) as List<Book>;
            }
            Book book = new Book(books[-1].ID + 1,  name, autors, tags, quantityInLibrary, quantityOfReaders);
            books.Add(book);
            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {

                xmlSerializer.Serialize(writer, books);
            }
        }

       
        public static List<Book> FindBooks(string name, string autors, string tags)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            const string path = @"..\..\Data\Books.xml";
            List<Book> books;


            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                books = xmlSerializer.Deserialize(writer) as List<Book>;
            }
            List<Book> resultbooks = new List<Book> (from b in books where b.Name.ToLower().Contains(name.ToLower()) && b.Autors.ToLower().Contains(autors.ToLower()) && b.Tags.ToLower().Contains(tags.ToLower()) select b);

            return resultbooks;
        }

        public static Book FindBook(int ID)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Book>));
            const string path = @"..\..\Data\Books.xml";
            List<Book> books;


            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                books = xmlSerializer.Deserialize(writer) as List<Book>;
            }
            List<Book> resultbooks = new List<Book>(from b in books where ID == b.ID select b);
            return resultbooks[0];
        }

        public static List<Person> FindPersons(string firstname, string lastname, string pytronymic, string email, string phone, string login, int role)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));
            const string path = @"..\..\Data\Persons.xml";
            List<Person> persons;

            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                persons = xmlSerializer.Deserialize(writer) as List<Person>;
            }

            List<Person> resultpersons = new List<Person>(from p in persons where p.FirstName.ToLower().Contains(firstname.ToLower()) && p.LastName.ToLower().Contains(lastname.ToLower()) && 
                                                      p.Pytronymic.ToLower().Contains(pytronymic.ToLower()) && p.Phone.ToLower().Contains(phone.ToLower()) && p.Email.ToLower().Contains(email.ToLower()) &&
                                                      p.Login.ToLower().Contains(login.ToLower()) && role == p.IdRole select p);

            return resultpersons;
        }

        public static List<Book> FindPersonsBook(string personLogin)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BookPerson>));
            const string path = @"..\..\Data\BookPerson.xml";
            List<BookPerson> bookPersons = new List<BookPerson> { };
            List<Book> books = new List<Book> { };

         

            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                bookPersons = xmlSerializer.Deserialize(writer) as List<BookPerson>;
            }

            for (int i = 0; i < bookPersons.Count; i++)
            {
                if (bookPersons[i].PersonLogin == personLogin)
                {
                    books.Add(FindBook(bookPersons[i].BookId));
                }
            }

            return books;
        }

        public static BookPerson FindBookPerson(string personLogin, int bookID)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BookPerson>));
            const string path = @"..\..\Data\BookPerson.xml";
            List<BookPerson> bookPersons = new List<BookPerson> { };
            BookPerson bookPerson;



            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                bookPersons = xmlSerializer.Deserialize(writer) as List<BookPerson>;
            }

            bookPerson = (new List<BookPerson>(from bp in bookPersons where bp.PersonLogin == personLogin && bp.BookId == bookID select bp))[0];

            return bookPerson;
        }

        public static void AddBookPerson(Person person, Book book, string days) {

            //
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BookPerson>));
            const string path = @"..\..\Data\BookPerson.xml";

            if (!int.TryParse(days, out int num)) throw new Exception("Количество дней должно быть числом!");
            if (Convert.ToInt32(days) <= 0) throw new Exception("Количество дней должно быть больше 0!");

            
            BookPerson bookPerson = new BookPerson(person.Login, book.ID, DateTime.Now, Convert.ToInt32(days));
            List<BookPerson> bookPersons = new List<BookPerson> {};


            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                bookPersons = xmlSerializer.Deserialize(writer) as List<BookPerson>;
            }

            for (int i = 0; i < bookPersons.Count; i++)
            {
                if (bookPerson.Equals(bookPersons[i])) throw new Exception("У этого пользователя уже есть эта книга!");
            }
            

            
            XmlSerializer xmlSerializerBook = new XmlSerializer(typeof(List<Book>));
            const string pathBook = @"..\..\Data\Books.xml";
            List<Book> books;
            using (FileStream writer = new FileStream(pathBook, FileMode.OpenOrCreate))
            {
                books = xmlSerializerBook.Deserialize(writer) as List<Book>;
            }

            if (books[book.ID].QuantityInLibrary <= 0) throw new Exception("В библиотеке закончилась эти книга!");

            books[book.ID].QuantityInLibrary -= 1;
            books[book.ID].QuantityOfReaders += 1;
            using (FileStream writer = new FileStream(pathBook, FileMode.OpenOrCreate))
            {

                xmlSerializerBook.Serialize(writer, books);
            }

            bookPersons.Add(bookPerson);

            using (FileStream writer = new FileStream(path, FileMode.OpenOrCreate))
            {

                xmlSerializer.Serialize(writer, bookPersons);
            }
        }

        public static void ReturnBook(Person person, Book book)
        {
            BookPerson bookPerson = FindBookPerson(person.Login, book.ID);
            List<BookPerson> bookPersonsHistory, bookPersons;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BookPerson>));

            using (FileStream writer = new FileStream(@"..\..\Data\BookPerson.xml", FileMode.OpenOrCreate))
            {
                bookPersons = xmlSerializer.Deserialize(writer) as List<BookPerson>;
            }
            using (FileStream writer = new FileStream(@"..\..\Data\BookPersonHistory.xml", FileMode.OpenOrCreate))
            {
                bookPersonsHistory = xmlSerializer.Deserialize(writer) as List<BookPerson>;
            }


            for (int i = 0; i < bookPersons.Count; i++)
            {
                if (bookPerson.Equals(bookPersons[i]))
                {
                    bookPersonsHistory.Add(bookPerson);
                    bookPersons.RemoveAt(i);

                    XmlSerializer xmlBook = new XmlSerializer(typeof(List<Book>));
                    List<Book> books;
                    using (FileStream writer = new FileStream(@"..\..\Data\Books.xml", FileMode.OpenOrCreate))
                    {
                        books = xmlBook.Deserialize(writer) as List<Book>;
                    }
                    books[book.ID].QuantityInLibrary += 1;
                    books[book.ID].QuantityOfReaders -= 1;
                    using (FileStream writer = new FileStream(@"..\..\Data\Books.xml", FileMode.OpenOrCreate))
                    {
                        xmlBook.Serialize(writer, books);
                    }

                    break;
                }
            }


            
            using (FileStream writer = new FileStream(@"..\..\Data\BookPerson.xml", FileMode.Create))
            {

                xmlSerializer.Serialize(writer, bookPersons);
            }
            using (FileStream writer = new FileStream(@"..\..\Data\BookPersonHistory.xml", FileMode.Create))
            {

                xmlSerializer.Serialize(writer, bookPersonsHistory);
            }


        }

    }

}
