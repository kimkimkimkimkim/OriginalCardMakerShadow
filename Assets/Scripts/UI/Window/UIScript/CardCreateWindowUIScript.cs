using System;
using System.Collections.Generic;
using GameBase;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[ResourcePath("UI/Window/Window-CardCreate")]
public class CardCreateWindowUIScript : WindowBase
{
    const float CARD_WIDTH = 1920.0f;
    const float CARD_HEIGHT = 1076.0f;

    [SerializeField] protected Button _backButton;
    [SerializeField] protected Button _createButton;
    [SerializeField] protected RectTransform _cardParentRT;
    [SerializeField] protected RectTransform _cardRT;
    [SerializeField] protected Button _editClassButton;
    [SerializeField] protected Button _editNameButton;
    [SerializeField] protected Button _editRarityButton;
    [SerializeField] protected Button _editCostButton;
    [SerializeField] protected Button _editPhotoButton;
    [SerializeField] protected Button _editUnevolvedAttackButton;
    [SerializeField] protected Button _editUnevolvedDefenseButton;
    [SerializeField] protected Button _editUnevolvedDescriptionButton;
    [SerializeField] protected Button _editEvolvedDescriptionButton;
    [SerializeField] protected Button _editEvolvedAttackButton;
    [SerializeField] protected Button _editEvolvedDefenseButton;
    [SerializeField] protected Button _editDiscriptionButton;
    [SerializeField] protected InputField _nameInputField;
    [SerializeField] protected InputField _costInputField;
    [SerializeField] protected InputField _unevolvedAttackInputField;
    [SerializeField] protected InputField _unevolvedDefenseInputField;
    [SerializeField] protected InputField _unevolvedDescriptionInputField;
    [SerializeField] protected InputField _evolvedAttackInputField;
    [SerializeField] protected InputField _evolvedDefenseInputField;
    [SerializeField] protected InputField _evolvedDescriptionInputField;
    [SerializeField] protected InputField _discriptionInputField;
    [SerializeField] protected Text _unevolvedAttackText;
    [SerializeField] protected Text _unevolvedDefenseText;
    [SerializeField] protected Image _backgroundImage;
    [SerializeField] protected Image _cardImage;
    [SerializeField] protected RawImage _photoRawImage;
    [SerializeField] protected GameObject _followerTextPanel;
    [SerializeField] protected GameObject _spellAndAmuletTextPanel;
    [SerializeField] protected GameObject _followerButtonPanel;
    [SerializeField] protected GameObject _spellAndAmuletButtonPanel;

    private CardInfo cardInfo = new CardInfo();

    public override void Init(WindowInfo info)
    {
        var onClickClose = (Action)info.param["onClickClose"];

        RefreshCardUI();
        ResizeCardSize();

        _backButton.OnClickIntentAsObservable(ButtonClickIntent.OnlyOneTap)
            .SelectMany(_ => UIManager.Instance.CloseWindowObservable())
            .Do(_ => onClickClose())
            .Subscribe();

        _createButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                var cardWholeItem = UIManager.Instance.CreateContent<CardWholeItem>(PhotoManager.Instance._cardWholeParent);
                cardWholeItem.Resize(PhotoManager.Instance._cardWholeParent);
                cardWholeItem.RefreshUI(cardInfo);

                var cardAloneItem = UIManager.Instance.CreateContent<CardAloneItem>(PhotoManager.Instance._cardAloneParent);
                cardAloneItem.Resize(PhotoManager.Instance._cardAloneParent);
                cardAloneItem.RefreshUI(cardInfo);
            })
            .SelectMany(_ => CardConfirmWindowFactory.Create(new CardConfirmWindowRequest() { cardInfo = cardInfo}))
            .Subscribe();

        _editClassButton.OnClickIntentAsObservable()
            .SelectMany(_ => CardSelectClassDialogFactory.Create(new CardSelectClassDialogRequest() { cardClass = cardInfo.cardClass}))
            .Where(res => res.responseType == CommonDialogResponseType.Yes)
            .Do(res =>
            {
                cardInfo.cardClass = res.cardClass;
                RefreshPanel();
                RefreshClass();
                RefreshTypeAndRarity();
            })
            .Subscribe();

        _editRarityButton.OnClickIntentAsObservable()
            .SelectMany(_ => CardSelectTypeAndRarityDialogFactory.Create(new CardSelectTypeAndRarityDialogRequest() { 
                type = cardInfo.type,
                rarity = cardInfo.rarity,
            }))
            .Where(res => res.responseType == CommonDialogResponseType.Yes)
            .Do(res =>
            {
                cardInfo.type = res.type;
                cardInfo.rarity = res.rarity;
                RefreshPanel();
                RefreshClass();
                RefreshTypeAndRarity();
            })
            .Subscribe();

        _editNameButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _nameInputField.interactable = true;
                _nameInputField.ActivateInputField();
            })
            .Subscribe();

        _nameInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _nameInputField.interactable = false;
                cardInfo.name = text;
                RefreshName();
            })
            .Subscribe();

        _editCostButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _costInputField.interactable = true;
                _costInputField.ActivateInputField();
            })
            .Subscribe();

        _costInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _costInputField.interactable = false;
                if (int.TryParse(text, out int cost)) cardInfo.cost = cost;
                RefreshCost();
            })
            .Subscribe();

        _editUnevolvedAttackButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _unevolvedAttackInputField.interactable = true;
                _unevolvedAttackInputField.ActivateInputField();
            })
            .Subscribe();

        _unevolvedAttackInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _unevolvedAttackInputField.interactable = false;
                if (int.TryParse(text, out int attack)) cardInfo.unevolvedAttack = attack;
                RefreshUnevolvedAttack();
            })
            .Subscribe();

        _editUnevolvedDefenseButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _unevolvedDefenseInputField.interactable = true;
                _unevolvedDefenseInputField.ActivateInputField();
            })
            .Subscribe();

        _unevolvedDefenseInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _unevolvedDefenseInputField.interactable = false;
                if (int.TryParse(text, out int defense)) cardInfo.unevolvedDefense = defense;
                RefreshUnevolvedDefense();
            })
            .Subscribe();

        _editUnevolvedDescriptionButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _unevolvedDescriptionInputField.interactable = true;
                _unevolvedDescriptionInputField.ActivateInputField();
            })
            .Subscribe();

        _unevolvedDescriptionInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _unevolvedDescriptionInputField.interactable = false;
                cardInfo.unevolvedDescription = text;
                RefreshUnevolvedDescription();
            })
            .Subscribe();

        _editEvolvedAttackButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _evolvedAttackInputField.interactable = true;
                _evolvedAttackInputField.ActivateInputField();
            })
            .Subscribe();

        _evolvedAttackInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _evolvedAttackInputField.interactable = false;
                if (int.TryParse(text, out int attack)) cardInfo.evolvedAttack = attack;
                RefreshEvolvedAttack();
            })
            .Subscribe();

        _editEvolvedDefenseButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _evolvedDefenseInputField.interactable = true;
                _evolvedDefenseInputField.ActivateInputField();
            })
            .Subscribe();

        _evolvedDefenseInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _evolvedDefenseInputField.interactable = false;
                if (int.TryParse(text, out int defense)) cardInfo.evolvedDefense = defense;
                RefreshEvolvedDefense();
            })
            .Subscribe();

        _editEvolvedDescriptionButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _evolvedDescriptionInputField.interactable = true;
                _evolvedDescriptionInputField.ActivateInputField();
            })
            .Subscribe();

        _evolvedDescriptionInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _evolvedDescriptionInputField.interactable = false;
                cardInfo.evolvedDescription = text;
                RefreshEvolvedDescription();
            })
            .Subscribe();

        _editDiscriptionButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _discriptionInputField.interactable = true;
                _discriptionInputField.ActivateInputField();
            })
            .Subscribe();

        _discriptionInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(text =>
            {
                _discriptionInputField.interactable = false;
                cardInfo.unevolvedDescription = text;
                RefreshUnevolvedDescription();
            })
            .Subscribe();

        _editPhotoButton.OnClickIntentAsObservable()
            .SelectMany(_ => CardSelectPhotoDialogFactory.Create(new CardSelectPhotoDialogRequest()))
            .Do(res =>
            {
                switch (res.photoType) {
                    case PhotoType.Camera:
                        PhotoManager.Instance.TakePhotoAndCrop(0.8f, (texture) =>
                        {
                            cardInfo.photoTexture = texture;
                            RefreshPhoto();
                        });
                        break;
                    case PhotoType.Library:
                        PhotoManager.Instance.PickAndCrop(0.8f, (texture) =>
                         {
                             cardInfo.photoTexture = texture;
                             RefreshPhoto();
                         });
                        break;
                    case PhotoType.None:
                    default:
                        break;
                }
            })
            .Subscribe();
    }

    private void ResizeCardSize()
    {
        var deviceWidth = _cardParentRT.rect.width;
        var deviceHeight = _cardParentRT.rect.height;
        var widthRatio = deviceWidth / CARD_WIDTH;
        var heightRatio = deviceHeight / CARD_HEIGHT;

        // 比率が小さい方に合わせてサイズ調整
        var ratio = Math.Min(widthRatio, heightRatio);
        _cardRT.localScale = new Vector2(ratio, ratio);
    }

    private void RefreshCardUI()
    {
        RefreshPanel();
        RefreshClass();
        RefreshTypeAndRarity();
        RefreshName();
        RefreshCost();
        RefreshUnevolvedAttack();
        RefreshUnevolvedDefense();
        RefreshUnevolvedDescription();
        RefreshEvolvedAttack();
        RefreshEvolvedDefense();
        RefreshEvolvedDescription();
    }

    private void RefreshPanel()
    {
        var isFollower = cardInfo.type == Type.Follower;
        _followerTextPanel.SetActive(isFollower);
        _followerButtonPanel.SetActive(isFollower);
        _spellAndAmuletTextPanel.SetActive(!isFollower);
        _spellAndAmuletButtonPanel.SetActive(!isFollower);
    }

    private void RefreshClass()
    {
        _backgroundImage.sprite = OricameResourceManager.Instance.GetCardBackgroundSprite(cardInfo.cardClass,cardInfo.type);
    }

    private void RefreshTypeAndRarity()
    {
        _backgroundImage.sprite = OricameResourceManager.Instance.GetCardBackgroundSprite(cardInfo.cardClass, cardInfo.type);
        _cardImage.sprite = OricameResourceManager.Instance.GetCardSprite(cardInfo.cardClass, cardInfo.type, cardInfo.rarity);
    }

    private void RefreshName()
    {
        var isShort = cardInfo.name.Length <= 5;
        var right = isShort ? -36 : 0;
        _nameInputField.textComponent.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        _nameInputField.textComponent.GetComponent<RectTransform>().offsetMax = new Vector2(right, 0);
        _nameInputField.textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
        _nameInputField.text = cardInfo.name;
    }

    private void RefreshCost()
    {
        _costInputField.text = cardInfo.cost.ToString();
    }

    private void RefreshUnevolvedAttack()
    {
        _unevolvedAttackInputField.text = cardInfo.unevolvedAttack.ToString();
        _unevolvedAttackText.text = cardInfo.unevolvedAttack.ToString();
    }

    private void RefreshUnevolvedDefense()
    {
        _unevolvedDefenseInputField.text = cardInfo.unevolvedDefense.ToString();
        _unevolvedDefenseText.text = cardInfo.unevolvedDefense.ToString();
    }

    private void RefreshUnevolvedDescription()
    {
        _unevolvedDescriptionInputField.text = cardInfo.unevolvedDescription;
        _discriptionInputField.text = cardInfo.unevolvedDescription;
    }

    private void RefreshEvolvedAttack()
    {
        _evolvedAttackInputField.text = cardInfo.evolvedAttack.ToString();
    }

    private void RefreshEvolvedDefense()
    {
        _evolvedDefenseInputField.text = cardInfo.evolvedDefense.ToString();
    }

    private void RefreshEvolvedDescription()
    {
        _evolvedDescriptionInputField.text = cardInfo.evolvedDescription;
    }

    private void RefreshPhoto()
    {
        if(cardInfo.photoTexture != null)_photoRawImage.texture = cardInfo.photoTexture;
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
