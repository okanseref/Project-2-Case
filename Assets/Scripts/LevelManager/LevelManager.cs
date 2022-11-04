using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _finishPrefab;
    private GameObject _currentFinish;
    private GameObject _oldFinish;
    public void SetFinish(int levelLength)
    {
        if (_oldFinish != null)
        {
            Destroy(_oldFinish);
        }
        float stackSizeZ = GameManager.Instance.StackAssets.GetStackSize().z;
        _currentFinish = Instantiate(_finishPrefab);
        _currentFinish.transform.position = new Vector3(0, 0, stackSizeZ * (levelLength+0.5f));
    }
}
