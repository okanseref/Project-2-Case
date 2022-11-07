using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StackAssets", menuName = "ScriptableObjects/StackAssets")]
public class StackAssets : ScriptableObject
{
    [SerializeField] Material[] materials;
    [SerializeField] Vector3 defaultSize;
    public Material GetMaterial(int index)
    {
        return materials[Mathf.Clamp(index, 0, materials.Length)];
    }
    public Vector3 GetStackSize()
    {
        return defaultSize;
    }
}
