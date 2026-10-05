Queue<string> queue = new Queue<string>();

void Enqueue(string val)
{
    queue.Enqueue(val);
    Console.WriteLine($"Queued {val}");
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
Process();
Process();
Process();

