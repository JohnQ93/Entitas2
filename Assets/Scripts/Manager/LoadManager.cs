using UnityEngine;

namespace Manager
{
    public class LoadManager
    {
        public static LoadManager Instance { get; private set; } = new LoadManager();
        public T Load<T>(string path, string name) where T : class
        {
            return Resources.Load(path + name) as T;
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
    }
}
