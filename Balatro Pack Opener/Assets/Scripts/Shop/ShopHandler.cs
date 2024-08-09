using System.Collections.Generic;
using UnityEngine;
using System;

namespace NS_Shop
{
    public class ShopHandler : MonoBehaviour
    {
        public Func<Dictionary<Collectable, bool>> OnGetCollectionData;
    }
}