using MyLibrary.LogicClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLibrary.ViewModel
{
    public class LibrarianWindowViewModel : BaseViewModel
    {
       
        //----- "Найти книгу"------

        //Значения в поисковых строках 

        private string searchBookName;
        private string searchBookAutors;
        private string searchBookTags;

        //Найденные книги
        private List<Book> searchBooks;

        //Выбранная книга
        private Book selectedBook;

        //Поиск книги
        private RelayCommand searchBooksCommand;

        public RelayCommand SearchBooksCommand
        {
            get
            {
                return searchBooksCommand ??
                  (searchBooksCommand = new RelayCommand(obj =>
                  {
                      UpdateSearchBooks();
                  }));
            }
        }

        public string SearchBookName
        {
            get { return searchBookName; }
            set
            {
                searchBookName = value;
                OnPropertyChanged(nameof(SearchBookName));
            }
        }
        public string SearchBookAutors
        {
            get { return searchBookAutors; }
            set
            {
                searchBookAutors = value;
                OnPropertyChanged(nameof(SearchBookAutors));
            }
        }
        public string SearchBookTags
        {
            get { return searchBookTags; }
            set
            {
                searchBookTags = value;
                OnPropertyChanged(nameof(SearchBookTags));
            }
        }
        public List<Book> SearchBooks
        {
            get { return searchBooks; }
            set
            {
                searchBooks = value;
                OnPropertyChanged(nameof(SearchBooks));
            }
        }
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }


        //----- "Найти читателя"------

        //Значения в поисковых строках "Найти читателя"

        private string searchPersonLogin;
        private string searchPersonLastName;
        private string searchPersonPhone;
        private string searchPersonEmail;
        

        //Найденные читатели
        private List<Person> searchPersons;

        //Выбранный читатель
        public Person selectedPerson;

        public string SearchPersonLogin
        {
            get { return searchPersonLogin; }
            set
            {
                searchPersonLogin = value;
                OnPropertyChanged(nameof(SearchPersonLogin));
            }
        }
        public string SearchPersonLastName
        {
            get { return searchPersonLastName; }
            set
            {
                searchPersonLastName = value;
                OnPropertyChanged(nameof(SearchPersonLastName));
            }
        }
        public string SearchPersonPhone
        {
            get { return searchPersonPhone; }
            set
            {
                searchPersonPhone = value;
                OnPropertyChanged(nameof(SearchPersonPhone));
            }
        }
        public string SearchPersonEmail
        {
            get { return searchPersonEmail; }
            set
            {
                searchPersonEmail = value;
                OnPropertyChanged(nameof(SearchPersonEmail));
            }
        }
        public List<Person> SearchPersons
        {
            get { return searchPersons; }
            set
            {
                searchPersons = value;
                OnPropertyChanged(nameof(SearchPersons));
            }
        }
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));

                UpdateGiveInfo();
                UpdateSelectedBooks();
            }
        }

        //Поиск читателя
        private RelayCommand searchPersonsCommand;

        public RelayCommand SearchPersonsCommand
        {
            get
            {
                return searchPersonsCommand ??
                  (searchPersonsCommand = new RelayCommand(obj =>
                  {
                      UpdateSearchPersons();
                  }));
            }
        }



        //----"Создать читательский билет"
        //Поля читателя
        private string createPersonLogin;
        private string createPersonFirstName;
        private string createPersonLastName;
        private string createPersonPytronymic;
        private string createPersonPhone;
        private string createPersonEmail;
        private string createPersonAddress;


        public string CreatePersonLogin
        {
            get { return createPersonLogin; }
            set
            {
                createPersonLogin = value;
                OnPropertyChanged(nameof(CreatePersonLogin));
            }
        }
        public string CreatePersonFirstName
        {
            get { return createPersonFirstName; }
            set
            {
                createPersonFirstName = value;
                OnPropertyChanged(nameof(CreatePersonFirstName));
            }
        }
        public string CreatePersonLastName
        {
            get { return createPersonLastName; }
            set
            {
                createPersonLastName = value;
                OnPropertyChanged(nameof(CreatePersonLastName));
            }
        }
        public string CreatePersonPytronymic
        {
            get { return createPersonPytronymic; }
            set
            {
                createPersonPytronymic = value;
                OnPropertyChanged(nameof(CreatePersonPytronymic));
            }
        }
        public string CreatePersonPhone
        {
            get { return createPersonPhone; }
            set
            {
                createPersonPhone = value;
                OnPropertyChanged(nameof(CreatePersonPhone));
            }
        }
        public string CreatePersonEmail
        {
            get { return createPersonEmail; }
            set
            {
                createPersonEmail = value;
                OnPropertyChanged(nameof(CreatePersonEmail));
            }
        }
        public string CreatePersonAddress
        {
            get { return createPersonAddress; }
            set
            {
                createPersonAddress = value;
                OnPropertyChanged(nameof(CreatePersonAddress));
            }
        }

        private RelayCommand createPersonsCommand;

        public RelayCommand CreatePersonsCommand
        {
            get
            {
                return createPersonsCommand ??
                  (createPersonsCommand = new RelayCommand(obj =>
                  {
                      CreateLibraryCard();
                  }));
            }
        }


        //---"Вернуть книгу"---
        private string returnInfo;
        private List<Book> booksSelectedPerson;
        private Book selectedBooksSelectedPerson;


        public string ReturnInfo
        {
            get { return returnInfo; }
            set
            {
                returnInfo = value;
                OnPropertyChanged(nameof(ReturnInfo));
            }
        }
        public List<Book> BooksSelectedPerson
        {
            get { return booksSelectedPerson; }
            set
            {
                booksSelectedPerson = value;
                OnPropertyChanged(nameof(BooksSelectedPerson));
            }
        }
        public Book SelectedBooksSelectedPerson
        {
            get { return selectedBooksSelectedPerson; }
            set
            {
                selectedBooksSelectedPerson = value;
                OnPropertyChanged(nameof(SelectedBooksSelectedPerson));

            }
        }

        private RelayCommand returnPersonsCommand;

        public RelayCommand ReturnPersonsCommand
        {
            get
            {
                return returnPersonsCommand ??
                  (returnPersonsCommand = new RelayCommand(obj =>
                  {
                      ReturnBook();
                  }));
            }
        }


        //---"Выдать книгу"---

        private string giveInfo;
        private string days;

        public string GiveInfo
        {
            get { return giveInfo; }
            set
            {
                giveInfo = value;
                OnPropertyChanged(nameof(GiveInfo));
            }
        }
        
        public string Days
        {
            get { return days; }
            set
            {
                days = value;
                OnPropertyChanged(nameof(Days));
            }

        }


        private RelayCommand givePersonsCommand;

        public RelayCommand GivePersonsCommand
        {
            get
            {
                return givePersonsCommand ??
                  (givePersonsCommand = new RelayCommand(obj =>
                  {
                      GiveBook();
                  }));
            }
        }





        private void GiveBook()
        {
           
            if (selectedPerson != null && SelectedBook != null)
            {
                try
                { 
                   DataBaseController.AddBookPerson(SelectedPerson, SelectedBook, Days);
                   MessageBox.Show("Выдача успешна!");
                }
                catch (Exception ex)
                {
                   MessageBox.Show(ex.Message);
                }
            }
            UpdateAll();

        }


        private void ReturnBook()
        {

            if (selectedBooksSelectedPerson != null && SelectedPerson != null)
            {
                DataBaseController.ReturnBook(SelectedPerson, SelectedBooksSelectedPerson);
                MessageBox.Show("Книга возвращена!");
                UpdateAll();
            }

            
        }

        private void UpdateGiveInfo()
        {
            if (selectedBook == null || selectedPerson == null)
            {
                GiveInfo = "Вам нужно выбрать читателя и книгу.";
            }
            else
            {
                GiveInfo = "Выдать книгу: \n" + SelectedBook.ToString() + "\nЧитателю: \n" + selectedPerson.ToString();

            }
        }

        private void UpdateSearchPersons()
        {
            SearchPersons = DataBaseController.FindPersons("", searchPersonLastName, "", searchPersonEmail, searchPersonPhone, searchPersonLogin, 0); 
        }

        private void UpdateSearchBooks()
        {
            SearchBooks = DataBaseController.FindBooks(SearchBookName, SearchBookAutors,SearchBookTags);
        }

        private void UpdateSelectedBooks()
        {
            if (selectedPerson != null)
            {
                ReturnInfo = "Читатель:\n" + selectedPerson.ToString();
                BooksSelectedPerson = DataBaseController.FindPersonsBook(selectedPerson.Login);
            }
        }

        private void UpdateAll()
        {
            SelectedPerson = null;
            SelectedBook = null;
            SelectedBooksSelectedPerson = null;
            UpdateSearchPersons();
            UpdateSearchBooks();
            UpdateSelectedBooks();
            UpdateGiveInfo();
        }

        private void CreateLibraryCard()
        {
            try
            {
                Password passwordNewPerson = new Password();
                Person newPerson = new Person(createPersonLogin, passwordNewPerson.HeshPassword, createPersonFirstName, createPersonLastName, createPersonPytronymic, createPersonEmail, createPersonPhone, createPersonAddress, 0);
                DataBaseController.RegistrationReader(newPerson);
                Mail.SendMailNewLibraryCard(newPerson, passwordNewPerson.NotHeshPassword);
                MessageBox.Show("Создание билета произошло успешно!");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        


    }
}
