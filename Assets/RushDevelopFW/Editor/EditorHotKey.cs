using UnityEditor;
using UnityEngine;
using RDFW;

namespace RDFW
{
    public class EditorHotKey
    {
        #region 版本管理
        #region 自动导出包 ctrl+e
        [MenuItem("R.D.Tools/VersionManager/GeneratePackage %e")]
        private static void ExportPackageWithTime()
        {
            EditorVersionChecker.ScanFolderAndGenerateConfig();
            EditorExporter.ExportPackageWithTime();
        }
        #endregion
        #region 检查更新
        [MenuItem("R.D.Tools/VersionManager/CheckUpdate")]
        private static void CheckUpdate()
        {
            EditorVersionChecker.CheckConfigAndClearDir();
        }
        #endregion
        #endregion

        #region 检测分辨率并打印 shift+r
        [MenuItem("R.D.Tools/DebugScreenType #r")]
        private static void DistinguishResolution()
        {
            RDScreenManager.DebugScreenType();
        }
        #endregion

        #region 添加分辨率组件
        [MenuItem("R.D.Tools/RushScripts/AddComponent/Screen/SetResolution")]
        private static void AddResComponent()
        {
            Selection.activeGameObject.AddComponent<SetResolution>();
        }
        #endregion
    }
}

