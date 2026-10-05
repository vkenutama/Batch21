Stack<string> stack = new Stack<string>();

void Type(string word)
{
    stack.Push(word);
    Console.WriteLine($"Typed {word}");
}

void Undo()
{
    if (stack.Count == 0)
        return;

    string word = stack.Pop();
    Console.WriteLine($"Undid {word}");
}

Type("foo");
Type("bar");
Undo();
Undo();
