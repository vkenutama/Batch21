Stack<string> stack = new Stack<string>();
Stack<string> redoStack = new Stack<string>();

const int historyLimit = 3;

void Type(string word)
{
    List<string> stackDatas = new();

    string msg = "";
    
    if (stack.Count + 1 > historyLimit)
    {
        while (stack.TryPop(out var item))
        {
            stackDatas.Add(item);
        }

        stackDatas.Reverse();

        foreach (var data in stackDatas[1..])
        {
            stack.Push(data);
        }

        msg = $"Dropped bottom, Typed {word}";
    }
    else
    {
        msg = $"Typed {word}";
    }

    stack.Push(word);
    redoStack.Clear();
    Console.WriteLine(msg);
}

void Undo()
{
    if (stack.Count == 0)
    {
        Console.WriteLine("Stack is empty");
        return;
    }

    string word = stack.Pop();
    redoStack.Push(word);

    Console.WriteLine($"Undid {word}");
}

void Redo()
{
    if (redoStack.Count == 0)
    {
        Console.WriteLine("Redo stack is empty");
        return;
    }

    string word = redoStack.Pop();
    stack.Push(word);
    
    Console.WriteLine($"Redid {word}");
}

Type("A");
Type("B");
Undo();
Redo();