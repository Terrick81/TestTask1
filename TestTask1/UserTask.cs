// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using TestTask1;

public class UserTask : Common, IObject
{
    [JsonProperty]
    private string _name = "";
    [JsonProperty]
    private string _description = "";
    [JsonProperty]
    private TypeTask _type = TypeTask.toDo;
    [JsonProperty]
    private int _projectId;
    [JsonProperty]
    private int _ownerId;
    
    public string GetName()
    {
        return _name;
    }
    public void Add()
    {
        _name = Write.ReadName();
        _description = Write.ReadDescription();

        _projectId = Write.GetIdInList(JsonManager.ReadJson<Project>());
        if (_projectId == -1) { return; }

        _ownerId = Write.GetIdInList(JsonManager.ReadJson<User>());
        if (_ownerId == -1) { return; }
        JsonManager.AddObject(this);
    }
    public void Change()
    {
        Write.Name(_id, _name);
        if (Write.ChangeField("name"))
            _name = Write.ReadName();
        if (Write.ChangeField("description"))
            _description = Write.ReadDescription();
        if (Write.ChangeField("project"))
        {
            _projectId = Write.GetIdInList(JsonManager.ReadJson<Project>());
            if (_projectId == -1) { return; }
        }
        if (Write.ChangeField("owner"))
        {
            _ownerId = Write.GetIdInList(JsonManager.ReadJson<User>());
            if (_ownerId == -1) { return; }
        }
        JsonManager.ChangeObject(this);
    }
    public void ChangeStatus(TypeTask newStatus)
    {
        if (newStatus != _type)
        {
            Log.AddLog(_ownerId, _id, _type, newStatus);
            _type = newStatus;
            JsonManager.ChangeObject(this);
        }
        else
        {
            Write.Uncorrect();
        }
    }
    public void Print()
    {
        Write.Name(_id, _name);
        Write.Description(_description);
        Write.TypeTask(_type);
        Write.GetNameById<Project>(_projectId);
        Write.GetNameById<User>(_ownerId);
        Write.Empty();
    }
    public void Remote()
    {
        JsonManager.RemoveObject(this);
    }
    public void SimplePrint()
    {
        Write.Name(_id, _name);
        Write.TypeTask(_type);
        Write.Empty();
    }
    public int GetOwnerId()
    {
        return _ownerId;
    }
}