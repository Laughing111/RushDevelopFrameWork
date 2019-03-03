using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RDFW;

namespace RDFW
{
    public class EditorExporter
    {
        #region 导出package,用时间命名 
        public static void ExportPackageWithTime()
        {
            //拼接时间字符串合成文件名
            string fileName = "Assets/RushDevelopFW_" + RDTimer.GetTime() + ".unitypackage";
            EditorUtility.DisplayProgressBar("R.D.PackageManager", "正在导出UnityPackage,请不要关闭编辑器...", 99);
            //自动导出
            AssetDatabase.ExportPackage("Assets", fileName, ExportPackageOptions.Recurse);
            //导出后，打开package所在文件夹
            
            RDIO.OpenFolder(Application.dataPath);
            Debug.LogFormat("已成功导出Packagez至{0}！", "Assets/");
            EditorUtility.ClearProgressBar();
        }
        #endregion
    }
}

