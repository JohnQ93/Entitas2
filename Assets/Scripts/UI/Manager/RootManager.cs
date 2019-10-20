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
        private BtnStateManager _btnManager;
        private void Awake()
        {
            Instance = this;
            _uiManager = gameObject.AddComponent<UIManager>();
            _effectManager = gameObject.AddComponent<UIEffectManager>();
            _layerManager = gameObject.AddComponent<UILayerManager>();
            _inputManager = gameObject.AddComponent<InputManager>();
            _btnManager = gameObject.AddComponent<BtnStateManager>();

            _uiManager.AddGetLayerObjectListener(_layerManager.GetLayerObject);
            _uiManager.AddInitCallBackListener((uiTrans) =>
            {
                var list = _uiManager.GetDefaultBtnTrans(uiTrans);
                _btnManager.InitBtnParent(list);
            });
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

        public void ButtonLeft()
        {
            _btnManager.Left();
        }

        public void ButtonRight()
        {
            _btnManager.Right();
        }

        public void SelectedButton()
        {
            _btnManager.SelectedButton();
        }

        private void ExecuteEffect(Tuple<Transform, Transform> uiParam)
        {
            _effectManager.Show(uiParam.Item1);
            _effectManager.Hide(uiParam.Item2);
            _btnManager.Show(uiParam.Item1);
            _btnManager.Hide(uiParam.Item2);
        }
    }
}
