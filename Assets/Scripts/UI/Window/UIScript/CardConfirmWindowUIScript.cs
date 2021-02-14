using System;
using System.Collections.Generic;
using GameBase;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[ResourcePath("UI/Window/Window-CardConfirm")]
public class CardConfirmWindowUIScript : WindowBase
{
    [SerializeField] protected RenderTexture _cardWholeRenderTexture;
    [SerializeField] protected RenderTexture _cardAloneRenderTexture;
    [SerializeField] protected Button _backButton;
    [SerializeField] protected Button _homeButton;
    [SerializeField] protected Button _shareButton;
    [SerializeField] protected Button _cardWholeSaveButton;
    [SerializeField] protected Button _cardAloneSaveButton;
    [SerializeField] protected RectTransform _cardWholeParent;
    [SerializeField] protected RectTransform _cardAloneParent;

    private bool isCardWholeSaved;
    private bool isCardAloneSaved;
    private CardInfo cardInfo;

    public override void Init(WindowInfo info)
    {
        var onClickClose = (Action)info.param["onClickClose"];
        cardInfo = (CardInfo)info.param["cardInfo"];

        _backButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseWindowObservable())
            .Do(_ => onClickClose())
            .Subscribe();

        _homeButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseWindowObservable())
            .SelectMany(_ => UIManager.Instance.CloseWindowObservable())
            .Do(_ => onClickClose())
            .Subscribe();

        _shareButton.OnClickIntentAsObservable()
            .Subscribe();

        _cardWholeSaveButton.OnClickIntentAsObservable()
            .SelectMany(_ =>
            {
                if (isCardWholeSaved)
                {
                    return CommonDialogFactory.Create(new CommonDialogRequest() { body = "保存済みです" }).AsUnitObservable();
                }
                else
                {
                    return Observable.ReturnUnit().Do(res => SaveImage(true));
                }
            })
            .Subscribe();

        _cardAloneSaveButton.OnClickIntentAsObservable()
            .SelectMany(_ =>
            {
                if (isCardAloneSaved)
                {
                    return CommonDialogFactory.Create(new CommonDialogRequest() { body = "保存済みです" }).AsUnitObservable();
                }
                else
                {
                    return Observable.ReturnUnit().Do(res => SaveImage(false));
                }
            })
            .Subscribe();

        var cardWholeItem = UIManager.Instance.CreateContent<CardWholeItem>(_cardWholeParent);
        cardWholeItem.Resize(_cardWholeParent);
        cardWholeItem.RefreshUI(cardInfo);

        var cardAloneItem = UIManager.Instance.CreateContent<CardAloneItem>(_cardAloneParent);
        cardAloneItem.Resize(_cardAloneParent);
        cardAloneItem.RefreshUI(cardInfo);
    }

    private void SaveImage(bool isWhole)
    {
        var renderTexture = isWhole ? _cardWholeRenderTexture : _cardAloneRenderTexture;

        Observable.ReturnUnit()
            .Select(res =>
            {
                var texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false, false);
                RenderTexture.active = renderTexture;
                texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                texture.Apply();

                // Save the screenshot to Gallery/Photos
                NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(texture, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));

                return permission;
            }) 
            .SelectMany(res =>
            {
                if (res == NativeGallery.Permission.Granted)
                {
                    if (isWhole)
                    {
                        isCardWholeSaved = true;
                    }
                    else 
                    {
                        isCardAloneSaved = true;
                    }

                    //PlayerPrefsUtil.AddTodaySavedCount();
                    return CommonDialogFactory.Create(new CommonDialogRequest() { body = "画像を保存しました"}).AsUnitObservable();
                }
                else
                {
                    return CommonDialogFactory.Create(new CommonDialogRequest() { body = "画像の保存に失敗しました\nアルバムへの許可がされているかどうか確認してみてください"}).AsUnitObservable();
                }
            })
            //.Do(_ => RefreshTodaySavedCountText())
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
