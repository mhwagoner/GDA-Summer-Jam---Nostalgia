public class EventBus
{
    private static EventBus _instance;

    public static EventBus Instance
    {
        get
        {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    private EventBus()
    {

    }
}