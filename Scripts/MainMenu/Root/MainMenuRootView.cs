using UnityEngine;

public class MainMenuRootView: MonoBehaviour
{ 
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Transform _mainSceneContainer;
    private void Awake()
    { 
        HideLoadingScreen();
    }
    public void ShowLoadingScreen()
    {
        _loadingScreen.SetActive(true);
    }
    public void HideLoadingScreen()
    { 
        _loadingScreen.SetActive(false);
    }
    public void AttachSceneUI(GameObject sceneUI)
    { 
        ClearSceneUI(); 
        sceneUI.transform.SetParent(_mainSceneContainer, false);
    }
    private void ClearSceneUI()
    { 
        var childCount = _mainSceneContainer.childCount; 
        for(int i = 0; i < childCount; i++)
        { 
            Destroy( _mainSceneContainer.GetChild(i).gameObject);
        }
    }
}