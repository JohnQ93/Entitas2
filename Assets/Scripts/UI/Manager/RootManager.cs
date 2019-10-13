using Const;
using UnityEngine;
using System;

namespace UIFrame
{
    public class RootManager : MonoBehaviour
    {
        public static RootManager Instance { get; private set; }
        private UIManager _uiManager;
        private UIEffectManager _effectManager;
        private UILayerManager _layerManager;
        private InputManager _inputManager;
        private void Awake()
        {
            Instance = this;
            _uiManager = gameObject.AddComponent<UIManager>();
            _effectManager = gameObject.AddComponent<UIEffectManager>();
            _layerManager = gameObject.AddComponent<UILayerManager>();
            _inputManager = gameObject.AddComponent<InputManager>();

            _uiManager.AddGetLayerObjectListener(_layerManager.GetLayerObject);
        }
        private void Start()
        {
            Show(UiId.MainMenu);
        }
        public void Show(UiId id)
        {
            var uiParam = _uiManager.Show(id);
            ExecuteEffect(uiParam);
        }

        public void Back()
        {
            var uiParam = _uiManager.Back();
            ExecuteEffect(uiParam);
        }

        private void ExecuteEffect(Tuple<Transform, Transform> uiParam)
        {
            _effectManager.Show(uiParam.Item1);
            _effectManager.Hide(uiParam.Item2);
        }
    }
}
