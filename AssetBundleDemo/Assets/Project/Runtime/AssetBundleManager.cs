using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AssetBundleDemoAsync
{
    public class AssetBundleManager
    {
        private readonly IAssetBundleContainer _assetBundleContainer;
        private readonly List<AssetBundleModule> _assetBundleModuleList;
        
        
        #region Constructor

        public AssetBundleManager(IAssetBundleContainer assetBundleContainer)
        {
            _assetBundleContainer = assetBundleContainer;
            _assetBundleModuleList = new List<AssetBundleModule>();
        }

        #endregion
        

        #region AssetBundle: Download

        public async void Download()
        {
            Debug.Log($"[{GetType().Name}] - Download: Start: All");
            
            var tasks = new List<UniTask>(_assetBundleModuleList.Count);
            
            foreach (var assetBundleModule in _assetBundleModuleList)
            {
                tasks.Add(assetBundleModule.Download());
            }
            
            await UniTask.WhenAll(tasks);
            
            Debug.Log($"[{GetType().Name}] - Download: Complete: All");
        }

        public async void Download(string assetBundleName)
        {
            Debug.Log($"[{GetType().Name}] - Download: Start: (AssetBundle: {assetBundleName})");
            
            var assetBundleModule = GetAssetBundleModule(assetBundleName);
            await assetBundleModule.Download();
            
            Debug.Log($"[{GetType().Name}] - Download: Complete: (AssetBundle: {assetBundleName})");
        }
        
        #endregion
        
        #region AssetBundle: Load

        public async void Load()
        {
            Debug.Log($"[{GetType().Name}] - Load: Start: All");
            
            var tasks = new List<UniTask>();
            
            foreach (var assetBundleModule in _assetBundleModuleList)
            {
                tasks.Add(assetBundleModule.Load());
            }
            
            await UniTask.WhenAll(tasks);
            
            Debug.Log($"[{GetType().Name}] - Load: Complete: All");
        }

        public async void Load(string assetBundleName)
        {
            Debug.Log($"[{GetType().Name}] - Load: Start: (AssetBundle: {assetBundleName})");
            
            var assetBundleModule = GetAssetBundleModule(assetBundleName);
            await assetBundleModule.Load();
            
            Debug.Log($"[{GetType().Name}] - Load: Complete: (AssetBundle: {assetBundleName})");
        }
        
        #endregion

        #region AssetBundle: Unload

        public void Unload(bool unloadAllLoadedObjects)
        {
            Debug.Log($"[{GetType().Name}] - Unload: Start: All");
            
            foreach (var assetBundleModule in _assetBundleModuleList)
            {
                assetBundleModule.Unload(unloadAllLoadedObjects);
            }
            
            Debug.Log($"[{GetType().Name}] - Unload: Complete: All");
        }
        
        public void Unload(string assetBundleName, bool unloadAllLoadedObjects)
        {
            Debug.Log($"[{GetType().Name}] - Unload: Start: (AssetBundle: {assetBundleName})");
            
            var assetBundleModule = GetAssetBundleModule(assetBundleName);
            assetBundleModule.Unload(unloadAllLoadedObjects);
            
            Debug.Log($"[{GetType().Name}] - Unload: Completed: (AssetBundle: {assetBundleName})");
        }

        #endregion

        #region AssetBundle: Save

        public void Save()
        {
            Debug.Log($"[{GetType().Name}] - Save: Start: All");
            
            foreach (var assetBundleModule in _assetBundleModuleList)
            {
                assetBundleModule.Save();
            }
            
            Debug.Log($"[{GetType().Name}] - Save: Complete: All");
        }
        
        public void Save(string assetBundleName)
        {
            Debug.Log($"[{GetType().Name}] - Save: Start: (AssetBundle: {assetBundleName})");
            
            var assetBundleModule = GetAssetBundleModule(assetBundleName);
            assetBundleModule.Save();
            
            Debug.Log($"[{GetType().Name}] - Save: Completed: (AssetBundle: {assetBundleName})");
        }

        #endregion
        
        #region AssetBundle: Delete

        public void Delete()
        {
            Debug.Log($"[{GetType().Name}] - Delete: Start: All");
            
            foreach (var assetBundleModule in _assetBundleModuleList)
            {
                assetBundleModule.Delete();
            }
            
            Debug.Log($"[{GetType().Name}] - Delete: Complete: All");
        }
        
        public void Delete(string assetBundleName)
        {
            Debug.Log($"[{GetType().Name}] - Delete: Start: (AssetBundle: {assetBundleName})");
            
            var assetBundleModule = GetAssetBundleModule(assetBundleName);
            assetBundleModule.Delete();
            
            Debug.Log($"[{GetType().Name}] - Delete: Completed: (AssetBundle: {assetBundleName})");
        }

        #endregion
        

        #region AssetBundleModule: Register

        public AssetBundleModule RegisterAssetBundleModule(AssetBundleInfo assetBundleInfo)
        {
            var assetBundleModule = new  AssetBundleModule(assetBundleInfo, _assetBundleContainer);
            _assetBundleModuleList.Add(assetBundleModule);

            return assetBundleModule;
        }

        #endregion

        #region AssetBundleModule: Get

        private AssetBundleModule GetAssetBundleModule(string assetBundleName)
        {
            return _assetBundleModuleList.Find(x => x.AssetBundleInfo.Name == assetBundleName);
        }

        #endregion
    }
}