using Const;
using UnityEngine;
using System;
using Manager;

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
        private UIAudioManager _audioManager;
        private void Awake()
        {
            Instance = this;
            _uiManager = gameObject.AddComponent<UIManager>();
            _effectManager = gameObject.AddComponent<UIEffectManager>();
            _layerManager = gameObject.AddComponent<UILayerManager>();
            _inputManager = gameObject.AddComponent<InputManager>();
            _btnManager = gameObject.AddComponent<BtnStateManager>();
            _audioManager = gameObject.AddComponent<UIAudioManager>();

            _uiManager.AddGetLayerObjectListener(_layerManager.GetLayerObject);
            _uiManager.AddInitCallBackListener((uiTrans) =>
            {
                var list = _uiManager.GetDefaultBtnTrans(uiTrans);
                _btnManager.InitBtnParent(list);
            });
            _audioManager.Init(Path.UI_AUDIO_PATH, LoadManager.Single.LoadAll<AudioClip>);
            _audioManager.PlayBg(UIAudioName.UI_bg.ToString());
        }
        private void Start()
        {
            Show(UiId.MainMenu);
        }
        public void Show(UiId id)
        {
            var uiParam = _uiManager.Show(id);
            ExecuteEffect(uiParam);
            ShowBtnState(uiParam.Item1);
        }

        public void Back()
        {
            var uiParam = _uiManager.Back();
            ExecuteEffect(uiParam);
            ShowBtnState(_uiManager.GetCurrentUiTrans());
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
            ShowUI(uiParam.Item1);
            HideUI(uiParam.Item2);
        }

        private void ShowUI(Transform showUI)
        {
            ShowEffect(showUI);
            ShowUIAudio();
        }

        private void HideUI(Transform hideUI)
        {
            HideEffect(hideUI);
            HideUIAudio();
        }

        private void ShowUIAudio()
        {
            _audioManager.Play(UIAudioName.UI_in.ToString());
        }

        private void HideUIAudio()
        {
            _audioManager.Play(UIAudioName.UI_out.ToString());
        }

        private void ShowEffect(Transform showUI)
        {
            if (showUI == null)
            {
                _effectManager.ShowOthersEffect(_uiManager.GetCurrentUiTrans());
            }
            else
            {
                _effectManager.Show(showUI);
            }
        }

        private void HideEffect(Transform hideUI)
        {
            if (hideUI == null)
            {
                _effectManager.HideOthersEffect(_uiManager.GetBasicUiTrans());
            }
            else
            {
                _effectManager.Hide(hideUI);
            }
        }

        private void ShowBtnState(Transform ui)
        {
            _btnManager.Show(ui);
        }

        public void PlayAudio(UIAudioName name)
        {
            _audioManager.Play(name.ToString());
        }
    }
}
