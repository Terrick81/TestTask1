using TestTask1;

internal class AdminUser: User
{
    public AdminUser()
    {

    }
    public override void Add( int Admin = false)
    {
        _login = Write.ReadName();
        _password = Write.ReadPassword();
        _admin = true;
        JsonManager.AddObject((User)this);
    }

    public override void Change()
    {
        base.Change();
        if (Write.ChangeField("admin"))
            _admin = Write.EnterBool("admin");
    }
}