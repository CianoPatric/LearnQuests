using System.Collections;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint 
{ 
    private static GameEntryPoint _instance;
    private Coroutines _coroutines;
    private MainMenuRootView _menuRoot;
    private readonly DIContainer _rootContainer = new();
    private DIContainer CasheContainer;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void FirstLoad()
    {
        _instance = new GameEntryPoint();
        _instance.StartAPP();
    }

    private GameEntryPoint()
    {
        _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);
        var prefabUIRoot = Resources.Load<MainMenuRootView>("MenuRoot");
        _menuRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_menuRoot.gameObject);
        _rootContainer.RegisterInstance(_menuRoot);
    }
    private void StartAPP()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "MainMenu")
        {
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
            return;
        }
        if(sceneName == "GameScene")
        {
            GameSceneEnterParams _gameEnter = new GameSceneEnterParams(3, 3, true);
            _coroutines.StartCoroutine(LoadAndStartGameScene(_gameEnter));
            return;
        }
        if (sceneName != "Boot")
        {
            return;
        }
#endif
        _coroutines.StartCoroutine(LoadAndStartMainMenu());
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
    {
        _menuRoot.ShowLoadingScreen();
        CasheContainer?.Dispose();
        yield return LoadScene("Boot");
        yield return LoadScene("MainMenu");
        yield return new WaitForSeconds(0.5f);
        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        var UIContainer = CasheContainer = new DIContainer(_rootContainer);
        sceneEntryPoint.Run(UIContainer, enterParams).Subscribe(mainMenuExitParams =>
        {
            _coroutines.StartCoroutine(LoadAndStartGameScene(mainMenuExitParams.GameSceneEnterParams));
        });
        _menuRoot.HideLoadingScreen();
    }
    private IEnumerator LoadAndStartGameScene(GameSceneEnterParams gameSceneEnterParams)
    {
        _menuRoot.ShowLoadingScreen();
        CasheContainer?.Dispose();
        yield return LoadScene("Boot");
        yield return LoadScene("GameScene");
        yield return new WaitForSeconds(0.5f);
        var sceneEntryPoint = Object.FindFirstObjectByType<GameSceneEntryPoint>();
        var gameSceneContainer = CasheContainer = new DIContainer(_rootContainer);
        sceneEntryPoint.Run(gameSceneContainer, gameSceneEnterParams).Subscribe(gameSceneExitParams =>
        {
            _coroutines.StartCoroutine(LoadAndStartMainMenu(gameSceneExitParams.MainMenuEnterParams));
        });
        _menuRoot.HideLoadingScreen();
    }
    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    } 
}