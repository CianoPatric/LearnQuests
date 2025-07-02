using UnityEngine;
using R3;

public class GameSceneRootBinder:MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubj;
    public void HandleGoToMainMenuButtonClick()
    {
        _exitSceneSignalSubj?.OnNext(Unit.Default);
    }
    public void Bind(Subject<Unit> exitSceneSignalSubj)
    {
        _exitSceneSignalSubj = exitSceneSignalSubj;
    }    
}