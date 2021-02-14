using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using GameBase;
using System;

public class CardSelectPhotoDialogFactory
{
    public static IObservable<CardSelectPhotoDialogResponse> Create(CardSelectPhotoDialogRequest request)
    {
        return Observable.Create<CardSelectPhotoDialogResponse>(observer => {
            var param = new Dictionary<string, object>();
            param.Add("onClose", new Action<PhotoType>((type) => {
                observer.OnNext(new CardSelectPhotoDialogResponse() { photoType = type });
                observer.OnCompleted();
            }));
            UIManager.Instance.OpenDialog<CardSelectPhotoDialogUIScript>(param, true);
            return Disposable.Empty;
        });
    }
}