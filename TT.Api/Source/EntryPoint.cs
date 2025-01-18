using TT;
using TT.Interfaces;

internal static class EntryPoint
{
    private static IApplication _taskTrainApp = new TaskTrainAppliction();

    public static void Main(string[] args)
    {
        _taskTrainApp.Build(args);
        _taskTrainApp.Run();
    }
}