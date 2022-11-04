using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFactory : MonoBehaviour
{
    public StackObject GetStackObject(int colorIndex)
    {
        GameObject gameObject = PoolManager.Instance.BoxPool.GetObject();
        gameObject.SetActive(true);
        gameObject.transform.localScale = GameManager.Instance.StackAssets.GetStackSize();
        StackObject stackObject = gameObject.GetComponent<StackObject>();
        stackObject.StackMeshRenderer.material = GameManager.Instance.StackAssets.GetMaterial(colorIndex);
        return stackObject;
    }
    public void DestroyStackObject(StackObject stackObjectToDestroy)
    {
        PoolManager.Instance.BoxPool.ReturnObject(stackObjectToDestroy.gameObject);
    }
}
