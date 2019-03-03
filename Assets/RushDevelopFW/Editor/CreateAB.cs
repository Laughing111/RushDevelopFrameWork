using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateAB
{

    private static string abUrl = "Assets/StreamingAssets"; 
    [MenuItem("R.D.Tools/AssetBundle/CreatAB")]
   static void CreatABAssets()
    {
        BuildPipeline.BuildAssetBundles(abUrl, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
    }
}
