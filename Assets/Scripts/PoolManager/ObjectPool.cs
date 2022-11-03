using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject _objectPrefab;

    private Queue<GameObject> _queue;

    private void Start()
    {
        _queue = new Queue<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            CreateNewObject();
        }
    }
    public void ReturnObject(GameObject objectReturned)
    {
        objectReturned.SetActive(false);
        _queue.Enqueue(objectReturned);
    }
    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(_objectPrefab, null);
        newObject.SetActive(false);
        _queue.Enqueue(newObject);
    }
    public GameObject GetObject()
    {
        if (_queue.Count < 3)
        {
            CreateNewObject();
        }
        return _queue.Dequeue();
    }
}
