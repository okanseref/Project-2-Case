using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackGarbageCollector : MonoBehaviour
{
    private List<StackObject> garbageList;
    private void Awake()
    {
        garbageList = new List<StackObject>();
    }
    public void ThrowToGarbage(StackObject stackObject)
    {
        garbageList.Add(stackObject);
    }
    public void DestroyImmediate(StackObject stackObject)
    {
        MainManager.Instance.PoolManager.BoxPool.ReturnObject(stackObject.gameObject);
    }
    public void ClearGarbages()
    {
        foreach (StackObject stackObject in garbageList)
        {
            DestroyImmediate(stackObject);
        }
        garbageList.Clear();
    }
}
