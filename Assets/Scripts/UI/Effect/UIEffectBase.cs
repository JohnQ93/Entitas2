using UnityEngine;
using System;
using Const;
using Util;

namespace UIFrame
{
    public abstract class UIEffectBase : MonoBehaviour
    {
        protected Vector2 _defaultAnchorPos = new Vector2(0,0);

        protected Action _onEnterComplete;
        protected Action _onExitComplete;
        public virtual void Enter()
        {
            if (_defaultAnchorPos == Vector2.zero)
            {
                _defaultAnchorPos = transform.RectTransform().anchoredPosition;
            }
        }
        public abstract void Exit();
        public abstract UiEffect GetEffectLevel();

        public virtual void OnEnterComplete(Action enterAction)
        {
            _onEnterComplete = enterAction;
        }

        public virtual void OnExitComplete(Action exitAction)
        {
            _onExitComplete = exitAction;
        }
    }
}
