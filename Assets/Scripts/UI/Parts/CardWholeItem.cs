using System;
using GameBase;
using UnityEngine;
using UnityEngine.UI;

[ResourcePath("UI/Parts/Parts-CardWhole")]
public class CardWholeItem : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTransform;
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
    [SerializeField] protected Image _cardBackgroundImage;
    [SerializeField] protected Image _cardImage;
    [SerializeField] protected RawImage _photoRawImage;
    [SerializeField] protected GameObject _followerTextPanel;
    [SerializeField] protected GameObject _spellAndAmuletTextPanel;

    public void Resize(RectTransform parent)
    {
        var parentWidth = parent.rect.width;
        var parentHeight = parent.rect.height;
        var widthRatio = parentWidth / _rectTransform.rect.width;
        var heightRatio = parentHeight / _rectTransform.rect.height;

        // 比率が小さい方に合わせてサイズ調整
        var ratio = Math.Min(widthRatio, heightRatio);
        _rectTransform.localScale = new Vector2(ratio, ratio);
    }

    public void RefreshUI(CardInfo cardInfo)
    {
        _nameInputField.text = cardInfo.name;
        _costInputField.text = cardInfo.cost.ToString();
        _unevolvedAttackInputField.text = cardInfo.unevolvedAttack.ToString();
        _unevolvedAttackText.text = cardInfo.unevolvedAttack.ToString();
        _unevolvedDefenseInputField.text = cardInfo.unevolvedDefense.ToString();
        _unevolvedDefenseText.text = cardInfo.unevolvedDefense.ToString();
        _unevolvedDescriptionInputField.text = cardInfo.unevolvedDescription;
        _evolvedAttackInputField.text = cardInfo.evolvedAttack.ToString();
        _evolvedDefenseInputField.text = cardInfo.evolvedDefense.ToString();
        _evolvedDescriptionInputField.text = cardInfo.evolvedDescription;
        _discriptionInputField.text = cardInfo.unevolvedDescription;
        _cardBackgroundImage.sprite = OricameResourceManager.Instance.GetCardBackgroundSprite(cardInfo.cardClass, cardInfo.type);
        _cardImage.sprite = OricameResourceManager.Instance.GetCardSprite(cardInfo.cardClass,cardInfo.type,cardInfo.rarity);
        _photoRawImage.texture = cardInfo.photoTexture;

        var isFollower = cardInfo.type == Type.Follower;
        _followerTextPanel.SetActive(isFollower);
        _spellAndAmuletTextPanel.SetActive(!isFollower);
    }
}
