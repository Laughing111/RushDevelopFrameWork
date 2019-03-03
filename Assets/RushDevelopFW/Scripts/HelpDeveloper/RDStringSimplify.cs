using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDFW;

namespace RDFW
{
    public static class RDStringSimplify
    {
        /// <summary>
        /// 检测字符串中是否包含某字符串，如果包含，则修改字符串为指定内容
        /// </summary>
        /// <param name="str">需要修改的字符串</param>
        /// <param name="targetStr">需要替换的部分</param>
        /// <param name="newStr">新内容</param>
        public static string StringModify(this string str,string targetStr,string newStr)
        {
            str = str.Contains(targetStr) ? str.Replace(targetStr, newStr) : str;
            return str;
        }
    }
}

