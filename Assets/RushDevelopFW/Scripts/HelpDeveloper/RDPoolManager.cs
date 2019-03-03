using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDPoolManager<T>
{
    /// <summary>
    /// 用来存储T类型对象的队列集合
    /// </summary>
    protected Queue<T> cacheQueue = new Queue<T>();

    /// <summary>
    /// 外部自定义的创建对象方法
    /// </summary>
    private Func<T> pCreateMethod;

    /// <summary>
    /// 外部可自定义的回收对象的方法
    /// </summary>
    private Action<T> pRecycleMethod;

    /// <summary>
    /// 队列数量,属性设置
    /// </summary>
    private int queueCount;
    public int QueueCount
    {
        get { queueCount = cacheQueue.Count;
                return queueCount;}
    }

    /// <summary>
    /// 外部构造方法
    /// </summary>
    /// <param name="createMethod">创建对象的方法</param>
    /// <param name="recycleMethod">回收对象的方法</param>
    /// <param name="initMaxCount">对象池初始化数量</param>
    public RDPoolManager(Func<T> createMethod, Action<T> recycleMethod, int initMaxCount)
    {
        pCreateMethod = createMethod;
        pRecycleMethod = recycleMethod;
        
        //初始化对象队列的长度
        for(int i=0;i< initMaxCount; i++)
        {
            cacheQueue.Enqueue(createMethod());
        }
    }
    /// <summary>
    /// 共外部继承的对象创建方法
    /// </summary>
    /// <returns></returns>
    public T CreatObj(Action<T> initObjMethod)
    {
        T outObj = cacheQueue.Count > 0 ? cacheQueue.Dequeue() : pCreateMethod();
        initObjMethod(outObj);
        return outObj;
    }

    /// <summary>
    /// 供外部继承的对象回收方法
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int RecycleObj(T obj)
    {
        cacheQueue.Enqueue(obj);
        pRecycleMethod(obj);
        return queueCount; 
    }

}
