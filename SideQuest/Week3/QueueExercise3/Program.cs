using QueueExercise3;

List<QueueItem> queueItems = new List<QueueItem>();

void Enqueue(string val, int priority)
{
    QueueItem queueItem = new()
    {
        Value = val,
        Priority = priority,
        CreatedAt = DateTime.UtcNow
    };

    queueItems.Add(queueItem);
    Console.WriteLine($"Queued {val} with priority {priority}");
}


void Process()
{
    if (queueItems.Count == 0)
    {
        Console.WriteLine("Queue is empty");
        return;
    }

    var process = queueItems
        .OrderByDescending(q => q.Priority)
        .ThenBy(q => q.CreatedAt)
        .FirstOrDefault();

    if (process != null)
    {
        Console.WriteLine($"Processed {process.Value}");
        queueItems.Remove(process);
    }
}

Enqueue("A", 1);
Enqueue("B", 5);
Enqueue("C", 5);

Process();
Process();
