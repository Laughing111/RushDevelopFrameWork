using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Receive : MonoBehaviour {
    [Header("OSCManager")]
    public OSC oscReference;
    public string address;
    //接收到的消息,供外部子类调用
    public string RecieveMessage;
    public virtual void Start()
    {
        oscReference.SetAddressHandler("/" + address, OnReceive);
    }
    //用来处理接收到指定消息后的处理
    void OnReceive(OscMessage message)
    {
        RecieveMessage = message.values[0].ToString();
        Debug.Log (RecieveMessage);
        //u can add ur own method to respond
        Respond();
    }
    //子类继承的响应方法
    public virtual void Respond()
    {
       
    }






   




}