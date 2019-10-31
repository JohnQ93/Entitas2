using UnityEngine;

namespace Const
{
    /// <summary>
    /// UI�㼶
    /// </summary>
    public enum UILayer
    {
        BASIC_UI,
        OVERLAY_UI,
        TOP_UI
    }

    /// <summary>
    /// UI����״̬
    /// </summary>
    public enum UIState
    {
        NORMAL,
        INIT,
        SHOW,
        HIDE
    }

    public enum UiId
    {
        MainMenu,
        StartGame,
        NewGameWarning
    }

    public enum UiEffect
    {
        VIEW_EFFECT,
        OTHERS_EFFECT
    }

    public enum SelectedState
    {
        SELECTED,
        UNSELECTED
    }

    public enum UIAudioName
    {
        UI_bg,
        UI_click,
        UI_in,
        UI_logo_in,
        UI_logo_out,
        UI_out
    }

    public enum DifficultLevel
    {
        NONE,
        EASY,
        NORMAL,
        HARD
    }
}
