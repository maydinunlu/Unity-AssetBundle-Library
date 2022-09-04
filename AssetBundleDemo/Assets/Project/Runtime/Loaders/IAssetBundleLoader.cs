using Cysharp.Threading.Tasks;

namespace AssetBundleDemoAsync
{
    public interface IAssetBundleLoader
    {
        UniTask Load();
    }
}