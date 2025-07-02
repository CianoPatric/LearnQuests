using UnityEngine;
using R3;

public class GameSceneEntryPoint: MonoBehaviour
{
    [SerializeField] private GameSceneRootBinder _sceneUIRootPrefab;
    // ReSharper disable Unity.PerformanceAnalysis
    public Observable<GameSceneExitParams> Run(DIContainer container, GameSceneEnterParams gameSceneEnterParams)
    {
        GameSceneRegistrationDI.Register(container, gameSceneEnterParams);
        var gameSceneViewModelContainer = new DIContainer(container);
        GameSceneViewDIRegistration.Register(gameSceneViewModelContainer);
        
        var uiScene = Instantiate(_sceneUIRootPrefab);
        var uiRoot = container.Resolve<MainMenuRootView>();
        uiRoot.AttachSceneUI(uiScene.gameObject);
        
        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);
        Debug.Log($"{gameSceneEnterParams.Width} x {gameSceneEnterParams.Height}");
        //var grid = Object.FindFirstObjectByType<BuildingsGrid>();
        //var cam = Object.FindFirstObjectByType<ControllCamera>();
        //(cam as IInjectable)?.Inject(container);
        //(grid as IInjectable)?.Inject(container);
        var enterParams = new MainMenuEnterParams("Fatality");
        var exitParams = new GameSceneExitParams(enterParams);
        var exitToUISceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        return exitToUISceneSignal;
    }
}