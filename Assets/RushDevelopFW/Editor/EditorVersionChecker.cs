using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using RDFW;
using UnityEngine.Networking;
using System;

namespace RDFW
{
    [InitializeOnLoad]
    public class EditorVersionChecker
    {
        /// <summary>
        /// 构造函数，当导入安装包时，进行更新检查
        /// </summary>
        static EditorVersionChecker()
        {
            //启动检查更新
            //检查序列化的更新标记
            if (!PlayerPrefs.HasKey("haveUpdated")|| PlayerPrefs.GetInt("haveUpdated") == 0)
            {
              WebRequestInEdior.GetIns().GetVersion();
              PlayerPrefs.SetInt("haveUpdated", 1);
              PlayerPrefs.Save();
            }
            //Debug.Log(PlayerPrefs.GetInt("haveUpdated").ToString());
        }
     

        private static string scanPath = Application.dataPath + @"\RushDevelopFW";
        //检索文件目录
        public static void ScanFolderAndGenerateConfig()
        {
            ConfigJson config = new ConfigJson();
            WriteFilePath(scanPath, config);
            string configTXT = JsonUtility.ToJson(config);
            File.WriteAllText(Application.dataPath + "/RushDevelopFW/PackageConfig.json", configTXT);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 遍历每一个文件目录
        /// </summary>
        /// <param name="path">遍历的地址</param>
        /// <param name="config">在config文本中，添加需要的信息</param>
        private static void WriteFilePath(string path, ConfigJson config)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            Debug.Log(fileInfos.Length);
            for (int i = 0; i < fileInfos.Length; i++)
            {
                if (fileInfos[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                config.log = RDUpdateConfig.log;
                config.versionNum = RDUpdateConfig.CurVersionNum;
                config.direct.Add(fileInfos[i].FullName);

            }
        }
        [MenuItem("R.D.Tools/VersionManager/CheckUpdate")]
        public static void CheckConfigAndClearDir()
        {
            #region 检查多余
            //if (!File.Exists(Application.dataPath + "/RushDevelopFW/PackageConfig.txt"))
            //{
            //    return;
            //}
            //ConfigJson currentConfig = JsonUtility.FromJson<ConfigJson>(File.ReadAllText(Application.dataPath + "/RushDevelopFW/PackageConfig.txt"));
            //ConfigJson downloadConfig = JsonUtility.FromJson<ConfigJson>(File.ReadAllText(@"C:\Users\Administrator\Desktop" + "/PackageConfig.txt"));
            //int currentConfigLength = currentConfig.direct.Count;
            //int downConfigLength = downloadConfig.direct.Count;
            //if (currentConfigLength > downConfigLength)
            //{
            //    for (int i = 0; i < currentConfigLength; i++)
            //    {
            //        if (!downloadConfig.direct.Contains(currentConfig.direct[i]))
            //        {
            //            Debug.LogError("在框架库中发现多余脚本：" + currentConfig.direct[i] + ",可前往删除！");
            //        }
            //    }
            //}
            #endregion
            //下载版本号 对比是否需要更新
            //用playerPrefs永久序列化更新标志位到本地
            WebRequestInEdior.GetIns().GetVersion();
        }
  }
}

