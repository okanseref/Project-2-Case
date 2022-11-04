using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObject : MonoBehaviour
{
    public BoxCollider StackBoxCollider { get; private set; }
    public MeshRenderer StackMeshRenderer { get; private set; }
    public Rigidbody IRigidbody { get; private set; }
    public void Construct(BoxCollider boxCollider, MeshRenderer meshRenderer,Rigidbody rigidbody)
    {
        StackBoxCollider = boxCollider;
        StackMeshRenderer = meshRenderer;
        IRigidbody = rigidbody;
    }
}
