using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDFW;

namespace RDFW
{
    public static class RDGameObjectSimplify
    {
        /// <summary>
        /// 修改GameObject的名字
        /// </summary>
        /// <param name="obj">目标GameObject</param>
        /// <param name="newName">新名字</param>
        public static void ModifyName(this GameObject obj, string newName)
        {
            obj.name = newName;
        }
    }
}

