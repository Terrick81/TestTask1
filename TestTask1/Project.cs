using Newtonsoft.Json;
using TestTask1;

internal class Project: Common, IObject
{
    [JsonProperty]
    private string _name = "";
    [JsonProperty]
    private string _description = "";

    public void Add()
    {
        _name = Write.ReadName();
        _description = Write.ReadDescription();
        JsonManager.AddObject(this);
    }
    public void Change()
    {
        Write.Name(_id, _name);
        if (Write.ChangeField("name"))
            _name = Write.ReadName();
        if (Write.ChangeField("description"))
            _description = Write.ReadDescription();
        JsonManager.ChangeObject(this);
    }
    public void Print()
    {
        Write.Name(_id, _name);
        Write.Description(_description);
        Write.Empty();
    }
    public void Remote()
    {
        JsonManager.RemoveObject(this);
    }
    public string GetName()
    {
        return _name;
    }
    public void SimplePrint()
    {
        Write.Name(_id, _name);
        Write.Empty();
    }
}