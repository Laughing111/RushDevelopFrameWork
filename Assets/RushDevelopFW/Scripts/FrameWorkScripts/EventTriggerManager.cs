using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RDFW;

namespace RDFW
{
    public class EventTriggerManager : UnityEngine.EventSystems.EventTrigger
    {


        /// <summary>
        /// 事件管理器的点击事件委托
        /// </summary>
        /// <param name="eventData">固定参数</param>
        public delegate void ClickListened(PointerEventData eventData);

        public ClickListened clickListened;
        /// <summary>
        /// 获取监听组件
        /// </summary>
        /// <param name="Listened">被监听的对象</param>
        /// <returns></returns>
        public static EventTriggerManager GetEventTriggerManager(GameObject Listened)
        {
            EventTriggerManager eventTriggerManager = Listened.GetComponent<EventTriggerManager>();
            if (eventTriggerManager == null)
            {
                eventTriggerManager = Listened.AddComponent<EventTriggerManager>();
            }
            return eventTriggerManager;

        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            //如果对象被点击，则出发这个方法
            clickListened(eventData);
            if (GameObject.Find("TimeManager") != null)
            {
                TimeManager tm = TimeManager.GetTimeManagerIns();
                tm.ResetTimeAndStartCount();
            }
        }

        public void AddClickEvent(ClickListened ClickEvent)
        {

            clickListened += ClickEvent;
        }

        public void RemoveAllRegister()
        {
            clickListened = null;
        }
    }
}


