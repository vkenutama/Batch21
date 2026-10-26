namespace StackExercise4;

public class CustomStack
{
    private readonly Stack<string> _stack = new Stack<string>();
    private readonly Stack<string> _redoStack = new Stack<string>();
    private readonly List<Func<string, bool>> _rules = new List<Func<string, bool>>();
    
    private const int HistoryLimit = 3;

    public void AddValidationRule(Func<string, bool> rule)
    {
        _rules.Add(rule);
    }
    
    public void Type(string word)
    {
        bool passed = _rules.All(rule => rule(word));
        if (!passed)
        {
            Console.WriteLine("Rejected");
            return;
        }
        
        List<string> stackDatas = new();
    
        string msg = "";
        
        if (_stack.Count + 1 > HistoryLimit)
        {
            while (_stack.TryPop(out var item))
            {
                stackDatas.Add(item);
            }
    
            stackDatas.Reverse();
    
            foreach (var data in stackDatas[1..])
            {
                _stack.Push(data);
            }
    
            msg = $"Dropped bottom, Typed {word}";
        }
        else
        {
            msg = $"Typed {word}";
        }
    
        _stack.Push(word);
        _redoStack.Clear();
        Console.WriteLine(msg);
    }
    
    public void Undo()
    {
        if (_stack.Count == 0)
        {
            Console.WriteLine("Stack is empty");
            return;
        }
    
        string word = _stack.Pop();
        _redoStack.Push(word);
    
        Console.WriteLine($"Undid {word}");
    }
    
    public void Redo()
    {
        if (_redoStack.Count == 0)
        {
            Console.WriteLine("Redo stack is empty");
            return;
        }
    
        string word = _redoStack.Pop();
        _stack.Push(word);
        
        Console.WriteLine($"Redid {word}");
    }
    
}