using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour, MainCharacter
{
    private Animator _animator;
    private float _speed = 0;
    private bool _runnable = false;


    public void Construct(Animator animator)
    {
        _animator = animator;
    }

    public void SetCenter(float xPosition)
    {
        transform.DOMoveX(xPosition, 0.2f);
    }
    public void Dance()
    {
        _animator.SetTrigger("dance");
    }

    public void Run()
    {
        _runnable = true;
        _animator.SetTrigger("run");
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void Stop()
    {
        _runnable = false;
    }
    private void Update()
    {
        if (_runnable)
        {
            transform.position = transform.position + Vector3.forward * Time.deltaTime * _speed;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
