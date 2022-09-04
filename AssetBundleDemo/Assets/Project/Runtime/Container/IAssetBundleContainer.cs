using UnityEngine;

namespace AssetBundleDemoAsync
{
    public interface IAssetBundleContainer
    {
        void RegisterAssetBundle(AssetBundle assetBundle);
        AssetBundle GetAssetBundle(string assetBundleName);
        
        TAsset LoadAsset<TAsset>(string assetBundleName, string assetName)
            where TAsset: Object;
    }
}