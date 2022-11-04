using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInputHandler : MonoBehaviour, InputHandler
{
    private Action _clickEvent;
    public void SetInputEvent(Action clickEvent)
    {
        _clickEvent = clickEvent;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickEvent();
        }
    }
}
