using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFactory : MonoBehaviour
{
    public StackObject GetStackObject(int colorIndex)
    {
        GameObject gameObject = MainManager.Instance.PoolManager.BoxPool.GetObject();
        gameObject.SetActive(true);
        gameObject.transform.localScale = MainManager.Instance.StackManager.StackAssetsObject.GetStackSize();
        StackObject stackObject = gameObject.GetComponent<StackObject>();
        stackObject.StackMeshRenderer.material = MainManager.Instance.StackManager.StackAssetsObject.GetMaterial(colorIndex);
        return stackObject;
    }
}
