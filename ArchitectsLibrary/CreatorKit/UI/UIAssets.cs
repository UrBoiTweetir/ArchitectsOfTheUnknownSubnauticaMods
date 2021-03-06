using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace CreatorKit.UI
{
    /// <summary>
    /// This static class contains a static reference to the AssetBundle and has some methods for obtaining commonly used UI Sprites.
    /// </summary>
    public static class UIAssets
    {
        public static AssetBundle assetBundle;
        public const string assetBundleName = "creationkitassets";

        public static void LoadAssetBundle()
        {
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", assetBundleName));
        }

        public static Sprite GetDefaultPackImage()
        {
            return assetBundle.LoadAsset<Sprite>("PackDefault");
        }

        public static GameObject GetPackLauncherPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("PackLauncher");
        }

        public static GameObject GetPackListButtonPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("PackListButton");
        }

        public static GameObject GetEditorLauncherPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("EditorLauncher");
        }

        #region Language Editor
        public static GameObject GetLanguageEditorPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("LanguageEditor");
        }
        public static GameObject GetFilteredListButtonPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("FilteredListButton");
        }
        #endregion

        public static GameObject GetMultipleChoicePopupPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("MultipleChoicePopup");
        }

        public static GameObject GetMultipleChoiceButtonPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("MultipleChoiceButton");
        }

        public static GameObject GetTextPopupPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("WriteTextPopup");
        }

        public static GameObject GetTextPopupInputPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("WriteTextInput");
        }

        public static GameObject GetEditorBackgroundPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("EditorBackground");
        }

        public static GameObject GetEditorHeaderPrefab()
        {
            return assetBundle.LoadAsset<GameObject>("EditorHeader");
        }
    }
}
