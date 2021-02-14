using System;
using System.Collections.Generic;
using GameBase;
using UniRx;

public class CardConfirmWindowFactory
{
    public static IObservable<CardConfirmWindowResponse> Create(CardConfirmWindowRequest request)
    {
        return Observable.Create<CardConfirmWindowResponse>(observer =>
        {
            var param = new Dictionary<string, object>();
            param.Add("cardInfo", request.cardInfo);
            param.Add("onClickClose", new Action(() =>
            {
                observer.OnNext(new CardConfirmWindowResponse());
                observer.OnCompleted();
            }));

            UIManager.Instance.OpenWindow<CardConfirmWindowUIScript>(param, animationType: WindowAnimationType.None);
            return Disposable.Empty;
        });
    }
}

