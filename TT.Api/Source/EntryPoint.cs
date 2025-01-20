using TT.Application;
using TT.Core.Interfaces;

internal static class EntryPoint
{
    private static IApplication _taskTrainApp = new TaskTrainAppliction();

    public static void Main(string[] args)
    {
        _taskTrainApp.Build(args);
        _taskTrainApp.Run();
    }
}