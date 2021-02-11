using System;
using GameBase;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[ResourcePath("UI/Window/Window-CardCreate")]
public class CardCreateWindowUIScript : WindowBase
{
    [SerializeField] protected Button _backButton;

    public override void Init(WindowInfo info)
    {
        var onClickClose = (Action)info.param["onClickClose"];

        _backButton.OnClickIntentAsObservable(ButtonClickIntent.OnlyOneTap)
            .SelectMany(_ => UIManager.Instance.CloseWindowObservable())
            .Do(_ => onClickClose())
            .Subscribe();
    }

    public override void Open(WindowInfo info)
    {
    }

    public override void Back(WindowInfo info)
    {
    }

    public override void Close(WindowInfo info)
    {
    }
}
