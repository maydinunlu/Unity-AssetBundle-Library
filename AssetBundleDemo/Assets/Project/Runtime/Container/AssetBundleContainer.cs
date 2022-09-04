using System.Collections.Generic;
using UnityEngine;

namespace AssetBundleDemoAsync
{
    public class AssetBundleContainer : IAssetBundleContainer
    {
        private readonly Dictionary<string, AssetBundle> _assetBundles = new Dictionary<string, AssetBundle>();

        
        #region IAssetBundleContainer: AssetBundle: Register

        public void RegisterAssetBundle(AssetBundle assetBundle)
        {
            if (!_assetBundles.ContainsKey(assetBundle.name))
            {
                _assetBundles.Add(assetBundle.name, assetBundle);
            }
        }

        #endregion

        #region IAssetBundleContainer: AssetBundle: Get 

        public AssetBundle GetAssetBundle(string assetBundleName)
        {
            return _assetBundles[assetBundleName];
        }

        #endregion
        
        
        #region IAssetBundleContainer: Assset: Load

        public TAsset LoadAsset<TAsset>(string assetBundleName, string assetName)
            where TAsset: Object
        {
            var assetBundle = _assetBundles[assetBundleName];
            if (assetBundle != null)
            {
                var asset = assetBundle.LoadAsset<TAsset>(assetName);
                return asset;
            }

            return null;
        }

        #endregion
    }
}