using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace AssetBundleDemoAsync
{
    public class AssetBundleLoaderRemote : AssetBundleLoader
    {
        private byte[] _data;
        public byte[] Data => _data;
        
        #region Constructor

        public AssetBundleLoaderRemote(AssetBundleInfo assetBundleInfo) : base(assetBundleInfo)
        {
            
        }

        #endregion
        
        
        #region Override: OnLoad

        protected override async UniTask OnLoad()
        {
            Debug.Log($"[{GetType().Name}] - OnLoad: Started: (AssetBundle: {_assetBundleInfo.Name})");
            
            OnLoadStartedEvent?.Invoke(this);
            
            var assetBundlePath = Path.Combine(_assetBundleInfo.RemoteLocation, _assetBundleInfo.Name);
            var assetBundleWebRequest = UnityWebRequest.Get(assetBundlePath).SendWebRequest();
            
            OnLoadProgress(assetBundleWebRequest);
            
            await assetBundleWebRequest;

            _data = assetBundleWebRequest.webRequest.downloadHandler.data;
            
            Debug.Log($"[{GetType().Name}] - OnLoad: Loading: (AssetBundle: {_assetBundleInfo.Name}) (Progress:{assetBundleWebRequest.progress:F1})");
            OnLoadProgressEvent?.Invoke(this, assetBundleWebRequest.progress);
            
            Debug.Log($"[{GetType().Name}] - OnLoad: Completed (AssetBundle: {_assetBundleInfo.Name}) (Downloaded Content: {ConvertBytesToMegabytes(assetBundleWebRequest.webRequest.downloadedBytes):0.00} mb)");
            OnLoadCompletedEvent?.Invoke(this);
        }
    
        private double ConvertBytesToMegabytes(ulong bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        
        #endregion
    }
}