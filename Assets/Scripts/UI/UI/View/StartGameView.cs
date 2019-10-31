using Const;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Data;

namespace UIFrame
{
    public class StartGameView : BasicUI
    {
        public override List<Transform> GetBtnParents()
        {
            List<Transform> list = new List<Transform>();
            list.Add(transform.GetBtnParent());
            return list;
        }

        public override UiId GetUiId()
        {
            return UiId.StartGame;
        }

        protected override void Init()
        {
            base.Init();
            transform.AddBtnListener("Continue", () => { });
            transform.AddBtnListener("Easy", LoadScene);
            transform.AddBtnListener("Normal", LoadScene);
            transform.AddBtnListener("Hard", LoadScene);
        }

        protected override void Show()
        {
            base.Show();
            SetContinueBtnState();
        }

        private void SetContinueBtnState()
        {
            bool exist = DataManager.JudgeExistData();
            transform.GetBtnParent().Find("Continue").gameObject.SetActive(exist);
        }

        private void LoadScene()
        {
            bool exist = DataManager.JudgeExistData();
            if (exist)
            {
                RootManager.Instance.Show(UiId.NewGameWarning);
            }
            else
            {
                //todo
            }
        }
    }
}
