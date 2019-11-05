using UnityEngine;
using System;

namespace CustomTool
{
    [Serializable]
    public class NamespaceData : ScriptableObject
    {
        [SerializeField]
        public string name;
    }
}
