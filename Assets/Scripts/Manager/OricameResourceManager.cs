using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBase;

public class OricameResourceManager : SingletonMonoBehaviour<OricameResourceManager>
{
    public Sprite GetCardBackgroundSprite(CardClass cardClass,Type type)
    {
        var path = $"Image/Background/{cardClass.ToString()}/{type.ToString()}";
        var texture = Resources.Load<Sprite>(path);
        return texture;
    }

    public Sprite GetCardSprite(CardClass cardClass,Type type,Rarity rarity)
    {
        var path = $"Image/Card/{cardClass.ToString()}/{type.ToString()}_{rarity.ToString()}";
        var texture = Resources.Load<Sprite>(path);
        return texture;
    }
}
