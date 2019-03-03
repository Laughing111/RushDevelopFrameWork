using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class CheckUpdate : MonoBehaviour
{
    private string currentVersion;
    private string newVersion;
    private void Awake()
    {
        string json=File.ReadAllText(Application.streamingAssetsPath + "/AssetsVersion/version.json");
        currentVersion = json;
    }
    public void CheckUpdateVersion()
    {
        StartCoroutine(GetVersionOnline(CheckVersion));  

    }

    IEnumerator GetVersionOnline(Action<string> checkVersion)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://unity-rushdevelopframework.oss-cn-hangzhou.aliyuncs.com/TestAssets/version.json");
        yield return webRequest.SendWebRequest();
        if(webRequest.error!=null)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            newVersion = webRequest.downloadHandler.text;
            checkVersion(newVersion);
        }
        webRequest.Dispose();
    }

    void CheckVersion(string newVersion)
    {
        Transform pop = transform.GetChild(2);
        if (newVersion!=currentVersion)
        {
            Debug.Log("要更新");
            
            pop.gameObject.SetActive(true);
            pop.GetChild(0).GetComponent<Text>().text = "有新版本：" + newVersion;
            pop.GetChild(1).gameObject.SetActive(true);

        }
        else
        {
            pop.gameObject.SetActive(true);
            pop.GetChild(0).GetComponent<Text>().text = "已是最新版本";
            pop.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void DownLoadNewPrefab()
    {
        StartCoroutine(DownLoadPrefab());
    }

    IEnumerator DownLoadPrefab()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://unity-rushdevelopframework.oss-cn-hangzhou.aliyuncs.com/TestAssets/assets/prefab");
        yield return webRequest.SendWebRequest();
        if (webRequest.error != null)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            byte[] data = webRequest.downloadHandler.data;
            //if(File.Exists(Application.streamingAssetsPath + "/prefab.prefab"))
            //{
            //    File.Delete(Application.streamingAssetsPath + "/prefab.prefab");
            //}
            File.WriteAllBytes(Application.streamingAssetsPath + "/prefab",data);
            File.WriteAllText(Application.streamingAssetsPath + "/AssetsVersion/version.json", newVersion);
            yield return new WaitForSeconds(1.0f);
        }
        webRequest.Dispose();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    
}
