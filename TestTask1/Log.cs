using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask1
{
    public class Log : Common, IObject
    {
        [JsonProperty]
        private DateTime _time;
        [JsonProperty]
        private int _workerId;
        [JsonProperty]
        private int _taskId;
        [JsonProperty]
        private TypeTask _lastType;
        [JsonProperty]
        private TypeTask _newType;

        Log()
        {
        
        }

        Log(int _workerId, int _taskId, TypeTask _lastType, TypeTask _newType)
        {
            this._workerId = _workerId;
            this._taskId = _taskId;
            this._lastType = _lastType;
            this._newType = _newType;
        }

        public static void AddLog(int _workerId, int _taskId, TypeTask _lastType, TypeTask _newType)
        {
            Log currentLog = new Log(_workerId, _taskId, _lastType, _newType);
            currentLog.Add();
        }

        public void Add()
        {
            _time = DateTime.Now;
            JsonManager.AddObject(this);
        }

        public void Change()
        {
            return;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            Write.Log(_workerId, _taskId, _lastType, _newType, _time);
        }

        public void Remote()
        {
            return;
        }

        public void SimplePrint()
        {
            Write.Log(_workerId, _taskId, _lastType, _newType, _time);
        }
    }
}
