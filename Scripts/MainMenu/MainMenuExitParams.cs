public class MainMenuExitParams 
{
    public GameSceneEnterParams GameSceneEnterParams { get; }
    public MainMenuExitParams(GameSceneEnterParams gameSceneEnterParams)
    {
        GameSceneEnterParams = gameSceneEnterParams;
    }
}