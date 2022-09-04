using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace AssetBundleDemoAsync
{
    public static class AssetBundleName
    {
        public const string Cubes = "cubes";
        public const string Spheres = "spheres";
    }
    
    public class AssetBundleTest : MonoBehaviour
    {
        #region Content: UI: CubeBundle

        [Header("Content: UI: CubeBundle")]
        
        [SerializeField]
        private Slider _sliderProgressCubeBundle;
        
        [SerializeField]
        private Text _txtProgressCubeBundle;

        #endregion
        
        #region Content: UI: SphereBundle

        [Header("Content: UI: SphereBundle")]
        
        [SerializeField]
        private Slider _sliderProgressSphereBundle;
        
        [SerializeField]
        private Text _txtProgressSphereBundle;

        #endregion

        #region Content: Environment

        [Header("Content: Environment")]
        
        [SerializeField]
        private Transform _assetContainerTransform;

        #endregion
        
        private AssetBundleContainer _assetBundleContainer;
        private AssetBundleManager _assetBundleManager;

        private AssetBundleModule _cubeModule;
        private AssetBundleModule _sphereModule;
        

        #region Unity: Start

        private void Start()
        {
            Init();
        }

        #endregion

        #region Init

        private void Init()
        {
            _assetBundleContainer = new AssetBundleContainer();
            _assetBundleManager = new AssetBundleManager(_assetBundleContainer);
            
            var cubeAssetBundleInfo = new AssetBundleInfo
            {
                Name = AssetBundleName.Cubes,
                LocalLocation = Path.Combine(Application.persistentDataPath, "AssetBundles"),
                RemoteLocation = @"" // TODO: Set Remote Location Path
            };
            
            var sphereAssetBundleInfo = new AssetBundleInfo
            {
                Name = AssetBundleName.Spheres,
                LocalLocation = Path.Combine(Application.persistentDataPath, "AssetBundles"),
                RemoteLocation = @"" // TODO: Set Remote Location Path
            };

            _cubeModule = _assetBundleManager.RegisterAssetBundleModule(cubeAssetBundleInfo);
            _cubeModule.OnDownloadStartedEvent += OnCubeModuleDownloadStarted;
            _cubeModule.OnDownloadProgressEvent += OnCubeModuleDownloadProgress;
            _cubeModule.OnDownloadCompletedEvent += OnCubeModuleDownloadCompleted;
            _cubeModule.OnLoadStartedEvent += OnCubeModuleDownloadStarted;
            _cubeModule.OnLoadProgressEvent += OnCubeModuleDownloadProgress;
            _cubeModule.OnLoadCompletedEvent += OnCubeModuleDownloadCompleted;
            
            _sphereModule = _assetBundleManager.RegisterAssetBundleModule(sphereAssetBundleInfo);
            _sphereModule.OnDownloadStartedEvent += OnCubeModuleDownloadStarted;
            _sphereModule.OnDownloadProgressEvent += OnCubeModuleDownloadProgress;
            _sphereModule.OnDownloadCompletedEvent += OnCubeModuleDownloadCompleted;
            _sphereModule.OnLoadStartedEvent += OnCubeModuleDownloadStarted;
            _sphereModule.OnLoadProgressEvent += OnCubeModuleDownloadProgress;
            _sphereModule.OnLoadCompletedEvent += OnCubeModuleDownloadCompleted;
        }

        private void OnCubeModuleDownloadStarted(AssetBundleModule assetBundleModule)
        {
            if (assetBundleModule.AssetBundleInfo.Name == AssetBundleName.Cubes)
            {
                _sliderProgressCubeBundle.value = 0f;
            }
            else if (assetBundleModule.AssetBundleInfo.Name == AssetBundleName.Spheres)
            {
                _sliderProgressSphereBundle.value = 0f;
            }
        }
        
        private void OnCubeModuleDownloadProgress(AssetBundleModule assetBundleModule, float progress)
        {
            if (assetBundleModule.AssetBundleInfo.Name == AssetBundleName.Cubes)
            {
                _sliderProgressCubeBundle.value = progress;
                _txtProgressCubeBundle.text = $"{(int)(progress * 100)}%";
            }
            else if (assetBundleModule.AssetBundleInfo.Name == AssetBundleName.Spheres)
            {
                _sliderProgressSphereBundle.value = progress;
                _txtProgressSphereBundle.text = $"{(int)(progress * 100)}%";
            }
        }

        private void OnCubeModuleDownloadCompleted(AssetBundleModule assetBundleModule)
        {
            if (assetBundleModule.AssetBundleInfo.Name == AssetBundleName.Cubes)
            {
                _sliderProgressCubeBundle.value = 1;
                _txtProgressCubeBundle.text = "100";
            }
            else if (assetBundleModule.AssetBundleInfo.Name == AssetBundleName.Spheres)
            {
                _sliderProgressSphereBundle.value = 1;
                _txtProgressSphereBundle.text = "100";
            }
        }

        #endregion


        #region AssetBundle: Download

        public void DownloadAssetBundle_Cube()
        {
            _assetBundleManager.Download(AssetBundleName.Cubes);
        }
        
        public void DownloadAssetBundle_Sphere()
        {
            _assetBundleManager.Download(AssetBundleName.Spheres);
        }

        #endregion

        #region AssetBundle: Save

        public void SaveAssetBundle_Cube()
        {
            _assetBundleManager.Save(AssetBundleName.Cubes);
        }
        
        public void SaveAssetBundle_Sphere()
        {
            _assetBundleManager.Save(AssetBundleName.Spheres);
        }

        #endregion
        
        #region AssetBundle: Delete

        public void DeleteAssetBundle_Cube()
        {
            _assetBundleManager.Delete(AssetBundleName.Cubes);
        }
        
        public void DeleteAssetBundle_Sphere()
        {
            _assetBundleManager.Delete(AssetBundleName.Spheres);
        }

        #endregion
        
        #region AssetBundle: Load

        public void LoadAssetBundle_Cube()
        {
            _assetBundleManager.Load(AssetBundleName.Cubes);
        }
        
        public void LoadAssetBundle_Sphere()
        {
            _assetBundleManager.Load(AssetBundleName.Spheres);
        }

        #endregion
        
        #region AssetBundle: Unload

        public void UnloadAssetBundle_Cube()
        {
            _assetBundleManager.Unload(AssetBundleName.Cubes, true);
        }
        
        public void UnloadAssetBundle_Sphere()
        {
            _assetBundleManager.Unload(AssetBundleName.Spheres, true);
        }

        #endregion
        
        
        #region Asset: Load: Cube_1 | Cube_2

        public void LoadAsset_Cube_1()
        {
            var asset = _assetBundleContainer.LoadAsset<GameObject>(AssetBundleName.Cubes, "Cube_1");
            var goAsset = Instantiate(asset, Vector3.zero, Quaternion.identity, _assetContainerTransform);
            goAsset.transform.localPosition = new Vector3(-2.2f, 0f, 0f);
        }
        
        public void LoadAsset_Cube_2()
        {
            var asset = _assetBundleContainer.LoadAsset<GameObject>(AssetBundleName.Cubes, "Cube_2");
            var goAsset = Instantiate(asset, Vector3.zero, Quaternion.identity, _assetContainerTransform);
            goAsset.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
        }

        #endregion
        
        #region Asset: Load: Sphere_1 | Sphere_2

        public void LoadAsset_Sphere_1()
        {
            var asset = _assetBundleContainer.LoadAsset<GameObject>(AssetBundleName.Spheres, "Sphere_1");
            var goAsset = Instantiate(asset, Vector3.zero, Quaternion.identity, _assetContainerTransform);
            goAsset.transform.localPosition = new Vector3(1f, 0f, 0f);
        }
        
        public void LoadAsset_Sphere_2()
        {
            var asset = _assetBundleContainer.LoadAsset<GameObject>(AssetBundleName.Spheres, "Sphere_2");
            var goAsset = Instantiate(asset, Vector3.zero, Quaternion.identity, _assetContainerTransform);
            goAsset.transform.localPosition = new Vector3(2.5f, 0f, 0f);
        }

        #endregion
    }
}