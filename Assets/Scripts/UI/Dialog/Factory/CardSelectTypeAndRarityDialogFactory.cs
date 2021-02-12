using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using GameBase;
using System;

public class CardSelectTypeAndRarityDialogFactory
{
    public static IObservable<CardSelectTypeAndRarityDialogResponse> Create(CardSelectTypeAndRarityDialogRequest request)
    {
        return Observable.Create<CardSelectTypeAndRarityDialogResponse>(observer => {
            var param = new Dictionary<string, object>();
            param.Add("type", request.type);
            param.Add("rarity", request.rarity);
            param.Add("onClickClose", new Action(() => {
                observer.OnNext(new CardSelectTypeAndRarityDialogResponse() { responseType = CommonDialogResponseType.No });
                observer.OnCompleted();
            }));
            param.Add("onClickOk", new Action<Type,Rarity>((type,rarity) => {
                observer.OnNext(new CardSelectTypeAndRarityDialogResponse() { 
                    responseType = CommonDialogResponseType.Yes, 
                    type = type,
                    rarity = rarity,
                 });
                observer.OnCompleted();
            }));
            UIManager.Instance.OpenDialog<CardSelectTypeAndRarityDialogUIScript>(param, true);
            return Disposable.Empty;
        });
    }
}