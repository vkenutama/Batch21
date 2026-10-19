LinkedList<int> llist = new LinkedList<int>();

void Append(int val)
{
    var ptr = llist.First;
    while (ptr != null && ptr.Value <= val)
    {
        if (ptr.Next == null)
        {
            llist.AddLast(val);
            ptr = null;
            break;
        }
        ptr = ptr.Next;
    }

    if (llist.First == null)
    {
        llist.AddFirst(val);
    }

    if (ptr != null)
    {
        llist.AddBefore(ptr, val);
    }
    
    Console.WriteLine($"Appended {val}");
}

void Print()
{
    var ptr = llist.First;

    string msg = "";
    while (ptr != null)
    {
        msg += $"{ptr.Value}";
        ptr = ptr.Next;

        if (ptr != null)
        {
            msg += " -> ";
        }
    }

    Console.WriteLine("Sequence: " + msg);
}

void PrintReverse()
{
    var ptr = llist.Last;

    string msg = "";
    while (ptr != null)
    {
        var val = ptr.Value;
        msg += val.ToString();

        ptr = ptr.Previous;

        if (ptr != null)
        {
            msg += " -> ";
        }
    }

    Console.WriteLine($"Reversed: {msg}");
}

Append(5);
Append(2);
Append(8);
Print();
PrintReverse();