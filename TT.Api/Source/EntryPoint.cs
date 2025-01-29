using TT.Core;
using TT.Application;

internal static class EntryPoint
{
    private static ITTApp _taskTrainApp = new TaskTrainAppliction();

    public static void Main(string[] args)
    {
        _taskTrainApp.Build(args);
        _taskTrainApp.Run();
    }
}