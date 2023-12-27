using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.LogicClasses
{
    public enum Role { Reader, Librarian}

    public class Person
    {

        private string password;
        private string login;
        private string firstName;
        private string lastName;
        private string pytronymic;
        private string email;
        private string phone;
        private string adress;
        private Role role;
        

        public string Password {  get { return password; } set { password = value != "" ? value : throw new Exception("Пароль пустой."); } }
        public string Login{ get { return login; } set { login = value != "" ? value : throw new Exception("Логин указан неверно."); } }
        public string FirstName { get { return firstName; } set { firstName = ValidData.IsValidName(value) ? value : throw new Exception("Имя указано неверно."); } }
        public string LastName { get { return lastName; } set { lastName = ValidData.IsValidName(value) ? value : throw new Exception("Фамилия указана неверно."); } }
        public string Pytronymic { get { return pytronymic; } set { pytronymic = value == "" || ValidData.IsValidName(value) ? value : throw new Exception("Отчество указано неверно."); } }
        public string Email { get { return email; } set { email = ValidData.IsValidEmail(value) ? value : throw new Exception("Почта указана неверно."); } }
        public string Phone { get { return phone; } set { phone = ValidData.IsValidPhone(value) ? value : throw new Exception("Телефон указан неверно."); } }
        public string Adress { get { return adress; } set { adress = value != "" ? value : throw new Exception("Адрес пустой."); } }
        public int IdRole { get { return (int)role; } set { role = (Role)value; } }


        public Person(string login, string password, string firstName, string lastName, string pytronymic, string email, string phone, string adress, int idRole)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Pytronymic = pytronymic;
            Email = email;
            Phone = phone;
            Adress = adress;
            IdRole = idRole;
        }

        public Person() { }

        public override string ToString()
        {
            string s = "Имя: " + lastName + " " + firstName + " " + pytronymic + "\nПочта: " + email + "\nНомер телефона: " + phone + "\nРоль в системе: "; 
            switch (role)
            {
                case Role.Reader:
                    s += "читатель";
                    break;
                case Role.Librarian:
                    s += "библиотекарь";
                    break;
            }
            return s;
        }

        public override bool Equals(object obj)
        {
            Person person = obj as Person;
            if (person != null && login == person.Login)
            {
                return true;
            }
            return false;
        }
    }
}
