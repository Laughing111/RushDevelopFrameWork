using UnityEngine;

namespace RDFW
{
    public class RDTimer:MonoBehaviour
    {
        #region 时间生成器
        /// <summary>
        /// 获得当前时间：年月日-小时
        /// </summary>
        /// <returns></returns>
        public static string GetTime()
        {
            return System.DateTime.Now.ToString("yyyMMdd_HHmm");
        }
        #endregion

        #region 定时器
        #endregion
    }
}

