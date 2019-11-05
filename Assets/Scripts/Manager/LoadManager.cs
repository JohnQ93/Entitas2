using UnityEngine;
using Util;

namespace Manager
{
    public class LoadManager : SingletonBase<LoadManager>
    {
        public T Load<T>(string path, string name) where T : class
        {
            return Resources.Load(path + name) as T;
        }

        public GameObject LoadAndInstantiate(string path, Transform parent)
        {
            var temp = Resources.Load<GameObject>(path);
            if(temp == null)
            {
                Debug.LogError("path: " + path + "is null");
                return null;
            }
            else
            {
                var go = Object.Instantiate(temp, parent);
                return go;
            }
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
    }
}
