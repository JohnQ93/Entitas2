using Const;
using UnityEngine;
using DG.Tweening;
using Util;

namespace UIFrame
{
    public class StartGameTitleEffect : UIEffectBase
    {
        public override void Enter()
        {
            base.Enter();
            transform.RectTransform().DOAnchorPosX(0, 1);
        }

        public override void Exit()
        {
            transform.RectTransform().DOAnchorPos(_defaultAnchorPos, 1);
        }

        public override UiEffect GetEffectLevel()
        {
            return UiEffect.OTHERS_EFFECT;
        }
    }
}
