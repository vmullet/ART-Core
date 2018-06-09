using System;
using System.Collections.Generic;

public static class SingletonFactory {

    private static readonly IDictionary<Type, object> singletons;

    static SingletonFactory()
    {
        singletons = new Dictionary<Type, object>();
    }

    public static T Instance<T>(params object[] args)
    {
        Type instanceType = typeof(T);
        if (!singletons.ContainsKey(instanceType))
        {
            T instance = (T)Activator.CreateInstance(instanceType,true);
            singletons.Add(instanceType, instance);
        }
            return (T)singletons[instanceType];
    }

}
