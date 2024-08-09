using UnityEngine;

namespace NS_ObjectPooler
{
    public class PoolerRequest : MonoBehaviour
    {
        [SerializeField]
        private PoolData data;

        private void Awake()
        {
            Pooler.Instance.AddToPool(data);
        }
    }
}
