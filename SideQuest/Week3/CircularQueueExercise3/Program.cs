const int maximumCapacity = 3;
Queue<int> queue = new Queue<int>();

void Log(int val)
{
    int countAfter = queue.Count + 1;
    
    float queueCapacity = (float)queue.Count/ maximumCapacity;
    
    if (queueCapacity >= 0.66f && queueCapacity < 1.0f)
    {
        Console.WriteLine($"Warning: Buffer at {0.66f:P0}");
    }
    
    if (countAfter > maximumCapacity)
    {
        Console.WriteLine("Critical: Buffer Full");

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