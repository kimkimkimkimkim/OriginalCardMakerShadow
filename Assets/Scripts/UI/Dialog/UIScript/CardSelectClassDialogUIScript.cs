using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using GameBase;

[ResourcePath("UI/Dialog/Dialog-CardSelectClass")]
public class CardSelectClassDialogUIScript : DialogBase
{
    [SerializeField] protected Button _closeButton;
    [SerializeField] protected Button _okButton;
    [SerializeField] protected List<Toggle> _toggleList; // CardClassEnumの順に格納

    private CardClass cardClass;

    public override void Init(DialogInfo info)
    {
        var onClickClose = (Action)info.param["onClickClose"];
        var onClickOk = (Action<CardClass>)info.param["onClickOk"];
        var initialCardClass = (CardClass)info.param["cardClass"];
        cardClass = (CardClass)info.param["cardClass"];

        _closeButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseDialogObservable())
            .Do(_ => {
                if (onClickClose != null)
                {
                    onClickClose();
                    onClickClose = null;
                }
            })
            .Subscribe();

        _okButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseDialogObservable())
            .Do(_ => {
                if (onClickOk != null)
                {
                    onClickOk(cardClass);
                    onClickOk = null;
                }
            })
            .Subscribe();

        _toggleList.ForEach((toggle, index) =>
        {
            toggle.OnValueChangedAsObservable()
                .Do(isOn =>
                {
                    if (isOn) cardClass = (CardClass)index;
                })
                .Subscribe();
        });

        _toggleList[(int)initialCardClass].isOn = true;
    }

    public override void Back(DialogInfo info)
    {
    }
    public override void Close(DialogInfo info)
    {
    }
    public override void Open(DialogInfo info)
    {
    }
}