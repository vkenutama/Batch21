LinkedList<int> llist = new LinkedList<int>();

void Append(int val)
{
    llist.AddLast(val);
    Console.WriteLine($"Appended {val}");
}

void Print()
{
    var ptr = llist.First;

    string msg = "";
    while(ptr != null)
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

Append(5);
Append(10);
Append(15);
Append(20);
Print();