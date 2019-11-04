using System.Collections.Generic;
using Const;
using Util;
using Manager;
using UnityEngine;

namespace UIFrame
{
    public class NewGameWarningView : OverlayUI
    {
        public override List<Transform> GetBtnParents()
        {
            List<Transform> list = new List<Transform>();
            list.Add(transform.GetBtnParent());
            return list;
        }

        public override UiId GetUiId()
        {
            return UiId.NewGameWarning;
        }

        protected override void Init()
        {
            base.Init();
            transform.AddBtnListener("Yes", () => 
            {
                DataManager.Single.ResetData();
                RootManager.Instance.Show(UiId.Loading);
            });
            transform.AddBtnListener("No", () => { RootManager.Instance.Back(); });
        }
    }
}
