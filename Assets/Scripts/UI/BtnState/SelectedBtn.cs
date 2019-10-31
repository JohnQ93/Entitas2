using UnityEngine;
using Util;
using DG.Tweening;
using Const;
using UnityEngine.EventSystems;
using System;

namespace UIFrame
{
    public class SelectedBtn : MonoBehaviour, IPointerEnterHandler
    {
        public SelectedState SelectedState 
        {
            set
            {
                switch (value)
                {
                    case SelectedState.SELECTED:
                        Selected();
                        break;
                    case SelectedState.UNSELECTED:
                        CancelSelected();
                        break;
                }
            }
        }

        public int Index
        {
            get { return transform.GetSiblingIndex(); }
        }

        private Color _defaultColor;
        private Action<SelectedBtn> _selectAction;

        private void Awake()
        {
            SaveDefaultColor(transform);
        }

        public void Selected()
        {
            if (!JudgeException(transform))
            {
                PlayEffect(transform);
            }
        }

        public void CancelSelected()
        {
            KillEffect(transform);
        }

        public void SelectedButton()
        {
            transform.Button().onClick.Invoke();
        }

        public void KillEffect(Transform btn)
        {
            if (btn == null)
                return;

            btn.Image().DOKill();
            btn.Image().color = _defaultColor;
        }

        public void AddSelectActionListener(Action<SelectedBtn> action)
        {
            _selectAction = action;
        }

        private bool JudgeException(Transform btn)
        {
            return btn.Button() == null || btn.Image() == null;
        }

        private void SaveDefaultColor(Transform btn)
        {
            //±£´æÄ¬ÈÏµÄColor
            _defaultColor = btn.Image().color;
        }

        private void PlayEffect(Transform btn)
        {
            btn.Image().DOColor(new Color32(0, 135, 255, 255), 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _selectAction?.Invoke(this);
            SelectedState = SelectedState.SELECTED;
        }
    }
}
