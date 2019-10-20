using Const;
using UnityEngine;
using DG.Tweening;

namespace UIFrame
{
    public class StartGameBtnBgEffect : UIEffectBase
    {
        public override void Enter()
        {
            base.Enter();
            transform.DOScaleY(1, 1);
        }
        public override void Exit()
        {
            transform.DOScaleY(0, 1);
        }

        public override UiEffect GetEffectLevel()
        {
            return UiEffect.OTHERS_EFFECT;
        }
    }
}
