using System;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    private ShopHandler instance;

    public ShopHandler Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<ShopHandler>();
            return instance;
        }
    }

    public Action OnEndedPackOpening;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
