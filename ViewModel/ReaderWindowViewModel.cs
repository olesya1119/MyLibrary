using MyLibrary.LogicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel
{
    internal class ReaderWindowViewModel : BaseViewModel
    {
        public ReaderWindowViewModel(Person person) { 
            Info = person.ToString();
            listBooks = DataBaseController.FindPersonsBook(person.Login);
        
        }
        private string info;
        private List<Book> listBooks;

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged(nameof(Info));
            }
        }

        public List<Book> ListBooks
        {
            get { return listBooks; }
            set
            {
                listBooks = value;
                OnPropertyChanged(nameof(ListBooks));
            }
        }

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


        private void UpdateSearchBooks()
        {
            SearchBooks = DataBaseController.FindBooks(SearchBookName, SearchBookAutors, SearchBookTags);
        }

    }
}
