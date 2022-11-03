using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StackAssets", menuName = "ScriptableObjects/StackAssets")]
public class StackAssets : ScriptableObject
{
    [SerializeField] Material[] materials;
    void Start()
    {
        
    }
    public Material GetMaterial(int index)
    {
        return materials[Mathf.Clamp(index, 0, materials.Length)];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
