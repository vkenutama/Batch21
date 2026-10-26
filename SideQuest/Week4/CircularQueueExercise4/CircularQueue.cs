namespace CircularQueueExercise4;

public class CircularQueue
{
    private int _bufferCapacity = 3;
    private readonly Queue<int> _queue = new Queue<int>();
    private bool _overwriteEnabled = false;

    public void SetCapacity(int bufferCapacity)
    {
        _bufferCapacity = bufferCapacity;
        _queue.Clear();
    }

    public void SetOverwritePolicy(bool isOverwriteEnabled)
    {
        _overwriteEnabled = isOverwriteEnabled;
    }

    public void Log(int val)
    {
        int countAfter = _queue.Count + 1;
        bool isFullAfterAdding = _queue.Count == _bufferCapacity;

        // Reject if policy is false
        if (isFullAfterAdding && !_overwriteEnabled)
        {
            Console.WriteLine("Value rejected");
            return;
        }

        float queueCapacity = (float)_queue.Count / _bufferCapacity;

        if (queueCapacity >= 0.66f && queueCapacity < 1.0f)
        {
            Console.WriteLine($"Warning: Buffer at {0.66f:P0}");
        }

        if (countAfter > _bufferCapacity)
        {
            Console.WriteLine("Critical: Buffer Full");

            bool success = _queue.TryDequeue(out _);
            if (success)
            {
                _queue.Enqueue(val);
                Console.WriteLine($"Overwritten oldest with {val}");
            }

            return;
        }

        _queue.Enqueue(val);
        Console.WriteLine($"Logged {val}");
    }

    public void Read()
    {
        if (_queue.TryDequeue(out int val))
        {
            Console.WriteLine($"Read {val}");
        }
    }
}