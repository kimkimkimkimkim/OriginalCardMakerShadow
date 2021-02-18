using UnityEngine;

public class CardInfo
{
    public CardClass cardClass { get; set; }
    public Type type { get; set; }
    public string name { get; set; } = "";
    public Rarity rarity { get; set; }
    public int cost { get; set; }
    public int unevolvedAttack { get; set; }
    public int unevolvedDefense { get; set; }
    public string unevolvedDescription { get; set; }
    public int evolvedAttack { get; set; }
    public int evolvedDefense { get; set; }
    public string evolvedDescription { get; set; }
    public Texture photoTexture { get; set; }
}

public enum CardClass
{
    Neutral = 0,
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
    Bronze = 0,
    Silver,
    Gold,
    Legend,
}

public enum Type
{
    Follower = 0,
    Spell,
    Amulet,
}
