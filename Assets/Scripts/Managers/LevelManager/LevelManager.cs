using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _finishPrefab;
    private GameObject _currentFinish;
    private GameObject _oldFinish;
    public void SetFinish(int levelLength, bool isFirstLevel)
    {
        if (_oldFinish != null)
        {
            Destroy(_oldFinish);
        }
        float stackSizeZ = MainManager.Instance.StackManager.StackAssetsObject.GetStackSize().z;
        _currentFinish = Instantiate(_finishPrefab);
        if (isFirstLevel)
        {
            _currentFinish.transform.position = new Vector3(0, 0,  (stackSizeZ * (levelLength + 0.5f)));

        }
        else
        {
            _currentFinish.transform.position = new Vector3(0, 0, MainManager.Instance.StackManager.GetZPosition() + (stackSizeZ * (levelLength-1 + 0.5f)));

        }
    }
    public float GetZLengthOfFinish()
    {
        Finish finish;
        if (_currentFinish.TryGetComponent(out finish)){
            return finish.GetZLength();
        }
        return 1;
    }
}
