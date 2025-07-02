public class GameSceneEnterParams 
{ 
    public int Width { get; } 
    public int Height { get; } 
    public bool GameMode { get; }
    
    public GameSceneEnterParams(int width, int height, bool gameMode) 
    { 
        Width = width; 
        Height = height; 
        GameMode = gameMode;
    }
}