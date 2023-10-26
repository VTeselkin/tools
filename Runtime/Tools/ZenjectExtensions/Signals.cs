namespace GameSoft.Tools.ZenjectExtensions
{
    public class AppLaunchedSignal
    {
    }

    public class ApplicationPause
    {
    }

    public class ApplicationUnpause
    {
    }

    public class AppSaveSignal
    {
    }

    public class ApplicationFocus
    {
        public bool HasFocus;

        public ApplicationFocus(bool hasFocus)
        {
            HasFocus = hasFocus;
        }
    }
}