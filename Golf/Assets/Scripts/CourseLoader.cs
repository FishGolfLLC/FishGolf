using System.Collections;
using UnityEngine;
using System.IO;

public class CourseLoader : MonoBehaviour
{
    // FOR NOW: load objects using scriptable objects in scene - when they are more solidified we can swap to external asset packs


    private void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        string assetName = "testCourse";
        string bundle = "course_1";

        AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundle));
        yield return bundleRequest;

        AssetBundle localAssetBundle = bundleRequest.assetBundle;

        if (localAssetBundle != null)
        {
            var assetRequest = localAssetBundle.LoadAssetAsync<Course>(assetName);
            yield return assetRequest;

            Debug.Log("loaded asset: " + assetRequest.asset.name);
        }
        else Debug.LogError("cannot find bundle");

        localAssetBundle.Unload(false);
    }
}
