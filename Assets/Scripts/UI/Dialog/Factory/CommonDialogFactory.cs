using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using GameBase;
using System;

public class CommonDialogFactory
{
    public static IObservable<CommonDialogResponse> Create(CommonDialogRequest request)
    {
        return Observable.Create<CommonDialogResponse>(observer => {
            var param = new Dictionary<string, object>();
            param.Add("type", request.type);
            param.Add("title", request.title);
            param.Add("body", request.body);
            param.Add("onClickClose", new Action(() => {
                observer.OnNext(new CommonDialogResponse() {});
                observer.OnCompleted();
            }));
            param.Add("onClickOk", new Action(() => {
                observer.OnNext(new CommonDialogResponse()
                {
                });
                observer.OnCompleted();
            }));
            UIManager.Instance.OpenDialog<CommonDialogUIScript>(param, true);
            return Disposable.Empty;
        });
    }
}