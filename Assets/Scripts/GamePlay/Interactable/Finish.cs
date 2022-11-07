using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private MeshFilter _mesh;
    private bool _hasCompleted = false;

    public void Construct(MeshFilter mesh)
    {
        _mesh = mesh;
    }
    public float GetZLength()
    {
        return _mesh.mesh.bounds.size.z * _mesh.transform.localScale.z;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")&& !_hasCompleted)
        {
            _hasCompleted = true;
            MainManager.Instance.GameManager.LevelComplete();
        }
    }
}
