using Const;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class StartGameView : BasicUI
    {
        public override UiId GetUiId()
        {
            return UiId.StartGame;
        }

        public void Start()
        {
            transform.Find("Buttons/Continue").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Easy").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Normal").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Hard").RectTransform().AddBtnListener(() => { });
        }
    }
}
