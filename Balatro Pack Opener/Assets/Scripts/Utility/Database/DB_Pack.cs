using UnityEngine;

public enum PackType
{
    Arcana,
    Buffon,
    Celestial,
    Spectral
}

public enum BoostType
{
    Normal,
    Jumbo,
    Mega
}

[CreateAssetMenu(fileName = "Pack", menuName = "Database/Pack", order = 2)]
public class DB_Pack : ScriptableObject
{
    [SerializeField]
    private uint collectablesAmount;
    [SerializeField]
    private PackType packType;
    [SerializeField]
    private BoostType boostType;
    [SerializeField]
    private GameObject prefab;

    public uint CollectablesAmount { get { return collectablesAmount; } }
    public PackType PackType { get { return packType; } }
    public BoostType BoostType { get { return boostType; } }
    public GameObject Prefab { get { return prefab; } }
}
