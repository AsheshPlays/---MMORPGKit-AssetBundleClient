using UnityEngine;
using UnityEngine.UI;

namespace MultiplayerARPG
{
    public partial class UIAssetBundleLoading : MonoBehaviour
    {
        public string formatLoadingAssetBundleFileName = "{0}";
        public string formatLoadedAssetBundles = "{0}/{1}";
        public GameObject rootObject;
        public Text uiTextProgress;
        public Image imageGage;
        public GameObject totalRootObject;
        public Text uiTextTotalProgress;
        public Image imageGageTotal;
        public Text textLoadingAssetBundleFileName;
        public Text textLoadedAssetBundlesCount;

        private void Awake()
        {
            if (rootObject != null)
                rootObject.SetActive(false);

            if (totalRootObject != null)
                totalRootObject.SetActive(false);
        }

        private void Update()
        {
            switch (AssetBundleManager.Singleton.CurrentLoadState)
            {
                case AssetBundleManager.LoadState.None:
                case AssetBundleManager.LoadState.LoadManifest:
                case AssetBundleManager.LoadState.Done:
                    if (rootObject != null)
                        rootObject.SetActive(false);

                    if (totalRootObject != null)
                        totalRootObject.SetActive(false);

                    if (textLoadingAssetBundleFileName != null)
                        textLoadingAssetBundleFileName.text = string.Empty;
                    if (textLoadedAssetBundlesCount != null)
                        textLoadedAssetBundlesCount.text = string.Empty;
                    break;
                case AssetBundleManager.LoadState.LoadAssetBundles:
                    if (AssetBundleManager.Singleton.LoadedAssetBundlesCount > 0)
                    {
                        if (rootObject != null)
                            rootObject.SetActive(true);
                        if (uiTextProgress != null)
                            uiTextProgress.text = (AssetBundleManager.Singleton.CurrentWebRequest.downloadProgress * 100f).ToString("N2") + "%";
                        if (imageGage != null)
                            imageGage.fillAmount = AssetBundleManager.Singleton.CurrentWebRequest.downloadProgress;

                        if (totalRootObject != null)
                            totalRootObject.SetActive(true);
                        if (uiTextTotalProgress != null)
                            uiTextTotalProgress.text = (AssetBundleManager.Singleton.TotalLoadProgress * 100f).ToString("N2") + "%";
                        if (imageGageTotal != null)
                            imageGageTotal.fillAmount = AssetBundleManager.Singleton.TotalLoadProgress;

                        if (textLoadingAssetBundleFileName != null)
                            textLoadingAssetBundleFileName.text = string.Format(formatLoadingAssetBundleFileName, AssetBundleManager.Singleton.LoadingAssetBundleFileName);
                        if (textLoadedAssetBundlesCount != null)
                            textLoadedAssetBundlesCount.text = string.Format(formatLoadedAssetBundles, AssetBundleManager.Singleton.LoadedAssetBundlesCount, AssetBundleManager.Singleton.LoadingAssetBundlesCount);
                    }
                    break;
            }
        }
    }
}
