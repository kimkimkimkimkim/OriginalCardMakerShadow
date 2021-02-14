using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using GameBase;

[ResourcePath("UI/Dialog/Dialog-CardSelectPhoto")]
public class CardSelectPhotoDialogUIScript : DialogBase
{
    [SerializeField] protected Button _closeButton;
    [SerializeField] protected Button _cameraButton;
    [SerializeField] protected Button _libraryButton;

    public override void Init(DialogInfo info)
    {
        var onClose = (Action<PhotoType>)info.param["onClose"];

        _closeButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseDialogObservable())
            .Do(_ => {
                if (onClose != null)
                {
                    onClose(PhotoType.None);
                    onClose = null;
                }
            })
            .Subscribe();

        _cameraButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseDialogObservable())
            .Do(_ => {
                if (onClose != null)
                {
                    onClose(PhotoType.Camera);
                    onClose = null;
                }
            })
            .Subscribe();

        _libraryButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseDialogObservable())
            .Do(_ => {
                if (onClose != null)
                {
                    onClose(PhotoType.Library);
                    onClose = null;
                }
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