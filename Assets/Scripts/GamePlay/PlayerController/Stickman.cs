using DG.Tweening;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour, MainCharacter
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private float _speed = 0;
    private bool _runnable = false;


    public void Construct(Animator animator,Rigidbody rigidbody)
    {
        _animator = animator;
        _rigidbody = rigidbody;
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
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        SetCenter(0);
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void Stop()
    {
        _runnable = false;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -2 && _runnable)
        {
            //fail
            _runnable = false;
            MainManager.Instance.GameManager.LevelFailed();
        }
        if (_runnable)
        {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _speed);
        }
        else
        {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        }
    }

    public bool IsRunning()
    {
        return _runnable;
    }
}
