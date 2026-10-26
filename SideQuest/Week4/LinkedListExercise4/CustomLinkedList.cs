namespace LinkedListExercise4;

public class CustomLinkedList
{
    private readonly LinkedList<int> _llist = new LinkedList<int>();
    private Func<int, int, int>? _comparer = null;
    private List<Func<int, bool>> _filterRules = new();

    public void SetSorting(Func<int, int, int> comparer)
    {
        _comparer = comparer;
    }

    public void AddFilter(Func<int, bool> filter)
    {
        _filterRules.Add(filter);
    }

    private List<int> Sort(List<int> list)
    {
        if (_comparer != null)
        {
            list.Sort((a, b) => _comparer(a, b));
        }
        else
        {
            list.Sort();
        }

        return list;
    }

    public void Append(int val)
    {
        _llist.AddLast(val);
        var sorted = Sort(_llist.ToList());

        var ptr = _llist.First;
        foreach (var data in sorted)
        {
            if (ptr != null)
            {
                ptr.Value = data;
                ptr = ptr.Next;
            }
        }

        Console.WriteLine($"Appended {val}");
    }

    public void Print()
    {
        var ptr = _llist.First;

        var tempData = new List<int>();
        while (ptr != null)
        {
            if (IsInFilter(ptr.Value))
            {
                tempData.Add(ptr.Value);
            }

            ptr = ptr.Next;
        }

        string msg = String.Empty;

        for (var index = 0; index < tempData.Count; index++)
        {
            var d = tempData[index];
            msg += d.ToString();

            if (index < tempData.Count - 1)
            {
                msg += " -> ";
            }
            
        }
        
        Console.WriteLine("Sequence: " + msg);
    }

    private bool IsInFilter(int val) => _filterRules.Any(filter => filter(val));

    public void PrintReverse()
    {
        var ptr = _llist.Last;
        
        var tempData = new List<int>();
        while (ptr != null)
        {
            var currentValue = ptr.Value;

            if (IsInFilter(currentValue))
            {
                tempData.Add(ptr.Value);
            }

            ptr = ptr.Previous;
        }

        string msg = String.Empty;
        
        for (int i = 0; i < tempData.Count; i++)
        {
            var d = tempData[i];
            msg += d.ToString();

            if (i < tempData.Count - 1)
            {
                msg += " -> ";
            }
        }
        
        Console.WriteLine($"Reversed: {msg}");
    }
}