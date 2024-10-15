using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using TestTask1;
public enum TypeTask
{
    toDo,
    InProgress,
    Done,
};

internal class Program
{
    public const int MAX_PASSWORD_CHARECTERS = 10;
    public const int MAX_NAME_CHARECTERS = 20;
    public const int MAX_DESCRIPTION_CHARECTERS = 100;

    private static void Main(string[] args)
    {
        Program p = new Program();
        var user =  p.Authorization();

        while (true)
        {
            int index = Write.Methods(user.GetAdmin());

            switch (index)
            {
                case 1:
                    user.PrintMyTask();
                    break;
                case 2:
                    user.ChangeStatusMyTask();
                    break;
            }

            if (user.GetAdmin())
            {
                switch (index)
                {
                    case 3:
                        Write.List(JsonManager.ReadJson<Log>());
                        break;
                    case 4:
                        WorkingWithObject(new Project());
                        break;
                    case 5:
                        WorkingWithObject(new UserTask());
                        break;
                    case 6:
                        WorkingWithObject(new User());
                        break;
                }
            }

            Write.Expectation();

        }
    }

    private static void WorkingWithObject<T>(T _object) where T : Common, IObject
    {
        int index = Write.EnterObjectsMenu();
        
        if (index > 1 && index < 4)
        {
            List<T> list = JsonManager.ReadJson<T>();
            int id = Write.GetIdInList(list);
            _object = JsonManager.FindById(id, list);
        }

        switch (index)
        {
            case 1:
                _object.Add();
                break;
            case 2:
                _object.Remote();
                break;
            case 3:
                _object.Change();
                break;
            case 4:
                List<T> list = JsonManager.ReadJson<T>();
                Write.List(list, simple: false);
                break;
            case 5:
                break;
        }

    }


    private User Authorization() 
    {
        List<User> users = JsonManager.ReadJson<User>();
        if (users.Count == 0)
        {
            User user = new User();
            user.AddAdmin();
            return user;
        }
        else
        {
            while (true)
            {
                var login = Write.EnterString("Login: ", MAX_NAME_CHARECTERS);
                var password = Write.EnterString("Password: ", MAX_PASSWORD_CHARECTERS);

                User? user = JsonManager.CheckAutorization(users, login, password);
                if (user != null)
                {
                    return user;
                }
                else Write.Uncorrect();
            }
        }
    }
}


