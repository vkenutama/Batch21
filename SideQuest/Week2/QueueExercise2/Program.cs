Queue<string> queue = new Queue<string>();

void Enqueue(string val)
{
    queue.Enqueue(val);
    Console.WriteLine($"Queued {val}");
}

void EnqueueVip(string val)
{
    int queueCount = queue.Count;
    List<string> temp = new List<string>();

    // Dequeue all
    for (int i = 0; i < queueCount; i++)
    {
        bool success = queue.TryDequeue(out string? queueValue);
        if (success)
        {
            temp.Add(queueValue!);
        }
    }

    queue.Enqueue(val);
    Console.WriteLine($"VIP Queued {val}");

    foreach (var t in temp)
    {
        queue.Enqueue(t);
    }
}

void Process()
{
    if (queue.Count == 0)
    {
        Console.WriteLine("Queue is empty");
        return;
    }

    var process = queue.Dequeue();
    Console.WriteLine($"Processed {process}");
}

Enqueue("A");
Enqueue("B");
EnqueueVip("C");
Process();
Process();
Process();
Process();