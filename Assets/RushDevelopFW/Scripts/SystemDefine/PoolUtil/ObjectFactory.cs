using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory<T> : IObjectFactory<T>
{
    /// <summary>
    /// 本地制造方法
    /// </summary>
    protected Func<T> mFactoryMethod;
    /// <summary>
    /// 构造函数，传入制造方法
    /// </summary>
    /// <param name="factoryMethod"></param>
    public ObjectFactory(Func<T> factoryMethod)
    {
        mFactoryMethod = factoryMethod;
    }
    /// <summary>
    /// 执行制造方法
    /// </summary>
    /// <returns></returns>
    public T Create()
    {
        return mFactoryMethod();
    }
}
