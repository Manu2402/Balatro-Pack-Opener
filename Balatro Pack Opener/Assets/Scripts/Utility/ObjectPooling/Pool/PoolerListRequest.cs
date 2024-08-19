using NS_ObjectPooler;
using UnityEngine;

// Temp.
public enum PoolerListType
{
    Collectables
}

public class PoolerListRequest : MonoBehaviour
{
    [SerializeField]
    private PoolData[] poolDatas;

    [SerializeField]
    private PoolerListType poolerListType;

    private void Awake()
    {
        foreach(PoolData poolData in poolDatas)
        {
            Pooler.Instance.AddToPool(poolData);
        }
    }

    #region GetRandomData
    public PoolData GetRandomData()
    {
        return poolDatas[Random.Range(0, poolDatas.Length)];
    }

    public PoolData GetRandomData(int index)
    {
        if (index < 0 || index > poolDatas.Length - 1) return null;
        return poolDatas[index];
    }
    #endregion

    public int GetDataLength()
    {
        return poolDatas.Length;
    }

}
