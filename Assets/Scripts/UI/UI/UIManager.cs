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
        private UILayerManager _layerManager;

        private void Awake()
        {
            _layerManager = GetComponent<UILayerManager>();
            if(_layerManager == null)
            {
                Debug.LogError("can not find UILayerManager");
            }
        }

        private async void Start()
        {
            Show(UiId.MainMenu);

            await Task.Delay(TimeSpan.FromSeconds(1));

            Show(UiId.StartGame);

            await Task.Delay(TimeSpan.FromSeconds(1));

            Back();
        }

        public void Show(UiId id)
        {
            GameObject ui = GetPrefabObject(id);
            if (ui == null)
            {
                Debug.LogError("can not find prefab " + id.ToString());
                return;
            }

            UIBase uiScript = GetUiScript(ui, id);
            if (uiScript == null)
                return;

            InitUi(uiScript);

            if (uiScript.Layer == UILayer.BASIC_UI)
            {
                uiScript.uiState = UIState.SHOW;
                Hide();
            }
            else
            {
                uiScript.uiState = UIState.SHOW;
            }

            _uiStack.Push(uiScript);
        }

        public void Hide()
        {
            if(_uiStack.Count != 0)
            {
                _uiStack.Peek().uiState = UIState.HIDE;
            }
        }

        public void Back()
        {
            if (_uiStack.Count > 1)
            {
                if (_uiStack.Peek().Layer == UILayer.BASIC_UI)
                {
                    _uiStack.Pop().uiState = UIState.HIDE;
                    _uiStack.Peek().uiState = UIState.SHOW;
                }
                else
                {
                    _uiStack.Pop().uiState = UIState.HIDE;
                }
            }
            else
            {
                Debug.LogError("uistack has not more than one element");
            }
        }

        public void InitUi(UIBase uiScript)
        {
            if (uiScript.uiState == UIState.NORMAL)
            {
                Transform ui = uiScript.transform;
                ui.SetParent(_layerManager.GetLayerObject(uiScript.Layer));
                ui.localPosition = Vector3.zero;
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
