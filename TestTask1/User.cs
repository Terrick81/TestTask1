// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System;
using TestTask1;
using static System.Net.Mime.MediaTypeNames;

public class User : Common, IObject
{
    [JsonProperty]
    protected string _login = "";
    [JsonProperty]
    protected string _password = "";
    [JsonProperty]
    protected bool  _admin = false;


    public void AddAdmin()
    {
        _login = Write.ReadName();
        _password = Write.ReadPassword();
        _admin = true;
        JsonManager.AddObject((User)this);
    }
    public void Add()
    {
        _login = Write.ReadName();
        _password = Write.ReadPassword();
        _admin = Write.EnterBool("admin");
        JsonManager.AddObject(this);
    }
    public virtual void Change()
    {
        if (Write.ChangeField("name"))
            _login = Write.ReadName();
        if (Write.ChangeField("password"))
            _password = Write.ReadPassword();
        if (Write.ChangeField("admin"))
            _admin = Write.EnterBool("admin");
    }
    public string GetName()
    {
        return _login;
    }
    public void Print()
    {
        Write.Name(_id, _login);
        Write.Password(_password);
        Write.Empty();
    }
    public void Remote()
    {
        JsonManager.RemoveObject(this);
    }
    public void SimplePrint()
    {
        Write.Name(_id, _login);
        Write.Empty();
    }
    public void PrintMyTask(bool simple = false)
    {
        List<UserTask>? Mytasks = JsonManager.FilterByUserId(_id);
        Write.List(Mytasks, simple);
    }
    public string GetLogin()
    {
        return _login;
    }
    public string GetPassword()
    {
        return _password;
    }
    public bool GetAdmin()
    {
        return _admin;
    }

    internal void ChangeStatusMyTask()
    {
        List<UserTask> Mytasks = JsonManager.FilterByUserId(_id);
        Write.List(Mytasks, simple: true);
        
        if (Mytasks.Count!= 0)
        {
            while (true)
            {
                int idTask = Write.EnterInt("Enter ID task: ");

                UserTask? a = JsonManager.FindById(idTask, Mytasks);
                if (a != null)
                {
                    a.ChangeStatus(Write.EnterStatus());
                    return;
                }
            }
        }

    }
}

