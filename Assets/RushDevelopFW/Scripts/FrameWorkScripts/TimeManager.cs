using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDFW;

namespace RDFW
{
    public class TimeManager : MonoBehaviour
    {
        private Dictionary<string, UIBase> runPanel;
        private int time;
        public int targetTime = 10;
        private static TimeManager timeManager;
        public static TimeManager GetTimeManagerIns()
        {
            if (timeManager == null)
            {
                GameObject timeManagerObj = GameObject.Find("TimeManager");
                if (timeManagerObj == null)
                {
                    timeManagerObj = new GameObject("TimeManager");
                    timeManagerObj.AddComponent<TimeManager>();
                }
                else
                {
                    if (timeManagerObj.GetComponent<TimeManager>() == null)
                    {
                        timeManagerObj.AddComponent<TimeManager>();
                    }
                }
                timeManager = timeManagerObj.GetComponent<TimeManager>();
            }
            return timeManager;
        }

        private void Awake()
        {
            if (runPanel == null)
            {
                runPanel = new Dictionary<string, UIBase>();
            }

        }
        private void Start()
        {
            StartCoroutine(TimeCount());
        }
        public void ResetTimeAndStartCount()
        {
            time = 0;
        }
        /// <summary>
        /// 计时方法
        /// </summary>
        /// <returns></returns>
        IEnumerator TimeCount()
        {
            //每次计时，都清理一次无效面板
            ClearDic();
            int count = runPanel.Count;
            if (count < 1)
            {
                yield return new WaitForSeconds(1.0f);
                yield return TimeCount();
            }
            if (time < targetTime)
            {
                time++;
                Debug.Log("开始计时：" + time);
            }
            else if (time >= targetTime)
            {
                //达到指定时间点
                //相应操作
                //yield return new WaitForSeconds(0.5f);
                foreach (KeyValuePair<string, UIBase> kv in runPanel)
                {
                    //if (kv.Key != "需要忽略的面板")
                    //{
                    //   kv.Value.OnInActive();
                    //}
                    //MessageCenter.OpenPanel("指定面板");
                    kv.Value.OnInActive();
                }
                time = 0;
            }
            yield return new WaitForSeconds(1.0f);
            yield return TimeCount();
        }

        [Header("PanelInfo：Running Panel")]
        public List<string> list;
        //清除无效面板
        public void ClearDic()
        {
            list = new List<string>();
            foreach (string key in runPanel.Keys)
            {
                list.Add(key);
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (runPanel[list[i]].gameObject.activeSelf == false)
                {
                    runPanel.Remove(list[i]);
                }
            }
        }

        /// <summary>
        /// 存储运行的面板
        /// </summary>
        /// <param name="PanelName"></param>
        /// <param name="uIBase"></param>
        public void StorePanel(string PanelName, UIBase uIBase)
        {
            if (runPanel == null)
            {
                runPanel = new Dictionary<string, UIBase>();
            }
            if (!runPanel.ContainsKey(PanelName))
            {
                runPanel.Add(PanelName, uIBase);
            }
        }




    }
}


