using UnityEditor;
using System.IO;
using UnityEngine;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string buildDirectory = "Assets/AssetBundles";
        var buildPath = Path.Combine(Application.dataPath, buildDirectory);

        if (Directory.Exists(buildDirectory))
        {
            Directory.Delete(buildDirectory, true);
        }
        Directory.CreateDirectory(buildDirectory);

        var result = BuildPipeline.BuildAssetBundles(buildDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        Debug.Log(result);
        /*
         * BuildAssetBundleOptions.None - nothing special
         * BuildAssetBundleOptions.DryRunBuild - does not build (good for error checking)
         * BuildAssetBundleOptions.StrictBuild - stops if any error occurs
         * BuildAssetBundleOptions.UncompressedAssetBundle - does not compress (good for testing)
         */
    }
}
