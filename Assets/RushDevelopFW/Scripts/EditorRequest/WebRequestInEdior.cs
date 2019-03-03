using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using RDFW;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;


namespace RDFW
{
    /// <summary>
    /// 配置文件类-Josn序列化
    /// </summary>
    [System.Serializable]
    public class ConfigJson
    {
        public string versionNum;
        public string log;
        public List<string> direct;
        public ConfigJson()
        {
            direct = new List<string>();
        }
    }
    /// <summary>
    /// 该类用来在编辑器下，使用WebRequest配合协程进行下载版本配置信息
    /// </summary>
#if UNITY_EDITOR
    [ExecuteInEditMode]
    public class WebRequestInEdior : MonoBehaviour
    {

        #region 单例模式
        private static WebRequestInEdior requestIns;
        private WebRequestInEdior()
        {

        }
        public static WebRequestInEdior GetIns()
        {
            if(requestIns==null)
            {
                GameObject gameObject = new GameObject("WebRequest");
                requestIns=gameObject.AddComponent<WebRequestInEdior>();
            }
            return requestIns;
        }
        #endregion
        /// <summary>
        ///   开启协程，用于获取版本配置信息
        /// </summary>
        public void GetVersion()
        {    
            StartCoroutine(GetVersion(CompareVersion));
            
        }
        /// <summary>
        /// 开启协程，用户下载最新的版本包
        /// </summary>
        public void GetNewPackage()
        {
            StartCoroutine(GetPackage(InstallPackage));
        }
        /// <summary>
        /// 安装下载好的版本包，安装完成后，删除该版本包
        /// </summary>
        /// <param name="packageUrl"></param>
        private void InstallPackage(string packageUrl)
        {
            //开始安装package
            AssetDatabase.ImportPackage(packageUrl, false);
            //删除package
            File.Delete(packageUrl);
            AssetDatabase.importPackageCompleted += AssetDatabase_importPackageCompleted;
        }
        /// <summary>
        /// 安装package完成后，显示更新日志
        /// </summary>
        /// <param name="packageName"></param>
        private void AssetDatabase_importPackageCompleted(string packageName)
        {
            EditorUtility.DisplayDialog(string.Format("R.D.FW{0}更新详情", RDUpdateConfig.CurVersionNum), RDUpdateConfig.log, "这就去尝试！");
        }

        /// <summary>
        /// 获取最新版本信息
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        IEnumerator GetVersion(Action<string> action)
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(RDUpdateConfig.url);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.error != null)
            {
                Debug.Log(unityWebRequest.error);
            }
            else
            {
                if (unityWebRequest.responseCode == 200)
                {
                    string version = unityWebRequest.downloadHandler.text;
                    //Debug.Log(version);
                    //执行委托
                    action?.Invoke(version);
                }
            }
            unityWebRequest.Abort();
            unityWebRequest.Dispose();
            unityWebRequest = null;
            DestroyImmediate(gameObject);
            requestIns = null;
        }
        /// <summary>
        /// 下载最新的版本包
        /// </summary>
        /// <param name="action">下载完成后，执行的委托</param>
        /// <returns></returns>
        IEnumerator GetPackage(Action<string> action)
        {

            UnityWebRequest unityWebRequest = UnityWebRequest.Get(RDUpdateConfig.packageUrl);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.error != null)
            {
                Debug.Log(unityWebRequest.error);
            }
            else
            {
                if (unityWebRequest.responseCode == 200)
                {
                    byte[] package = unityWebRequest.downloadHandler.data;
                    if (package.Length > 0)
                    {
                        //将版本包的byte[]数据，写在本地，生成unitypackage
                        File.WriteAllBytes(Application.dataPath + "/RD.unitypackage", package);
                    }
                    //yield return new WaitForSeconds(1.0f);
                    //执行下载并生成好版本包之后的委托，进行安装
                    action?.Invoke(Application.dataPath + "/RD.unitypackage");
                }
            }
            unityWebRequest.Dispose();
            DestroyImmediate(gameObject);
            requestIns = null;
        }
        /// <summary>
        /// 对比版本信息
        /// </summary>
        /// <param name="dversion">版本文本</param>
        private void CompareVersion(string dversion)
        {
            if (File.Exists(Application.dataPath + "/RushDevelopFW/PackageConfig.json"))
            {   
                //旧的版本信息
                ConfigJson oldVersion =JsonUtility.FromJson<ConfigJson>(File.ReadAllText(Application.dataPath + "/RushDevelopFW/PackageConfig.json"));
                //新的版本信息
                ConfigJson newVersion = JsonUtility.FromJson<ConfigJson>(dversion);
                //比较版本信息
                if (oldVersion.versionNum != newVersion.versionNum)
                {
                    RDUpdateManager epu =EditorWindow.GetWindow<RDUpdateManager>();
                    epu.minSize = RDUpdateManager.minResolution;
                    epu.maxSize = RDUpdateManager.minResolution;
                    epu.Init();
                    epu.Show();
                }
                else
                {
                    Debug.Log( "R.D.版本管理器：您当前使用的是最新版本！如有问题请联系244215913@qq.com"); 
                }
            }
        }
    }

    #region 编辑器下 提示更新的窗体
    /// <summary>
    /// 编辑器下的窗体显示
    /// </summary>
    public class RDUpdateManager : EditorWindow
    {
        private GUILayoutOption GUILayoutOption;
        public static Vector2 minResolution = new Vector2(400, 100);
        public static Rect middleCenterRect = new Rect(50, 10, 400, 100);
        static GUIStyle labelStyle = new GUIStyle();
        public void Init()
        {
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperLeft;
            labelStyle.fontSize = 14;
            labelStyle.border = new RectOffset(10, 10, 20, 20);
        }
        private void OnGUI()
        {
            GUILayout.BeginArea(middleCenterRect);
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("检测到R.D.发布了新版本，是否更新？", labelStyle, GUILayout.Width(200));
            GUILayout.Space(20);       
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("是", GUILayout.Width(80)))
            {
                Debug.Log("更新");
                Close();
                //开始下载package
                WebRequestInEdior.GetIns().GetNewPackage();
            }
            GUILayout.Space(20);
            if (GUILayout.Button( "否", GUILayout.Width(80)))
            {
                Debug.Log("取消更新");
                Close();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
    #endregion
#endif
}

