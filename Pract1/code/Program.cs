using System.Text;
using Xunit;

namespace ATARK_PZ
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10; // Що таке "a"? ПОГАНО
            int ageOfTheUserCurrentlyUsingTheApp = 10; // Надлишкова інформація ПОГАНО


            int userAge = 10;  // ДОБРЕ
            int MinimumAgeForAccess = 18;
            if (userAge >= MinimumAgeForAccess) //ДОБРЕ
            {
                //...
            }
            if (userAge < 18) //Що означає це число? ПОГАНО
            {
                //...
            }

            string username = "";

            var db = new Database();

            // Перевіряємо, чи існує користувач з таким ім'ям
            //var user = db.Users.SingleOrDefault(u => u.Username == username);

            // Використовуємо SingleOrDefault, оскільки очікуємо, що ім'я користувача унікальне
            var user = db.Users.SingleOrDefault(u => u.Username == username);

        }
    }
    
    class Calculator
    {
        public static int Add(int num1, int num2) 
        {
            return num1 + num2;
        }

        public static double Divide(double numerator, double divisor) { return numerator / divisor; }
    }

    class Database
    {
        public List<User> Users = new();

        [Fact]
        public void Divide_WhenDivisorIsZero_ShouldThrowException()
        {
            // Arrange
            double numerator = 10;
            double divisor = 0;

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => Calculator.Divide(numerator, divisor));
        }

        [Fact]
        public void Add_ShouldReturnSum()
        {
            // Arrange
            int a = 5;
            int b = 10;

            // Act
            int result = Calculator.Add(a, b);

            // Assert
            Assert.Equal(15, result);
        }
    }

    class Test
    {
        private string[] _nameList = { "Vova", "Danya", "Artur" };
        public void PrintNamesStartingWithVowel(IEnumerable<string> nameList)
        {
            char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'Y' };

            var filteredNames = nameList
                .Where(name => !string.IsNullOrEmpty(name) && vowels.Contains(char.ToUpper(name[0])))
                .ToList();

            foreach (var name in filteredNames)
            {
                Console.WriteLine(name);
            }
        }

        public void ProcessData(string data)
        {
            // TODO: Замінити цей парсер на більш оптимальний алгоритм після релізу v2.0.
            var result = data.Split(',');

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        public decimal CalculateDiscount(decimal price, int customerLoyaltyYears)
        {
            // Базова знижка становить 5%.
            // Якщо клієнт обслуговується понад 5 років, додається додаткова знижка у 1% за кожен рік, але не більше 10%.
            decimal baseDiscount = 0.05m;
            decimal loyaltyBonus = Math.Min(customerLoyaltyYears * 0.01m, 0.10m);

            return price - (price * (baseDiscount + loyaltyBonus));
        }
        /// <summary>
        /// Отримує інформацію про користувача за його ідентифікатором.
        /// </summary>
        /// <param name="userId">Унікальний ідентифікатор користувача.</param>
        /// <returns>
        /// Об'єкт <see cref="User"/> із даними користувача. 
        /// Якщо користувача не знайдено, повертає null.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Викидається, якщо <paramref name="userId"/> не є валідним.
        /// </exception>
        public User GetUserById(string userId)
        {
            return new User();
        }
    }

    public class User 
    {
        public string Username;
    }


    public interface IUserService
    {
        void AddUser(string username, string email);
        IEnumerable<string> GetAllUsernames();
    }

    // Реалізація сервісу
    public class UserService : IUserService
    {
        private const int MaxUsernameLength = 20;

        private readonly List<string> _usernames = new();

        public int UserCount => _usernames.Count;

        public void AddUser(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty.", nameof(username));
            }

            if (username.Length > MaxUsernameLength)
            {
                throw new ArgumentException($"Username cannot exceed {MaxUsernameLength} characters.", nameof(username));
            }

            _usernames.Add(username);
        }
        //...
        // Метод для отримання всіх імен користувачів
        public IEnumerable<string> GetAllUsernames()
        {
            return _usernames;
        }
    }
}