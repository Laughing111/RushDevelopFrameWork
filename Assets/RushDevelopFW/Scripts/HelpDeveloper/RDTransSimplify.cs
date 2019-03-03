using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDFW
{
    public static class RDTransSimplify
    {
        #region 扩展localPosition的设置方法
        /// <summary>
        /// 设置Transform的本地位置信息的单通道值
        /// </summary>
        /// <param name="transform">目标transform</param>
        /// <param name="pos">通道类型：x,y,z</param>
        /// <param name="custom">目标位置值</param>
        public static void SetLocalPos(this Transform transform, Pos pos, float custom)
        {
            Vector3 localPos = transform.localPosition;
            switch (pos)
            {
                case Pos.x:
                    localPos.x = custom;
                    break;
                case Pos.y:
                    localPos.y = custom;
                    break;
                case Pos.z:
                    localPos.z = custom;
                    break;
                default:
                    break;
            }
            transform.localPosition = localPos;
        }

        /// <summary>
        /// 设置Transform的本地位置信息的双通道值
        /// </summary>
        /// <param name="transform">目标transform</param>
        /// <param name="pos">通道类型：x,y,z</param>
        /// <param name="custom1">目标位置值1</param>
        /// <param name="custom2">目标位置值2</param>
        public static void SetLocalPos(this Transform transform, Pos pos, float custom1, float custom2)
        {
            Vector3 localPos = transform.localPosition;
            switch (pos)
            {
                case Pos.xy:
                    localPos.x = custom1;
                    localPos.y = custom2;
                    break;
                case Pos.xz:
                    localPos.x = custom1;
                    localPos.x = custom2;
                    break;
                case Pos.yz:
                    localPos.y = custom1;
                    localPos.z = custom2;
                    break;
            }
            transform.localPosition = localPos;
        }
        #endregion

        #region Transform查找目标子物体
        /// <summary>
        /// 查找获得子UI物件的引用
        /// </summary>
        /// <param name="transform">查找的起点UI</param>
        /// <param name="target">目标UI的名字</param>
        /// <returns></returns>
        public static Transform SearchforChild(this Transform transform, string target)
        {
            if (transform.name == target)
            {
                return transform;
            }
            int count = transform.childCount;
            // Debug.Log(transform.name + count);
            if (count > 0)
            {
                Transform tar = transform.Find(target);
                if (tar == null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        tar = SearchforChild(transform.GetChild(i), target);
                        if (tar != null)
                        {
                            return tar;
                        }
                    }
                }
                // Debug.Log("Find the " + target);
                return tar;
            }
            //Debug.LogError("Can't find the " + target + ",please check it!");
            return null;

        }
        #endregion
    }
}

