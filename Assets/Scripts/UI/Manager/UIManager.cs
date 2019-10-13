using UnityEngine;
using Const;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace UIFrame
{
    public class UIManager : MonoBehaviour
    {
        private readonly Dictionary<UiId, GameObject> _prefabDictionary = new Dictionary<UiId, GameObject>();
        private readonly Stack<UIBase> _uiStack = new Stack<UIBase>();
        private Func<UILayer, Transform> GetLayerObject;

        public Tuple<Transform, Transform> Show(UiId id)
        {
            GameObject ui = GetPrefabObject(id);
            if (ui == null)
            {
                Debug.LogError("can not find prefab " + id.ToString());
                return null;
            }

            UIBase uiScript = GetUiScript(ui, id);
            if (uiScript == null)
                return null;

            // 初始化UI对应的父物体
            InitUi(uiScript);

            Transform hideUI = null;
            if (uiScript.Layer == UILayer.BASIC_UI)
            {
                uiScript.uiState = UIState.SHOW;
                hideUI = Hide();
            }
            else
            {
                uiScript.uiState = UIState.SHOW;
            }
            
            _uiStack.Push(uiScript);

            return new Tuple<Transform, Transform>(ui.transform, hideUI);
        }

        private Transform Hide()
        {
            if(_uiStack.Count != 0)
            {
                _uiStack.Peek().uiState = UIState.HIDE;
                return _uiStack.Peek().transform;
            }
            return null;
        }

        public Tuple<Transform, Transform> Back()
        {
            if (_uiStack.Count > 1)
            {
                UIBase hideUI = _uiStack.Pop();
                Transform showUI = null;
                if (hideUI.Layer == UILayer.BASIC_UI)
                {
                    hideUI.uiState = UIState.HIDE;
                    _uiStack.Peek().uiState = UIState.SHOW;
                    showUI = _uiStack.Peek().transform;
                }
                else
                {
                    hideUI.uiState = UIState.HIDE;
                }
                return new Tuple<Transform, Transform>(showUI, hideUI.transform);
            }
            else
            {
                Debug.LogError("uistack has not more than one element");
                return null;
            }
        }

        public void AddGetLayerObjectListener(Func<UILayer, Transform> fun)
        {
            if (fun == null)
            {
                Debug.LogError("GetLayerObject function can not be null");
                return;
            }
            GetLayerObject = fun;
        }

        private void InitUi(UIBase uiScript)
        {
            if (uiScript.uiState == UIState.NORMAL)
            {
                Transform ui = uiScript.transform;
                ui.SetParent(GetLayerObject?.Invoke(uiScript.Layer));
                ui.localPosition = Vector3.zero;
                ui.localScale = Vector3.one;
            }
        }

        private GameObject GetPrefabObject(UiId id)
        {
            if(!_prefabDictionary.ContainsKey(id) || _prefabDictionary[id] == null)
            {
                GameObject prefab = LoadManager.Instance.Load<GameObject>(Path.UIPath, id.ToString());
                if (prefab != null)
                {
                    _prefabDictionary[id] = Instantiate(prefab);
                }
                else
                {
                    Debug.LogError("can not find prefab name : " + id);
                }
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
            string scriptName = ConstValue.UI_NAMESPACE_NAME + "." + id + ConstValue.UI_SCRIPT_POSTFIX;
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
