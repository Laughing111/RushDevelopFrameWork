using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDFW
{
    public class RDMathf
    {
        #region 概率计算器
        /// <summary>
        /// 以0%-100%为概率范围，判断目标概率的事件是否发生
        /// </summary>
        /// <param name="targetPercent">目标概率</param>
        /// <returns></returns>
        public static bool PercentCal(int targetPercent)
        {
            return Random.Range(1, 101) <= targetPercent;
        }
        #endregion
    }
}

