using System;
using UnityEngine;
using Const;

namespace UIFrame
{
    public class UIEffectManager : MonoBehaviour
    {
        public void Show(Transform ui)
        {
            if (ui == null)
                return;
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                effectBase.Enter();
            }
        }

        public void Hide(Transform ui)
        {
            if (ui == null)
                return;
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                effectBase.Exit();
            }
        }

        public void AddViewEffectEnterListener(Transform ui, Action enterComplete)
        {
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                if(effectBase.GetEffectLevel() == UiEffect.VIEW_EFFECT)
                {
                    effectBase.OnEnterComplete(enterComplete);
                }
            }
        }

        public void AddViewEffectExitListener(Transform ui, Action exitComplete)
        {
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                if (effectBase.GetEffectLevel() == UiEffect.VIEW_EFFECT)
                {
                    effectBase.OnExitComplete(exitComplete);
                }
            }
        }
    }
}
