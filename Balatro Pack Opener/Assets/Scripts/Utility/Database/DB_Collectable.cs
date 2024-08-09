using UnityEngine;

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary,
    Unique
}

public enum CollectableType
{
    // Primary elements
    Joker,
    Planet,
    Tarot,
    Spectral,
    // Secondary elements
    Deck,
    Voucher,
    Tag,
    Blind
}

[CreateAssetMenu(fileName = "Collectable", menuName = "Database/Collectable", order = 1)]
public class DB_Collectable : ScriptableObject
{
    [SerializeField]
    private string collectableName;
    [SerializeField]
    private Rarity rarity;
    [SerializeField]
    private uint albumIndex;

    private uint amount;

    [SerializeField]
    private CollectableType type;
    [SerializeField]
    private float doubletReward;

    public string CollectableName { get { return collectableName; } }
    public Rarity Rarity { get { return rarity; } }
    public uint AlbumIndex { get { return albumIndex; } }
    public uint Amount { get { return amount; } }
    public CollectableType Type { get { return type; } }
    public float DoubletReward { get { return doubletReward; } }
}