using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using GameBase;

public class CardSelectClassDialogFactory
{
    public static IObservable<CardSelectClassDialogResponse> Create(CardSelectClassDialogRequest request)
    {
        return Observable.Create<CardSelectClassDialogResponse>(observer => {
            var param = new Dictionary<string, object>();
            param.Add("onClickClose", new Action(() => {
                observer.OnNext(new CardSelectClassDialogResponse());
                observer.OnCompleted();
            }));
            UIManager.Instance.OpenDialog<CardSelectClassDialogUIScript>(param, true);
            return Disposable.Empty;
        });
    }
}