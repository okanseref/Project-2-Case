using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInputHandler : MonoBehaviour, InputHandler
{
    private Action _clickEvent;
    private bool _inputEnabled=false;
    public void EnableInput(bool enable)
    {
        _inputEnabled = enable;
    }

    public void SetInputEvent(Action clickEvent)
    {
        _clickEvent = clickEvent;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_inputEnabled)
            {
                _clickEvent();
            }
        }
    }
}
