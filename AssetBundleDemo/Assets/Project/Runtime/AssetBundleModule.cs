using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AssetBundleDemoAsync
{
    public class AssetBundleModule 
    {
        #region Actions: Download

        public Action<AssetBundleModule> OnDownloadStartedEvent;
        public Action<AssetBundleModule, float> OnDownloadProgressEvent;
        public Action<AssetBundleModule> OnDownloadCompletedEvent;

        #endregion
        
        #region Actions: Load

        public Action<AssetBundleModule> OnLoadStartedEvent;
        public Action<AssetBundleModule, float> OnLoadProgressEvent;
        public Action<AssetBundleModule> OnLoadCompletedEvent;

        #endregion
        
        private readonly AssetBundleInfo _assetBundleInfo;
        public AssetBundleInfo AssetBundleInfo => _assetBundleInfo;

        private readonly IAssetBundleContainer _assetBundleContainer;
        
        private readonly AssetBundleLoaderRemote _assetBundleLoaderRemote;
        private readonly AssetBundleLoaderLocal _assetBundleLoaderLocal;


        #region Constructor

        public AssetBundleModule(AssetBundleInfo assetBundleInfo, IAssetBundleContainer assetBundleContainer)
        {
            _assetBundleInfo = assetBundleInfo;
            _assetBundleContainer = assetBundleContainer;
            
            _assetBundleLoaderRemote = new AssetBundleLoaderRemote(_assetBundleInfo);
            _assetBundleLoaderLocal = new AssetBundleLoaderLocal(_assetBundleInfo, assetBundleContainer);
        }

        #endregion


        #region Action: Download

        public UniTask Download()
        {
            RemoveDownloadEvents();
            AddDownloadEvents();
            
            return _assetBundleLoaderRemote.Load();
        }

        private void OnDownloadStarted(AssetBundleLoader assetBundleLoader)
        {
            OnDownloadStartedEvent?.Invoke(this);
        }
        
        private void OnDownloadProgress(AssetBundleLoader assetBundleLoader, float progress)
        {
            OnDownloadProgressEvent?.Invoke(this, progress);
        }
        
        private void OnDownloadCompleted(AssetBundleLoader assetBundleLoader)
        {
            RemoveDownloadEvents();
            
            OnDownloadCompletedEvent?.Invoke(this);
        }

        
        private void AddDownloadEvents()
        {
            _assetBundleLoaderRemote.OnLoadStartedEvent += OnDownloadStarted;
            _assetBundleLoaderRemote.OnLoadProgressEvent += OnDownloadProgress;
            _assetBundleLoaderRemote.OnLoadCompletedEvent += OnDownloadCompleted;
        }
        
        private void RemoveDownloadEvents()
        {
            _assetBundleLoaderRemote.OnLoadStartedEvent -= OnDownloadStarted;
            _assetBundleLoaderRemote.OnLoadProgressEvent = OnDownloadProgress;
            _assetBundleLoaderRemote.OnLoadCompletedEvent -= OnDownloadCompleted;
        }
        
        #endregion

        #region Action: Load

        public UniTask Load()
        {
            RemoveLoadEvents();
            AddLoadEvents();
            
            return _assetBundleLoaderLocal.Load();
        }

        private void OnLoadStarted(AssetBundleLoader assetBundleLoader)
        {
            OnLoadStartedEvent?.Invoke(this);
        }
        
        private void OnLoadProgress(AssetBundleLoader assetBundleLoader, float progress)
        {
            OnLoadProgressEvent?.Invoke(this, progress);
        }
        
        private void OnLoadCompleted(AssetBundleLoader assetBundleLoader)
        {
            OnLoadCompletedEvent?.Invoke(this);
        }
        
        
        private void AddLoadEvents()
        {
            _assetBundleLoaderLocal.OnLoadStartedEvent += OnLoadStarted;
            _assetBundleLoaderLocal.OnLoadProgressEvent += OnLoadProgress;
            _assetBundleLoaderLocal.OnLoadCompletedEvent += OnLoadCompleted;
        }
        
        private void RemoveLoadEvents()
        {
            _assetBundleLoaderLocal.OnLoadStartedEvent -= OnLoadStarted;
            _assetBundleLoaderLocal.OnLoadProgressEvent -= OnLoadProgress;
            _assetBundleLoaderLocal.OnLoadCompletedEvent -= OnLoadCompleted;
        }
        
        #endregion

        #region Action: Unload

        public void Unload(bool unloadAllLoadedObjects)
        {
            var assetBundle = _assetBundleContainer.GetAssetBundle(_assetBundleInfo.Name);
            assetBundle.Unload(unloadAllLoadedObjects);
        }

        #endregion

        #region Action: Save

        public void Save()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_assetBundleInfo.LocalLocation)))
            {
                Directory.CreateDirectory(_assetBundleInfo.LocalLocation);
            }

            var replacedPath = _assetBundleInfo.LocalLocation.Replace("/", "\\");
            var fullPath = Path.Combine(replacedPath, _assetBundleInfo.Name);
            
            try
            {
                File.WriteAllBytes(fullPath, _assetBundleLoaderRemote.Data);
                
                Debug.Log($"[{GetType().Name}] - Save: (Path: {fullPath})");
            }
            catch (Exception e)
            {
                Debug.Log($"[{GetType().Name}] - Error Save: (Path: {fullPath})\n -Error: {e.Message}");
            }
        }

        #endregion

        #region Action: Delete

        public void Delete()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_assetBundleInfo.LocalLocation)))
            {
                return;
            }
            
            var replacedPath = _assetBundleInfo.LocalLocation.Replace("/", "\\");
            var fullPath = Path.Combine(replacedPath, _assetBundleInfo.Name);
                
            try
            {
                File.Delete(fullPath);
                
                Debug.Log($"[{GetType().Name}] - Delete: (Path: {fullPath})");
            }
            catch (Exception e)
            {
                Debug.Log($"[{GetType().Name}] - Error Delete: (Path: {fullPath})\n -Error: {e.Message}");
            }
        }

        #endregion
    }
}