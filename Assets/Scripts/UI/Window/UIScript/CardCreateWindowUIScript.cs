using System;
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
    [SerializeField] protected InputField _nameInputField;
    [SerializeField] protected InputField _costInputField;
    [SerializeField] protected InputField _unevolvedAttackInputField;
    [SerializeField] protected InputField _unevolvedDefenseInputField;
    [SerializeField] protected InputField _unevolvedDescriptionInputField;
    [SerializeField] protected InputField _evolvedAttackInputField;
    [SerializeField] protected InputField _evolvedDefenseInputField;
    [SerializeField] protected InputField _evolvedDescriptionInputField;
    [SerializeField] protected Text _unevolvedAttackText;
    [SerializeField] protected Text _unevolvedDefenseText;

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

        _editNameButton.OnClickIntentAsObservable()
            .Do(_ =>
            {
                _nameInputField.interactable = true;
                _nameInputField.ActivateInputField();
            })
            .Subscribe();

        _nameInputField.OnEndEditAsObservable()
            .DelayFrame(1)
            .Do(_ => _nameInputField.interactable = false)
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
            .Do(_ => _costInputField.interactable = false)
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
            .Do(_ =>
            {
                _unevolvedAttackText.text = _unevolvedAttackInputField.text;
                _unevolvedAttackInputField.interactable = false;
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
            .Do(_ =>
            {
                _unevolvedDefenseText.text = _unevolvedDefenseInputField.text;
                _unevolvedDefenseInputField.interactable = false;
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
            .Do(_ => _unevolvedDescriptionInputField.interactable = false)
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
            .Do(_ => _evolvedAttackInputField.interactable = false)
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
            .Do(_ => _evolvedDefenseInputField.interactable = false)
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
            .Do(_ => _evolvedDescriptionInputField.interactable = false)
            .Subscribe();
    }

    private void ResizeCardSize()
    {
        var deviceWidth = UIManager.Instance.canvas.GetComponent<RectTransform>().sizeDelta.x;
        var deviceHeight = UIManager.Instance.canvas.GetComponent<RectTransform>().sizeDelta.y;
        var widthRatio = deviceWidth / CARD_WIDTH;
        var heightRatio = deviceHeight / CARD_HEIGHT;

        Debug.Log($"width:{deviceWidth} height:{deviceHeight}");

        // 比率が小さい方に合わせてサイズ調整
        var ratio = Math.Min(widthRatio, heightRatio);
        _cardRT.localScale = new Vector2(ratio, ratio);
    }

    private void RefreshCardUI()
    {
        RefreshClass();
        RefreshRarity();
        RefreshName();
        RefreshCost();
        RefreshUnevolvedAttack();
        RefreshUnevolvedDefense();
        RefreshUnevolvedDescription();
        RefreshEvolvedAttack();
        RefreshEvolvedDefense();
        RefreshEvolvedDescription();
    }

    private void RefreshClass()
    {

    }

    private void RefreshRarity()
    {

    }

    private void RefreshName()
    {
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
