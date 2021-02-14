using System;
using GameBase;
using UnityEngine;
using UnityEngine.UI;

[ResourcePath("UI/Parts/Parts-CardAlone")]
public class CardAloneItem : MonoBehaviour
{
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected InputField _nameInputField;
    [SerializeField] protected InputField _costInputField;
    [SerializeField] protected InputField _unevolvedAttackInputField;
    [SerializeField] protected InputField _unevolvedDefenseInputField;
    [SerializeField] protected Image _cardImage;
    [SerializeField] protected RawImage _photoRawImage;
    [SerializeField] protected GameObject _followerTextPanel;

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
        _unevolvedDefenseInputField.text = cardInfo.unevolvedDefense.ToString();
        _cardImage.sprite = OricameResourceManager.Instance.GetCardSprite(cardInfo.cardClass, cardInfo.type, cardInfo.rarity);
        _photoRawImage.texture = cardInfo.photoTexture;

        var isFollower = cardInfo.type == Type.Follower;
        _followerTextPanel.SetActive(isFollower);
    }
}
