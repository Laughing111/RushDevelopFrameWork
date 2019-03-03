using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrefab : MonoBehaviour
{
    public Transform uiroot;
    // Start is called before the first frame update
    void Start()
    {
        AssetBundle ab= AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/prefab");
        GameObject gameObject= ab.LoadAsset<GameObject>("prefab1");
        Instantiate(gameObject).transform.SetParent(uiroot,false);
        AssetBundle.UnloadAllAssetBundles(true);
    }

}
