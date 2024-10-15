using Newtonsoft.Json;
using System.Xml.Linq;
using TestTask1;

public abstract class Common
{
    private const string PROJECTS_JSON_NAME = "projects";
    private const string USER_TASK_JSON_NAME = "userTasks";
    private const string USER_JSON_NAME = "workers";
    private const string LOG_JSON_NAME = "log";

    [JsonProperty]
    protected int _id;

    public int GetID()
    {
        return _id;
    }
    public void SetID(int id)
    {
         _id = id;
    }
    public static string GetPath(System.Type type)
    {
        if (type == typeof(Project))
        {
            return NameToPath(PROJECTS_JSON_NAME);
        }
        else if (type == typeof(UserTask))
        {
            return NameToPath(USER_TASK_JSON_NAME);
        }
        else if (type == typeof(User))
        {
            return NameToPath(USER_JSON_NAME);
        }
        else if (type == typeof(Log))
        {
            return NameToPath(LOG_JSON_NAME);
        }

        return "";
    }
    private static string NameToPath(string name)
    {
        var relativePath = @"..\..\..\JsonFiles\" + name + ".json";
        var f = Path.GetFullPath(relativePath);
        return f;
    }
}