using UnityEngine;
using R3;

public class MainMenuRootBinder: MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubj;
    public void HandleGoToGameButtonClick()
    {
        _exitSceneSignalSubj?.OnNext(Unit.Default);
    }
    public void Bind(Subject<Unit> exitSceneSignalSubj)
    {
        _exitSceneSignalSubj = exitSceneSignalSubj;
    }
}