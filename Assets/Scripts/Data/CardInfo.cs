using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo
{
    public CardClass cardClass { get; set; }
    public string name { get; set; }
    public Rarity rarity { get; set; }
    public int cost { get; set; }
    public int unevolvedAttack { get; set; }
    public int unevolvedDefense { get; set; }
    public string unevolvedDescription { get; set; }
    public int evolvedAttack { get; set; }
    public int evolvedDefense { get; set; }
    public string evolvedDescription { get; set; }
}

public enum CardClass
{
    Neutral,
    Elf,
    Royal,
    Witch,
    Dragon,
    Necromancer,
    Vampier,
    Bishop,
    Nemesis,
}

public enum Rarity
{
    Bronze,
    Silver,
    Gold,
    Legend,
}
