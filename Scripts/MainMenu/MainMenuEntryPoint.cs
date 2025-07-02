using UnityEngine;
using R3;

public class MainMenuEntryPoint: MonoBehaviour
{
    [SerializeField] private MainMenuRootBinder _sceneUIRootPrefab;

    private Subject<MainMenuExitParams> _exitSceneSignalSubj = new();

    public Observable<MainMenuExitParams> Run(DIContainer container, MainMenuEnterParams uiEnterParams)
    { 
        MainMenuRegistrationDI.Register(container, uiEnterParams); 
        var uiViewModelContainer = new DIContainer(container); 
        MainMenuViewDIRegistration.Register(uiViewModelContainer);
        
        var uiScene = Instantiate(_sceneUIRootPrefab); 
        var uiRoot = container.Resolve<MainMenuRootView>(); 
        uiRoot.AttachSceneUI(uiScene.gameObject);
        
        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);
        var enterParams = new GameSceneEnterParams(3,3, false); //надо вводить данные
        var exitParams = new MainMenuExitParams(enterParams);
        var exitToUISceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        return exitToUISceneSignal;
    }
}