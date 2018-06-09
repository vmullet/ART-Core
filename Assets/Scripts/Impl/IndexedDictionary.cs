using System.Collections.Specialized;

public class IndexedDictionary<T,U> : OrderedDictionary {

    public new U this[int index] => (U)base[index];

    public U this[T key] => (U)base[key];

    public new void Add(object key,object value)
    {
        if (typeof(object) != typeof(T) || typeof(object) != typeof(U))
        {
            throw new System.Exception("You must add objects with key of type T and value of type U");
        }
    }

    public void Add(T key,U value)
    {
        base.Add(key, value);
    }

}
