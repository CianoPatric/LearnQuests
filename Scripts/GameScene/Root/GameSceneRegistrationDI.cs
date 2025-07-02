public static class GameSceneRegistrationDI
{
    public static void Register(DIContainer container, GameSceneEnterParams gameEnterParams)
    {
        container.RegisterInstance(gameEnterParams);
    }
}