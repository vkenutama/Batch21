const int maximumCapacity = 3;
Queue<int> queue = new Queue<int>();

void Log(int val)
{
    if (queue.Count + 1 > maximumCapacity)
    {
        bool success = queue.TryDequeue(out _);
        
        if (success)
        {
            queue.Enqueue(val);
            Console.WriteLine($"Overwritten oldest with {val}");
        }

        return;
    }


    queue.Enqueue(val);
    Console.WriteLine($"Logged {val}");
}

void Read()
{
    if (queue.TryDequeue(out int val))
    {
        Console.WriteLine($"Read {val}");
    }
}

Log(1);
Log(2);
Log(3);
Log(4);
Read();