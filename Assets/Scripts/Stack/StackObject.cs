using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObject : MonoBehaviour
{
    public BoxCollider _boxCollider { get; private set; }
    public MeshRenderer _meshRenderer { get; private set; }
    public void Construct(BoxCollider boxCollider, MeshRenderer meshRenderer)
    {
        _boxCollider = boxCollider;
        _meshRenderer = meshRenderer;
    }
}
