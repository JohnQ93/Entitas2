using System.Collections.Generic;
using Const;
using Manager;
using UnityEngine;

namespace UIFrame
{
    public class LoadingView : BasicUI
    {
        public override List<Transform> GetBtnParents()
        {
            return null;
        }

        public override UiId GetUiId()
        {
            return UiId.Loading;
        }

        protected override void Show()
        {
            base.Show();
            StartCoroutine(LoadSceneManager.Single.LoadSceneAsync(DataManager.Single.GetSceneName()));
            LoadSceneManager.Single.AllowSwitchScene();
        }
    }
}
