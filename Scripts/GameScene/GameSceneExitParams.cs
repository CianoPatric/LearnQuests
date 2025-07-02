public class GameSceneExitParams
{
    public MainMenuEnterParams MainMenuEnterParams { get; }
    public GameSceneExitParams(MainMenuEnterParams mainMenuEnterParams)
    {
        MainMenuEnterParams = mainMenuEnterParams;
    }
}