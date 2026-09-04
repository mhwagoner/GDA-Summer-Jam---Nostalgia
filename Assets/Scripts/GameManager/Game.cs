public class Game
{
    public AudioController audioController;

    private static Game _instance;

    public static Game Instance
    {
        get
        {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    private Game()
    {

    }
}