using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDScreenManager
{
    #region 分辨率识别器
    /// <summary>
    /// 检测分辨率，识别横竖屏，并打印至控制台
    /// </summary>
    public static void DebugScreenType()
    {
        bool landScape = Screen.width > Screen.height;
        Debug.LogFormat("{0}:{1}x{2}", landScape ? "横屏" : "竖屏", Screen.width, Screen.height);
    }
    #endregion
}
