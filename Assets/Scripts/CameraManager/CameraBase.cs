using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    protected CinemachineVirtualCamera Camera;

    public void Construct(CinemachineVirtualCamera camera)
    {
        Camera = camera;
    }
    public virtual void ToggleCamera(bool isOn)
    {
        Camera.enabled = isOn;
    }
}
