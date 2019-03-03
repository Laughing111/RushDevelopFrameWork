using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型池接口，设置需要实现的对象池的方法
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPool<T> 
{
    /// <summary>
    /// 申请动态内存的方法，动态创建对象
    /// </summary>
    /// <returns></returns>
    T Allocate();

    /// <summary>
    /// 回收对象内存的方法
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    bool Recycle(T obj);
}

/// <summary>
/// 设置了创建对象方法的接口（工厂接口）
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IObjectFactory<T>
{
    /// <summary>
    /// 创建对象的方法
    /// </summary>
    /// <returns></returns>
    T Create();
}

/// <summary>
/// 内存池的抽象类，实现了泛型池接口的方法
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Pool<T> : IPool<T>
{
    /// <summary>
    /// 用于存储对象的栈集合
    /// </summary>
    protected Stack<T> mCacheStack = new Stack<T>();

    /// <summary>
    /// 提供查看当前栈长度的属性
    /// </summary>
    public int CurCount
    {
        get { return mCacheStack.Count;}
    }
    /// <summary>
    /// 工厂方法
    /// </summary>
    protected IObjectFactory<T> mFactory;
    
    /// <summary>
    /// 虚方法：返回需要创建的对象，子类去继承扩展
    /// </summary>
    /// <returns></returns>
    public virtual T Allocate()
    {
        return mCacheStack.Count > 0 ? mCacheStack.Pop() : mFactory.Create();
    }

    /// <summary>
    /// 抽象回收方法，让子类实现
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public abstract bool Recycle(T obj);
}
