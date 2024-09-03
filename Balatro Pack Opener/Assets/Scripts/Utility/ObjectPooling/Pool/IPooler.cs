using UnityEngine;

namespace NS_ObjectPooler
{
    public interface IPooler
    {
        GameObject GetGameObject(); // Handling ScriptableObjects.
    }
}
