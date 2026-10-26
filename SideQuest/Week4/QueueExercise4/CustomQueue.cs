namespace QueueExercise4;

public class CustomQueue
{
    private Dictionary<string, int> _queueRules = new Dictionary<string, int>();

    private List<QueueItem> _queueItems = new();

    private record struct QueueItem
    {
        public string Value { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public void AddRule(string keyword, int priority)
    {
        _queueRules.Add(keyword, priority);
        Console.WriteLine($"Added rule: {keyword} with priority {priority}");
    }

    public void Enqueue(string value)
    {
        bool hasMatchRule = _queueRules.ContainsKey(value);
        QueueItem item = new QueueItem
        {
            Value = value,
            Priority = (hasMatchRule) ? _queueRules[value] : 0,
            CreatedAt = DateTime.UtcNow
        };
        
        _queueItems.Add(item);
        Console.WriteLine($"Queued {item.Value} with priority {item.Priority}");
    }

    public void Process()
    {
        if (_queueItems.Count == 0)
        {
            Console.WriteLine("Queue is empty");
            return;
        }

        var process = _queueItems
            .OrderByDescending(q => q.Priority)
            .ThenBy(q => q.CreatedAt)
            .First();

        Console.WriteLine($"Processed {process.Value}");
        _queueItems.Remove(process);
    }
}