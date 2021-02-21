using System;
using System.Collections.Generic;
using UniRx;
using GameBase;

public class CardCreateHintDialogFactory
{
    public static IObservable<CardCreateHintDialogResponse> Create(CardCreateHintDialogRequest request)
    {
        return Observable.Create<CardCreateHintDialogResponse>(observer => {
            var param = new Dictionary<string, object>();
            param.Add("onClickClose", new Action(() => {
                observer.OnNext(new CardCreateHintDialogResponse() {});
                observer.OnCompleted();
            }));
            UIManager.Instance.OpenDialog<CardCreateHintDialogUIScript>(param, true);
            return Disposable.Empty;
        });
    }
}
