using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFactory : MonoBehaviour
{
    private StackAssets _stackAssets;

    public void Construct(StackAssets stackAssets)
    {
        _stackAssets = stackAssets;
    }

    public StackObject GetStackObject(int colorIndex)
    {
        GameObject gameObject = PoolManager.Instance.BoxPool.GetObject();
        StackObject stackObject = gameObject.GetComponent<StackObject>();
        stackObject._meshRenderer.material = _stackAssets.GetMaterial(colorIndex);
        return stackObject;
    }
}
