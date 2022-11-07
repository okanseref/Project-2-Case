using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float danceTime = 1f;
    private CameraBase _playCam;
    private CameraDance _danceCam;

    public void Construct(CameraBase playCam, CameraDance danceCam)
    {
        _playCam = playCam;
        _danceCam = danceCam;
    }

    public void ChangeCam(CamType camType)
    {
        _playCam.ToggleCamera(camType is CamType.Play);
        _danceCam.ToggleCamera(camType is CamType.Dance);
    }
    public void Dance(Vector3 playerPosition,float time)
    {
        _danceCam.Dance(playerPosition, time);
    }

    public enum CamType
    {
        Play,
        Dance
    }
}
