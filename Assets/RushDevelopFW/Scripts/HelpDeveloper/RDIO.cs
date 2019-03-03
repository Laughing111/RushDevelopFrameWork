using UnityEngine;
using System.IO;

namespace RDFW
{
    public class RDIO
    {
        #region 访问指定目录
        /// <summary>
        /// 访问指定目录
        /// </summary>
        /// <param name="path">路径</param>
        public static void OpenFolder(string path)
        {
            Application.OpenURL("file:///" + path);
        }
        #endregion
    }
}

