using Const;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class MainMenuView : BasicUI
    {
        public override List<Transform> GetBtnParents()
        {
            List<Transform> list = new List<Transform>();
            list.Add(transform.GetBtnParent());
            return list;
        }

        public override UiId GetUiId()
        {
            return UiId.MainMenu;
        }

        protected override void Init()
        {
            base.Init();
            transform.AddBtnListener("StartGame", () => { RootManager.Instance.Show(UiId.StartGame); });
            transform.AddBtnListener("DOJO", () => { });
            transform.AddBtnListener("Help", () => { });
            transform.AddBtnListener("ExitGame", () => { Application.Quit(); });
        }
    }
}
