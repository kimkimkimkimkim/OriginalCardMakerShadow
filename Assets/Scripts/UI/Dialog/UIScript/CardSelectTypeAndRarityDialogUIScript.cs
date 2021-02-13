using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using GameBase;

[ResourcePath("UI/Dialog/Dialog-CardSelectTypeAndRarity")]
public class CardSelectTypeAndRarityDialogUIScript : DialogBase
{
    [SerializeField] protected Button _closeButton;
    [SerializeField] protected Button _okButton;
    [SerializeField] protected List<Toggle> _followerToggleList; // RarityEnum順に格納
    [SerializeField] protected List<Toggle> _spellToggleList; // RarityEnum順に格納
    [SerializeField] protected List<Toggle> _amuletToggleList; // RarityEnum順に格納

    private Type type;
    private Rarity rarity;

    public override void Init(DialogInfo info)
    {
        var onClickClose = (Action)info.param["onClickClose"];
        var onClickOk = (Action<Type,Rarity>)info.param["onClickOk"];
        var initialType = (Type)info.param["type"];
        var initialRarity = (Rarity)info.param["rarity"];
        type = (Type)info.param["type"];
        rarity = (Rarity)info.param["rarity"];

        _closeButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseDialogObservable())
            .Do(_ => {
                if (onClickClose != null)
                {
                    onClickClose();
                    onClickClose = null;
                }
            })
            .Subscribe();

        _okButton.OnClickIntentAsObservable()
            .SelectMany(_ => UIManager.Instance.CloseDialogObservable())
            .Do(_ => {
                if (onClickOk != null)
                {
                    onClickOk(type,rarity);
                    onClickOk = null;
                }
            })
            .Subscribe();

        _followerToggleList.ForEach((toggle, index) =>
        {
            toggle.OnValueChangedAsObservable()
                .Do(isOn =>
                {
                    if (isOn)
                    {
                        type = Type.Follower;
                        rarity = (Rarity)index;
                    }
                })
                .Subscribe();
        });

        _spellToggleList.ForEach((toggle, index) =>
        {
            toggle.OnValueChangedAsObservable()
                .Do(isOn =>
                {
                    if (isOn)
                    {
                        type = Type.Spell;
                        rarity = (Rarity)index;
                    }
                })
                .Subscribe();
        });

        _amuletToggleList.ForEach((toggle, index) =>
        {
            toggle.OnValueChangedAsObservable()
                .Do(isOn =>
                {
                    if (isOn)
                    {
                        type = Type.Amulet;
                        rarity = (Rarity)index;
                    }
                })
                .Subscribe();
        });

        ActivateToggle(initialType, initialRarity);
    }

    private void ActivateToggle(Type type,Rarity rarity)
    {
        var toggleList = 
            type == Type.Follower ? _followerToggleList :
            type == Type.Spell ? _spellToggleList :
            _amuletToggleList;

        toggleList[(int)rarity].isOn = true;
    }

    public override void Back(DialogInfo info)
    {
    }
    public override void Close(DialogInfo info)
    {
    }
    public override void Open(DialogInfo info)
    {
    }
}