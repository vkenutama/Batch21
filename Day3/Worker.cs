namespace Day3;

interface IWorkable
{
    void DoTask(string taskName);
}

public class Worker : IWorkable
{
    public virtual void DoTask(string taskName)
    {
        Console.WriteLine($"[Worker] is doing a task: {taskName}");
    }
}

public class Manager : Worker
{
    public sealed override void DoTask(string taskName)
    {
        Console.WriteLine($"[Manager/Assistant] is doing a task: {taskName}");
    }
}

public class AssistantManager : Manager
{
    // Cannot override inherited method 'void Day3.Manager.DoTask(string)' because it is sealed
    // public override void DoTask(string taskName)
    // {
    //     
    // }
}