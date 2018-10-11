using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool App = true;

            List<User> listOfUsers = new List<User>();        
            bool loggedin = false;
            List<Book> books = new List<Book>();
            Admin admin = new Admin();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };


            string jsonFromFile = File.ReadAllText(@"C:\Users\sadbo\Desktop\Škola\C#\Evidence_knih\ConsoleApp1\books.json");
            books = JsonConvert.DeserializeObject<List<Book>>(jsonFromFile, settings);

            jsonFromFile = File.ReadAllText(@"C:\Users\sadbo\Desktop\Škola\C#\Evidence_knih\ConsoleApp1\users.json");
            listOfUsers = JsonConvert.DeserializeObject<List<User>>(jsonFromFile, settings);
            while (App)
            {
                bool registration = true;
                bool login = true;
                int userType = 0;
                string currentUser = null;
                Console.WriteLine("1) Registrate user");
                Console.WriteLine("2) Login user");
                ConsoleKeyInfo choice = Console.ReadKey();
                if (choice.KeyChar == '1')
                {
                    while (registration)
                    {
                        Console.Clear();
                        Console.WriteLine("Registration");
                        Console.WriteLine("");
                        Console.WriteLine("Username");
                        string username = Console.ReadLine();
                        Console.WriteLine("Password");
                        string pass = Console.ReadLine();
                        Console.WriteLine("Confirm password");
                        string confirmpass = Console.ReadLine();
                        if (confirmpass == pass)
                        {
                            User user = new User();
                            user.Name = username;
                            user.Password = pass;
                            listOfUsers.Add(user);
                            registration = false;
                            Console.Clear();
                            Console.WriteLine("Registration succeful.");

                            string json = JsonConvert.SerializeObject(listOfUsers, settings);
                            File.WriteAllText(@"C:\Users\sadbo\Desktop\Škola\C#\EvidenceKnih\ConsoleApp1\ConsoleApp1\users.json", json);
                        
                    }
                }
                else if (choice.KeyChar == '2')
                {
                    while (login)
                    {                        
                        Console.Clear();
                        Console.WriteLine("Login");
                        Console.WriteLine("");
                        Console.Write("Username:");
                        string username = Console.ReadLine();
                        Console.Write("Password:");
                        string pass = Console.ReadLine();
                        if (username == admin.Name & pass == admin.Pass)
                        {
                            Console.Clear();
                            userType = 1;
                            loggedin = true;
                            login = false;
                        }
                        else
                        {
                            for (int i = 0; i < listOfUsers.Count; i++)
                            {
                                if (listOfUsers[i].Name == username & listOfUsers[i].Password == pass)
                                {
                                    Console.Clear();
                                    Console.WriteLine("You have been succefully log-in");
                                    userType = 0;
                                    login = false;
                                    loggedin = true;
                                    currentUser = username;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Wrong username or password.");
                                    Console.WriteLine("");
                                    while (true)
                                    {
                                        Console.WriteLine("Press T to try again");
                                        Console.WriteLine("Press E to exit to menu");
                                        choice = Console.ReadKey();
                                        if (choice.KeyChar == 't')
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                        else if (choice.KeyChar == 'e')
                                        {
                                            Console.Clear();
                                            login = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }                    
                    }
                    while (loggedin)
                    {
                        if(userType == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Logged as " + currentUser);
                            Console.WriteLine("1) Create a book");
                            Console.WriteLine("2) Edit a book");
                            Console.WriteLine("3) Delete a book");
                            Console.WriteLine("");
                            Console.WriteLine("Your books:");
                            for (int i = 0; i < books.Count(); i++)
                            {
                                Console.Write(i + " ");
                                Console.Write(books[i].Name);
                                Console.Write(" - ");
                                Console.Write(books[i].Isbn);
                                Console.WriteLine("");

                            }
                            choice = Console.ReadKey();
                            while (true)
                            {
                                if (choice.KeyChar == '1')
                                {
                                    Console.Clear();
                                    Console.WriteLine("What kind of book do you want to create?");
                                    Console.WriteLine("");
                                    Console.WriteLine("1)E-book");
                                    Console.WriteLine("2)Paper book");
                                    choice = Console.ReadKey();
                                    if (choice.KeyChar == '1')
                                    {
                                        Console.Clear();
                                        Console.Write("Name of the book:");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("");
                                        Console.Write("URI of the book:");
                                        string uri = Console.ReadLine();
                                        Console.WriteLine("");
                                        Console.Write("Size (in mb) of the book:");
                                        string sizemb = Console.ReadLine();
                                        Console.WriteLine("");
                                        int num;
                                        while(!int.TryParse(sizemb, out num))
                                        {
                                            Console.Write("Size (in mb) of the book:");
                                            sizemb = Console.ReadLine();
                                            Console.WriteLine("");
                                        }
                                        if(int.TryParse(sizemb, out num))
                                        {
                                            Ebook book = new Ebook();
                                            book.Name = name;
                                            book.Uri = uri;
                                            int sizeMB = int.Parse(sizemb);
                                            book.Sizemb = sizeMB;
                                            books.Add(book);
                                            Console.WriteLine("Book " + name + " have been succefuly added.");

                                            string json = JsonConvert.SerializeObject(books, settings);
                                            File.WriteAllText(@"C:\Users\sadbo\Desktop\Škola\C#\Evidence_knih\ConsoleApp1\books.json", json);
                                            break;
                                        }
                                    }
                                    else if (choice.KeyChar == '2')
                                    {
                                        Console.Clear();
                                        Console.Write("Name of the book:");
                                        string name = Console.ReadLine();
                                        Console.WriteLine("");
                                        Console.Write("Weight of the book:");
                                        string weight = Console.ReadLine();
                                        Console.WriteLine("");
                                        Console.Write("Stock of the book:");
                                        string stock = Console.ReadLine();
                                        Console.WriteLine("");
                                        int num;
                                        while (!int.TryParse(weight, out num))
                                        {
                                            Console.Write("Size (in mb) of the book:");
                                            weight = Console.ReadLine();
                                            Console.WriteLine("");
                                        }
                                        while (!int.TryParse(stock, out num))
                                        {
                                            Console.Write("Stock of the book:");
                                            stock = Console.ReadLine();
                                        }
                                        if (int.TryParse(weight, out num))
                                        {
                                            PaperBook book = new PaperBook();
                                            book.Name = name;
                                            book.Weight = int.Parse(weight);
                                            book.Stock = int.Parse(stock);
                                            books.Add(book);
                                            Console.WriteLine("Book " + name + " have been succefuly added.");

                                            string json = JsonConvert.SerializeObject(books, settings);
                                            File.WriteAllText(@"C:\Users\sadbo\Desktop\Škola\C#\Evidence_knih\ConsoleApp1\books.json", json);
                                            break;
                                        }
                                    }
                                }
                                else if (choice.KeyChar == '3')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter ID of book which you want to be deleted.");
                                    string deleteID = Console.ReadLine();
                                    int num;
                                    while (!int.TryParse(deleteID, out num))
                                    {
                                        for (int i = 0; i < books.Count(); i++)
                                        {
                                            Console.Write(i + " ");
                                            Console.Write(books[i].Name);
                                            Console.Write(" - ");
                                            Console.Write(books[i].Isbn);
                                            Console.WriteLine("");

                                        }
                                        Console.WriteLine();
                                        Console.WriteLine("Enter ID of book which you want to be deleted.");
                                        deleteID = Console.ReadLine();
                                    }
                                    if (int.TryParse(deleteID, out num))
                                    {
                                        int delete = int.Parse(deleteID);
                                        if (delete > books.Count())
                                        {
                                            Console.WriteLine("Wrong index");
                                        }
                                        else
                                        {
                                            books.RemoveAt(delete);
                                            string json = JsonConvert.SerializeObject(books, settings);
                                            File.WriteAllText(@"C:\Users\sadbo\Desktop\Škola\C#\Evidence_knih\ConsoleApp1\books.json", json);
                                            break;
                                        }
                                    }
                                }
                            }                            
                        }
                        else if(userType == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Logged as Admin");
                            Console.WriteLine("1) Save Books to excel");
                            Console.WriteLine("2) Delete user");
                            choice = Console.ReadKey();

                            if (choice.KeyChar == '1')
                            {
                                using (var p = new ExcelPackage())
                                {
                                    var ws = p.Workbook.Worksheets.Add("MySheet");
                                    for (int i = 0; i < books.Count(); i++)
                                    {
                                        ws.Cells[i + 1, 1].Value = books[i].Name;
                                        ws.Cells[i + 1, 2].Value = books[i].Isbn;
                                    }
                                    p.SaveAs(new FileInfo(@"C:\Users\sadbo\Desktop\Škola\C#\Evidence_knih\ConsoleApp1\excel.xlsx"));
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
