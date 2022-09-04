using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AssetBundleDemoAsync
{
    public class AssetBundleLoaderLocal : AssetBundleLoader
    {
        private readonly IAssetBundleContainer _assetBundleContainer;
        
        
        #region Constructor

        public AssetBundleLoaderLocal(AssetBundleInfo assetBundleInfo, IAssetBundleContainer assetBundleContainer) : base(assetBundleInfo)
        {
            _assetBundleContainer = assetBundleContainer;
        }

        #endregion

        
        #region Override: OnLoad

        protected override async UniTask OnLoad()
        {
            Debug.Log($"[{GetType().Name}] - OnLoad: Started (AssetBundle: {_assetBundleInfo.Name})");
            
            OnLoadStartedEvent?.Invoke(this);
            
            var assetBundlePath = Path.Combine(_assetBundleInfo.LocalLocation, _assetBundleInfo.Name);
            var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
            
            OnLoadProgress(assetBundleCreateRequest);

            await assetBundleCreateRequest;

            Debug.Log($"[{GetType().Name}] - OnLoad: Loading (AssetBundle: {_assetBundleInfo.Name}) (Progress:{assetBundleCreateRequest.progress:F1})");
            OnLoadProgressEvent?.Invoke(this, assetBundleCreateRequest.progress);
            
            var assetBundle = assetBundleCreateRequest.GetAwaiter().GetResult();
            _assetBundleContainer.RegisterAssetBundle(assetBundle);
            
            Debug.Log($"[{GetType().Name}] - OnLoad: Completed (AssetBundle: {_assetBundleInfo.Name}) (AssetBundle: Name: {assetBundle.name})");
            OnLoadCompletedEvent?.Invoke(this);
        }
    
        #endregion
    }
}