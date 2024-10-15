

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestTask1
{
    public class JsonManager
    {
        public static void AddObject<T>(T _object) where T : Common
        {
            List<T> list = ReadJson<T>();

            if (list.Count == 0)
            {
                _object.SetID(0);
                list = new List<T>();
            }
            else
            {
                int id = list[list.Count - 1].GetID();
                _object.SetID(id + 1);
            }

            list.Add(_object);
            WriteJson(list);
            Write.Done();
        }
        public static void RemoveObject<T>(T _object) where T : Common
        {
            List<T> list = ReadJson<T>();

            var r = FindById(_object.GetID(), list);
            if (r != null)
            {
                list.Remove(r);
                WriteJson(list);
                Write.Done();
            }
        }
        public static void ChangeObject<T>(T _object) where T : Common
        {
            List<T> list = ReadJson<T>();

            T? r = FindById(_object.GetID(), list);
            if (r != null)
            {
                list[list.IndexOf(r)] = _object;
                WriteJson(list);
                Write.Done();
            }
        }
        public static T? FindById<T>(int id, List<T> list) where T : Common
        {
            if (list.Count == 0)
            {
                Write.Uncorrect(annotation: true);
                Write.IDNotFound();
                return null;
            }
            foreach (var obj in list)
            {
                if (obj.GetID() == id)
                {
                    return obj;
                }
            }
            Write.Uncorrect(annotation: true);
            Write.IDNotFound();
            return null;
        }

        public static User? CheckAutorization(List<User> list, string Login, string Password)
        {
            foreach (var obj in list)
            {
                if (obj.GetLogin() == Login)
                {
                    if (obj.GetPassword() == Password)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }

        public static List<T> ReadJson<T> ()
        {
            List<T>? items;
            using (StreamReader r = new StreamReader(Common.GetPath(typeof(T))))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<T>>(json);
            }
            
            if (items == null || items.Count == 0) return new List<T>();
            return items;
        }
        private static void WriteJson<T>(List<T> objects)
        {
            File.WriteAllText(Common.GetPath(typeof(T)), JsonConvert.SerializeObject(objects, Formatting.Indented));
        }

        public static List<UserTask> FilterByUserId(int id)
        {
            List<UserTask>? tasks = ReadJson<UserTask>();
            if (tasks == null || tasks.Count == 0)
            {
                return new List<UserTask>();
            }
            else
            {
                return tasks.FindAll(
                    delegate (UserTask bk)
                    {
                        return bk.GetOwnerId() == id;
                    }
                    );
            }
        }
    }
}
