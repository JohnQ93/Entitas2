using Const;
using Util;
using UnityEngine;
using DG.Tweening;

namespace UIFrame
{
    public class WarningEffect : UIEffectBase
    {
        public override void Enter()
        {
            base.Enter();
            Init();

            transform.RectTransform().DOAnchorPosX(0, 1);
        }

        private void Init()
        {
            _defaultAnchorPos = new Vector2(1547f, 0);
            transform.RectTransform().anchoredPosition = _defaultAnchorPos;
        }

        public override void Exit()
        {
            transform.RectTransform().DOAnchorPosX(_defaultAnchorPos.x, 1);
        }

        public override UiEffect GetEffectLevel()
        {
            return UiEffect.VIEW_EFFECT;
        }
    }
}
