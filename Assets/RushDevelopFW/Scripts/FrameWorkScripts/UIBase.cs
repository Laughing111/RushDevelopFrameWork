using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;
using RDFW;

namespace RDFW{
    public class UIBase : MonoBehaviour
    {
        /// <summary>
        /// UI面板的名称
        /// </summary>
        private string UIName;

        /// <summary>
        /// 面板类型，是否需要倒计时管理
        /// </summary>
        [Header("IsTimeCountDown")]
        public TimeManagement timeManagement;

        /// <summary>
        /// 面板初始活跃类型
        /// </summary>
        [Header("IsActiveOnAwake")]
        public ActiveType ActiveTypeOnAwake;

        /// <summary>
        /// 本地信箱
        /// </summary>
        private Dictionary<LocalMessageAddress, MessageCenter.Message> LocalMessageBox;

        /// <summary>
        /// 倒计时管理器
        /// </summary>
        private TimeManager timeManager;

        #region 生命周期
        /// <summary>
        /// ui初始化
        /// </summary>
        public virtual void PanelInit()
        {
            if (timeManagement == TimeManagement.TimeCountDown)
            {
                timeManager = TimeManager.GetTimeManagerIns();
            }
            AddMessage2LocalBox(gameObject.name, "PanelActive", PanelActive);
            AddMessage2LocalBox(gameObject.name, "PanelInActive", PanelInActive);
            if (ActiveTypeOnAwake == ActiveType.ActiveOnAwake)
            {
                OnActive();
            }
            else
            {
                OnInActive();
            }
        }

        private void PanelInActive(MessageAddress messageAddress)
        {
            OnInActive();

        }
        private void PanelActive(MessageAddress messageAddress)
        {
            OnActive();

        }

        /// <summary>
        /// ui激活方法
        /// </summary>
        public virtual void OnActive()
        {
            gameObject.SetActive(true);
            if (timeManagement == TimeManagement.TimeCountDown)
            {
                timeManager.StorePanel(gameObject.name, this);
                timeManager.ResetTimeAndStartCount();
            }
        }



        /// <summary>
        /// ui失活方法
        /// </summary>
        public virtual void OnInActive()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region 给子类使用



        /// <summary>  
        ///  注册UI点击交互事件
        /// </summary>
        /// <param name="InterObjectName">被监听物体的名字</param>
        /// <param name="InterEvent">监听事件函数 方法参数需携带PointerEventData </param>
        public void RegisterInterObject(string InterObjectAssets, EventTriggerManager.ClickListened InterEvent)
        {

            //查找对象组件
            GameObject InterObject = transform.SearchforChild(InterObjectAssets).gameObject;
            //为对象添加监听组件
            EventTriggerManager eventTriggerManager = EventTriggerManager.GetEventTriggerManager(InterObject);
            //注册监听事件
            eventTriggerManager.AddClickEvent(InterEvent);
        }

        public void RegisterInterObject(Transform InterObjectAssets, EventTriggerManager.ClickListened InterEvent)
        {

            //查找对象组件
            GameObject InterObject = InterObjectAssets.gameObject;
            //为对象添加监听组件
            EventTriggerManager eventTriggerManager = EventTriggerManager.GetEventTriggerManager(InterObject);
            //注册监听事件
            eventTriggerManager.AddClickEvent(InterEvent);
        }

       //处理UGUI的Button
       



        /// <summary>
        /// 自定义消息添加至本地信箱
        /// </summary>
        /// <param name="messageAddress">用于筛选的消息地址</param>
        /// <param name="message">具体执行的方法</param>
        public void AddMessage2LocalBox(string Childaddress, string funcName, MessageCenter.Message message)
        {
            UIName = gameObject.name;

            if (LocalMessageBox == null)
            {
                LocalMessageBox = new Dictionary<LocalMessageAddress, MessageCenter.Message>();
            }
            //将本地信箱注册入消息中心
            AddLocalMessageBox2MessageCenter();
            LocalMessageAddress localMessageAddress = new LocalMessageAddress(Childaddress, funcName);
            if (!LocalMessageBox.ContainsKey(localMessageAddress))
            {
                LocalMessageBox.Add(localMessageAddress, null);
                //Debug.Log(UIName+"已添加本地消息体");
            }
            LocalMessageBox[localMessageAddress] = message;
        }
        #endregion


        #region 本地信箱的处理方法
        /// <summary>
        /// 把本地信箱的阅读方法，加载到消息处理中心
        /// </summary>
        private void AddLocalMessageBox2MessageCenter()
        {
            MessageCenter.AddMessage2Center(UIName, ReadLocalMessage);
        }
        /// <summary>
        /// 本地信箱的消息筛选阅读方法
        /// </summary>
        /// <param name="messageAddress">消息标志,消息处理中心的键值对</param>
        private void ReadLocalMessage(MessageAddress messageAddress)
        {

            Debug.Log("正在本地信箱筛选消息");
            //MessageCenter.Message message;
            foreach (KeyValuePair<LocalMessageAddress, MessageCenter.Message> kv in LocalMessageBox)
            {
                if (kv.Key.ChildAddress == messageAddress.Address && kv.Key.FuncName == messageAddress.FuncName)
                {
                    Debug.Log("找到本地消息体");
                    kv.Value(messageAddress);
                    return;
                }
            }

        }
        #endregion
    }
    /// <summary>
    /// 本地信箱标签
    /// </summary>
    public class LocalMessageAddress
    {
        public string ChildAddress { get; }
        public string FuncName { get; }
        public LocalMessageAddress(string _childAddress, string _funcName)
        {
            ChildAddress = _childAddress;
            FuncName = _funcName;
        }
    }
}

