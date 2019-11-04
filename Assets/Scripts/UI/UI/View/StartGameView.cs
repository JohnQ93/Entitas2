using Const;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Manager;

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
            transform.AddBtnListener("Continue", () => LoadScene(true));
            transform.AddBtnListener("Easy", () =>
            {
                LoadScene(false);
                DataManager.Single.DifficultLevel = DifficultLevel.EASY;
            });
            transform.AddBtnListener("Normal", () =>
            {
                LoadScene(false);
                DataManager.Single.DifficultLevel = DifficultLevel.NORMAL;
            });
            transform.AddBtnListener("Hard", () =>
            {
                LoadScene(false);
                DataManager.Single.DifficultLevel = DifficultLevel.HARD;
            });
        }

        protected override void Show()
        {
            base.Show();
            SetContinueBtnState();
        }

        private void SetContinueBtnState()
        {
            bool exist = DataManager.Single.JudgeExistData();
            transform.GetBtnParent().Find("Continue").gameObject.SetActive(exist);
        }

        private void LoadScene(bool isContinue)
        {
            if (isContinue)
            {
                ContinueGame();
            }
            else
            {
                NewGame();
            }
        }

        private void ContinueGame()
        {
            RootManager.Instance.Show(UiId.Loading);
        }

        private void NewGame()
        {
            bool exist = DataManager.Single.JudgeExistData();
            if (exist)
            {
                RootManager.Instance.Show(UiId.NewGameWarning);
            }
            else
            {
                RootManager.Instance.Show(UiId.Loading);
            }
        }
    }
}
