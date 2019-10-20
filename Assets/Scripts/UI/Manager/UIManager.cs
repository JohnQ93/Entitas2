using UnityEngine;
using Const;
using System.Collections.Generic;
using System;
using Util;

namespace UIFrame
{
    public class UIManager : MonoBehaviour
    {
        private readonly Dictionary<UiId, GameObject> _prefabDictionary = new Dictionary<UiId, GameObject>();
        private readonly Stack<UIBase> _uiStack = new Stack<UIBase>();
        private Func<UILayer, Transform> GetLayerObject;
        private Action<Transform> InitCallBack;

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

            return new Tuple<Transform, Transform>(ui.transform, hideUI);   //Item1:要显示的UI， Item2:要隐藏的UI
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

        public List<Transform> GetDefaultBtnTrans(Transform showUI)
        {
            if(showUI != null)
            {
                return showUI.GetComponent<UIBase>().GetBtnParents();
            }
            else
            {
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

        public void AddInitCallBackListener(Action<Transform> callBack)
        {
            if(callBack == null)
            {
                Debug.LogError("InitCallBack function can not be null");
                return;
            }
            InitCallBack = callBack;
        }

        private void InitUi(UIBase uiScript)
        {
            if (uiScript.uiState == UIState.NORMAL)
            {
                Debug.Log("InitUi");
                Transform ui = uiScript.transform;
                ui.SetParent(GetLayerObject?.Invoke(uiScript.Layer));
                ui.localPosition = Vector3.zero;
                ui.localScale = Vector3.one;
                ui.RectTransform().offsetMax = Vector2.zero;
                ui.RectTransform().offsetMin = Vector2.zero;
                
                InitCallBack?.Invoke(ui);
            }
        }

        /// <summary>
        /// 加载要显示的UI预制体，如果加载过则从缓存中读取
        /// </summary>
        /// <param name="id">要加载的UiId</param>
        /// <returns>UI GameObject</returns>
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

        /// <summary>
        /// 获取实例化UI上的脚本，如果没有则根据UiId添加对应的UIBase脚本
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
