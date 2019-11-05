using UnityEngine;
using Util;
using DG.Tweening;

namespace UIFrame
{
    public class ComicsItem : MonoBehaviour
    {
        public int Page { get; private set; }
        public void Init(Sprite sprite, int page)
        {
            if (transform.Image() != null)
            {
                transform.Image().sprite = sprite; 
            }
            Page = page;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void ResetParentAndPosition(Transform parent)
        {
            SetParent(parent);
            transform.RectTransform().DOKill();
            transform.RectTransform().anchoredPosition = Vector2.zero;
        }

        public void Move(Transform parent)
        {
            SetParent(parent);
            transform.RectTransform().DOKill();
            transform.RectTransform().DOAnchorPosX(0, 1).SetEase(Ease.Linear);
        }
    }
}
