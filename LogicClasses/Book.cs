using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyLibrary.LogicClasses
{
    public class Book
    {
        private int id;
        private string name;
        private string autors;
        private string tags;
        private int quantityInLibrary;
        private int quantityOfReaders;

        public int ID { get { return id; } set { id = value; } }
        public string Name {  get { return name; } set { name = value; } }
        public string Autors { get { return autors; } set { autors = value; } }
        public int QuantityInLibrary { get { return quantityInLibrary; } set { quantityInLibrary = value; } }
        public int QuantityOfReaders { get { return quantityOfReaders; } set { quantityOfReaders = value; } }
        public string Tags { get { return tags; } set { tags = value; } }

        public Book(int id, string name, string autors, string tags, int quantityInLibrary, int quantityOfReaders)
        {
            this.id = id;
            this.name = name;
            this.autors = autors;
            this.tags = tags;
            this.quantityInLibrary = quantityInLibrary;
            this.quantityOfReaders = quantityOfReaders;

        }

        public Book() { }

        public override string ToString()
        {
            return name + ". Автор(ы): " + autors + "\nТеги: " +tags + "\nКоличество книг в библиотеке/на руках: " + quantityInLibrary + "/" + quantityOfReaders + ".";
        }

        public string ToShortString()
        {
            return name + "\n. Автор(ы): " + autors;
        }

        public override bool Equals(object obj)
        {
            Book book = obj as Book;
            if (book != null && ID == book.ID)
            {
                return true;
            }

            return false;
           
        }
    }
}
