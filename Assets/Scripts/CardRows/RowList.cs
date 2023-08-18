using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RowList<T> : IEnumerable<T>
{
    public delegate bool SequenceDelegate(T current, T next);

    private List<T> values = new List<T>();

    public T Last { get { return values.LastOrDefault(); } }

    public IEnumerator<T> GetEnumerator()
    {
        return values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void AddLast(T value)
    {
        values.Add(value);
    }

    public IEnumerable<T> GetAfter(T card)
    {
        List<T> result = new List<T>();

        for (int i = values.IndexOf(card); i < values.Count; i++)
        {
            result.Add(values[i]);
        }

        return result;
    }

    public IEnumerable<T> RemoveAfter(T card)
    {
        IEnumerable<T> result = GetAfter(card);

        values = values.Except(result).ToList();

        return result;
    }

    public T GetPrevious(T obj)
    {
        T result = default;

        foreach (T value in values)
        {
            if (value.Equals(obj))
            {
                break;
            }

            result = value;
        }

        return result;
    }

    public IEnumerable<T> LastSequence(SequenceDelegate sequence)
    {
        List<T> result = new List<T>();

        T current = default;

        foreach (T item in values)
        {
            if (current != null && !sequence(current, item))
            {
                result.Clear();
            }

            current = item;
            result.Add(current);
        }

        return result;
    }

    public void ForEach(System.Action<T> action)
    {
        foreach (T obj in values)
        {
            action(obj);
        }
    }
}