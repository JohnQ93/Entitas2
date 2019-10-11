using UnityEngine;
using Const;
using System.Collections.Generic;
using System;

namespace UIFrame
{
    public class UIManager : MonoBehaviour
    {
        private Dictionary<UiId, GameObject> _prefabDictionary = new Dictionary<UiId, GameObject>();

        public void Show(UiId id)
        {
            GameObject ui = GetPrefabObject(id);
            if (ui == null)
            {
                Debug.LogError("can not find prefab " + id.ToString());
                return;
            }

            UIBase uiScript = GetUiScript(ui, id);
        }

        private GameObject GetPrefabObject(UiId id)
        {
            if(!_prefabDictionary.ContainsKey(id) || _prefabDictionary[id] == null)
            {
                _prefabDictionary[id] = LoadManager.Instance.Load<GameObject>(Path.UIPath, id.ToString());
            }

            return _prefabDictionary[id];
        }

        private UIBase GetUiScript(GameObject prefab, UiId id)
        {
            UIBase ui = prefab.GetComponent<UIBase>();

            if(ui == null)
            {
                return AddUiScript(prefab, id);
            }
            else
            {
                return ui;
            }
        }

        private UIBase AddUiScript(GameObject prefab, UiId id)
        {
            string scriptName = id + ConstValue.UI_SCRIPT_POSTFIX;
            Type ui = Type.GetType(scriptName);
            if (ui == null)
            {
                Debug.LogError("can not find script " + scriptName);
                return null;
            }
            return prefab.AddComponent(ui) as UIBase;
        }
    }
}
