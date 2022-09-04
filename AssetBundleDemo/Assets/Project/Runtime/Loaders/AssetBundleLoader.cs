using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AssetBundleDemoAsync
{
    public abstract class AssetBundleLoader : IAssetBundleLoader
    {
        #region Actions: OnLoadStarted | OnLoadProgress | OnLoadCompleted

        public Action<AssetBundleLoader> OnLoadStartedEvent;
        public Action<AssetBundleLoader, float> OnLoadProgressEvent;
        public Action<AssetBundleLoader> OnLoadCompletedEvent;

        #endregion
        
        protected readonly AssetBundleInfo _assetBundleInfo;

        
        #region Constructor

        protected AssetBundleLoader(AssetBundleInfo assetBundleInfo)
        {
            _assetBundleInfo = assetBundleInfo;
        }

        #endregion
        
        #region IAssetBundleLoader: Load -> OnLoad

        public async UniTask Load()
        {
           await OnLoad();
        }

        protected abstract UniTask OnLoad();
        
        #endregion

        #region Load: OnLoadProgress

        protected async void OnLoadProgress(AsyncOperation asyncOperation)
        {
            while (asyncOperation.progress < 1f)
            {
                Debug.Log($"[{GetType().Name}] - OnLoad: Loading (AssetBundle: {_assetBundleInfo.Name}) (Progress:{asyncOperation.progress:F1})");
                OnLoadProgressEvent?.Invoke(this, asyncOperation.progress);
                
                await UniTask.Yield();
            }
        }

        #endregion
    }
}