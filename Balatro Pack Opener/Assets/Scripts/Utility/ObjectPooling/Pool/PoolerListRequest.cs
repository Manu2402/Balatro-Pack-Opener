using UnityEngine;
using System.Collections.Generic;

public enum PoolerListType
{
    Collectable
}

namespace NS_ObjectPooler
{
    public class PoolerListRequest : MonoBehaviour
    {
        [SerializeField]
        private PoolData[] poolDatas;
        [SerializeField]
        private PoolerListType poolerListType;

        public PoolerListType PoolerListType { get { return poolerListType; } }

        private void Awake()
        {
            foreach (PoolData poolData in poolDatas)
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

        public GameObject[] GetPoolPrefabs()
        {
            List<GameObject> prefabs = new List<GameObject>(GetDataLength());

            foreach (PoolData data in poolDatas)
            {
                if (data.Obj is IPooler)
                {
                    IPooler obj = data.Obj as IPooler;
                    prefabs.Add(obj.GetGameObject());
                    continue;
                }

                prefabs.Add(data.Obj as GameObject);
            }

            return prefabs.ToArray();
        }

    }
}
