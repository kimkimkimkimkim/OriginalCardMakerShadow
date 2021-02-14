using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using GameBase;

[ResourcePath("UI/Dialog/Dialog-Common")]
public class CommonDialogUIScript : DialogBase
{
    [SerializeField] protected GameObject _closeButtonBase;
    [SerializeField] protected GameObject _okButtonBase;
    [SerializeField] protected Button _closeButton;
    [SerializeField] protected Button _okButton;
    [SerializeField] protected Text _titleText;
    [SerializeField] protected Text _bodyText;

    public override void Init(DialogInfo info)
    {
        var onClickClose = (Action)info.param["onClickClose"];
        var onClickOk = (Action)info.param["onClickOk"];
        var type = (CommonDialogType)info.param["type"];
        var title = (string)info.param["title"];
        var body = (string)info.param["body"];

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
                    onClickOk();
                    onClickOk = null;
                }
            })
            .Subscribe();

        _titleText.text = title;
        _bodyText.text = body;

        switch (type)
        {
            case CommonDialogType.OkAndNo:
                _closeButtonBase.SetActive(true);
                _okButtonBase.SetActive(true);
                break;
            case CommonDialogType.OnlyOk:
                _closeButtonBase.SetActive(false);
                _okButtonBase.SetActive(true);
                break;
            default:
                break;
        }
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