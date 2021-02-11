using System;
using System.Collections.Generic;
using GameBase;
using UniRx;

public class CardCreateWindowFactory
{
    public static IObservable<CardCreateWindowResponse> Create(CardCreateWindowRequest request)
    {
        return Observable.Create<CardCreateWindowResponse>(observer =>
        {
            var param = new Dictionary<string, object>();
            param.Add("onClickClose", new Action(() =>
            {
                observer.OnNext(new CardCreateWindowResponse());
                observer.OnCompleted();
            }));

            UIManager.Instance.OpenWindow<CardCreateWindowUIScript>(param, animationType: WindowAnimationType.None);
            return Disposable.Empty;
        });
    }
}
