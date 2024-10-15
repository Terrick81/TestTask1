using System.Security.Cryptography;
using System;
using System.Xml.Linq;
using TestTask1;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;

public class Write
{
    public static void Hello()
    {
        Console.WriteLine("Hello, User!\n");
    }
    public static void Login()
    {
        Console.WriteLine("Your login: ");
    }

    public static void Password()
    {
        Console.WriteLine("Your password: ");
    }

    public static void Uncorrect(bool annotation = false)
    {
        Console.Write("\n");
        if (annotation)
        {
            Console.Write("Uncorrect: ");
        }
        else
        {
            Console.WriteLine("Uncorrect");
        }

    }
    public static void MaxLenght()
    {
        Console.WriteLine("The maximum length has been exceeded\n");
    }

    public static void MinLenght()
    {
        Console.WriteLine("Empty value\n");
    }

    public static void IDNotFound()
    {
        Console.WriteLine("Id not found\n");
    }

    public static void Done()
    {
        Console.WriteLine("Done\n");
    }

    public static void Name(int id, string name)
    {
        Console.WriteLine($"[{id}] {name}");
    }

    public static void Password(string p)
    {
        Console.WriteLine("password: " + p);
    }
    public static void Description(string description)
    {
        Console.WriteLine("description: " + description);
    }

    public static string ReadName()
    {
        return EnterString("(MAX " + Program.MAX_NAME_CHARECTERS
            + ") Enter name: ", Program.MAX_NAME_CHARECTERS);
    }

    public static string ReadPassword()
    {
        return EnterString("(MAX " + Program.MAX_PASSWORD_CHARECTERS
            + ") Enter password: ", Program.MAX_PASSWORD_CHARECTERS);
    }
    public static string EnterString(string text, int maxLengh)
    {
        while (true)
        {
            Console.Write(text);
            string t = Console.ReadLine();
            if (t.Length > maxLengh)
            {
                Uncorrect(annotation: true);
                MaxLenght();
            }
            else if(t.Length <= 0)
            {
                Uncorrect(annotation: true);
                MinLenght();
            }
            else
            {
                return t;
            }
        }
    }

    public static int EnterInt(string text, int Max = int.MaxValue)
    {
        while (true)
        {
            Console.Write( $"\n {text}");
            int number;
            string t = Console.ReadLine();
            if(int.TryParse(t, out number))
            {
                if (number >= 0 && number < Max)
                    return number;
            }
            Uncorrect();
        }
    }





    public static string ReadDescription()
    {
        return EnterString("(MAX " + Program.MAX_DESCRIPTION_CHARECTERS
            + ") Enter description: ", Program.MAX_DESCRIPTION_CHARECTERS);
    }

    public static void List<T>(List<T>? list, bool simple = true) where T : IObject
    {
        if (list == null || list.Count == 0) Console.WriteLine("List is empty");
        else
        {
            Console.WriteLine("_______________________\n");
            foreach (var item in list)
            {
                if (simple) { item.SimplePrint(); }
                else { item.Print(); }
            }
        }
    }

    public static void Log(int workerId, int taskId, TypeTask lastType, TypeTask newType, DateTime time)
    {
        Console.WriteLine($"{time} - '{GetNameById<User>(workerId)}' changed the " +
            $"status of the task '{GetNameById<UserTask>(taskId)}' " +
            $"with '{lastType.ToString()}' on '{newType.ToString()}'");
    }

    public static string GetNameById<T>(int id) where T: Common, IObject
    {
        List<T>? list = JsonManager.ReadJson<T>();
        if (list == null) return "unknown";

        T? a = JsonManager.FindById(id, list);
        if (a == null) return "unknown";
        else return a.GetName();
    }

    public static int GetIdInList<T>(List<T>? list) where T : IObject
    {
        Write.List(list);
        if (list == null || list.Count == 0)
        {
            Write.Uncorrect();
            return -1;
        }
        else
        {
            return EnterInt("Enter ID: ");
        }
    }

    public static bool EnterBool(string text)
    {
        while (true)
        {
            Console.Write($"{text} [Y] [N]? ");
            var key = Console.ReadKey(false).Key;
            Console.WriteLine("\n");
            if (key == ConsoleKey.Y) return true;
            if (key == ConsoleKey.N) return false;
            else Write.Uncorrect();
        }
    }

    public static bool ChangeField(string v)
    {
        return EnterBool($"Change {v}");
    }

    internal static void TypeTask(TypeTask type)
    {
        Console.WriteLine("Status: " + type.ToString());
    }

    internal static int Methods(bool admin)
    {
        int max = 2;
        Console.WriteLine("\n_________MENU_________");
        Console.WriteLine("[1] check my task");
        Console.WriteLine("[2] change task status");

        if (admin)
        {
            Console.WriteLine("[3] print logs");
            Console.WriteLine("\n__ working with __\n");
            Console.WriteLine("[4] Projects");
            Console.WriteLine("[5] Tasks");
            Console.WriteLine("[6] Workers");

            max += 4;
        }

        return EnterInt("Enter index method: ", max + 1);

    }

    internal static TypeTask EnterStatus()
    {
        Console.WriteLine("[0] ToDo");
        Console.WriteLine("[1] InProgress");
        Console.WriteLine("[2] Done");

        int index = Write.EnterInt("index: ", 3);

        return (TypeTask)index;
    }

    internal static int EnterObjectsMenu()
    {
        Console.WriteLine("\n");
        Console.WriteLine("[1] Add");
        Console.WriteLine("[2] Remote");
        Console.WriteLine("[3] Change");
        Console.WriteLine("[4] Print");
        Console.WriteLine("[5] Back");
        
        return Write.EnterInt("index: ", 6);
    }

    internal static void Expectation()
    {
        Console.Write("Enter any button ");
        Console.ReadKey(false);
        Console.Clear();
    }

    internal static void Empty()
    {
        Console.WriteLine();
    }
}