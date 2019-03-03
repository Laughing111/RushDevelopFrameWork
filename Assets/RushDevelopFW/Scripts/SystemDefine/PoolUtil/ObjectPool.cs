using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象缓存池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> : Pool<T>
{
    /// <summary>
    /// 回收对象的方法
    /// </summary>
    readonly Action<T> mResetMethod;
    /// <summary>
    /// 类的构造函数，初始化对象池
    /// </summary>
    /// <param name="factoryMethod">自定义工厂的生产方法</param>
    /// <param name="resetMethod">自定义回收对象的方法</param>
    /// <param name="initCount">初始最大缓存对象数量</param>
    public ObjectPool(Func<T> factoryMethod,Action<T> resetMethod=null,int initCount=0)
    {
        mFactory = new ObjectFactory<T>(factoryMethod);
        mResetMethod = resetMethod;
        for(int i=0;i<initCount;i++)
        {
            mCacheStack.Push(mFactory.Create());
        }

    }
    /// <summary>
    /// 创建对象的方法
    /// </summary>
    /// <returns></returns>
    public override T Allocate()
    {
        return base.Allocate();
    }
    /// <summary>
    /// 复写的回收对象方法，用于对象使用完毕后，回收对象入对象池
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Recycle(T obj)
    {
        if (mResetMethod != null)
        {
            mResetMethod(obj);
        }
        mCacheStack.Push(obj);
        return true;
    }
}
