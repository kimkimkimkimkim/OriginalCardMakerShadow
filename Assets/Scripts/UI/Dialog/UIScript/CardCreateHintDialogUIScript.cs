using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using GameBase;

[ResourcePath("UI/Dialog/Dialog-CardCreateHint")]
public class CardCreateHintDialogUIScript : DialogBase
{
    [SerializeField] protected Button _closeButton;
    [SerializeField] protected Button _copyButton;

    public override void Init(DialogInfo info)
    {
        var onClickClose = (Action)info.param["onClickClose"];

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

        _copyButton.OnClickIntentAsObservable()
            .SelectMany(_ =>
            {
                //クリップボードにコピーする文字列
                var coptyText = "<color=#BBA669></color>";

                //クリップボードへ文字を設定(コピー)
                GUIUtility.systemCopyBuffer = coptyText;

                return CommonDialogFactory.Create(new CommonDialogRequest()
                {
                    type = CommonDialogType.OnlyOk,
                    title = "確認",
                    body = $"クリップボードに以下の文字列を貼り付けました\n{coptyText}",
                });
            })
            .Subscribe();
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