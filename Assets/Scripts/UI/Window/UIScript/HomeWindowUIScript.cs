using GameBase;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[ResourcePath("UI/Window/Window-Home")]
public class HomeWindowUIScript : WindowBase
{
    [SerializeField] protected Button _cardCreateButton;

    public override void Init(WindowInfo info)
    {
        _cardCreateButton.OnClickIntentAsObservable()
            .SelectMany(_ => CardCreateWindowFactory.Create(new CardCreateWindowRequest()))
            .Subscribe();
    }

    public override void Open(WindowInfo info)
    {
    }

    public override void Back(WindowInfo info)
    {
    }

    public override void Close(WindowInfo info)
    {
    }
}
