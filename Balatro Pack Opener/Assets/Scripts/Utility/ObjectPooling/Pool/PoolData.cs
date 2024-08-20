using UnityEngine;

[CreateAssetMenu(fileName = "PoolData", menuName = "ObjectPooling/Pool", order = 1)]
public class PoolData : ScriptableObject
{
    [SerializeField]
    private string poolKey;
    [SerializeField]
    private Object obj; // It could be both GameObject and ScriptableObject with GameObject data.
                        // Object is the generic serializable type, and group GameObject and ScriptableObject.
    [SerializeField]
    private int poolNumber;
    [SerializeField]
    private bool resizablePool;
    [SerializeField]
    private bool activeAtStart;

    public string PoolKey
    {
        get { return poolKey; }
    }

    public Object Obj
    {
        get { return obj; }
    }

    public int PoolNumber
    {
        get { return poolNumber; }
    }

    public bool ResizablePool
    {
        get { return resizablePool; }
    }

    public bool ActiveAtStart
    {
        get { return activeAtStart; }
    }

    private void OnValidate() // Called whenever a serialized field has going changed.
    {
        if (obj is GameObject || obj is ScriptableObject) return;
        Debug.LogError("Didn't set a GameObject or ScriptableObject! You set the type: " + obj.GetType().ToString());
    }
}
