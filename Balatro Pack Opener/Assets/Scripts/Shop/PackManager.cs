using UnityEngine;

namespace NS_Shop
{
    public static class PackManager
    {
        private static Pack[] packs;

        static PackManager()
        {
            packs = GameObject.FindObjectsOfType<Pack>();
        }

        public static void HidePacks()
        {
            foreach (Pack pack in packs)
            {
                pack.HidePack();
            }
        }

        public static void ShowPacks()
        {
            foreach (Pack pack in packs)
            {
                pack.ShowPack();
            }
        }
    }
}
