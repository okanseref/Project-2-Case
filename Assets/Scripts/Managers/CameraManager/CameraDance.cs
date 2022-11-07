using DG.Tweening;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDance : CameraBase
{
    public AnimationCurve ease;
    public AnimationCurve ease2;
    public override void ToggleCamera(bool isOn)
    {
        base.ToggleCamera(isOn);
        if (isOn)
        {
            Dance(MainManager.Instance.GameManager.MainCharacter.GetPosition(), MainManager.Instance.CameraManager.danceTime);
        }
    }
    public void Dance(Vector3 playerPosition, float time)
    {
        Ease ease = Ease.InSine;
        Ease ease2 = Ease.OutSine;
        float distance = 8;
        Vector3 camPosition = playerPosition;
        camPosition.y += 10;
        camPosition.z -= distance;
        base.Camera.transform.position = camPosition;
        camPosition.z += distance;
        time /= 4; // because of circle function
        Sequence danceSequence = DOTween.Sequence();
        danceSequence.Append(base.Camera.transform.DOMoveZ(camPosition.z, time).SetEase(ease));
        danceSequence.Join(base.Camera.transform.DOMoveX(camPosition.x - distance, time).SetEase(ease2));

        danceSequence.Append(base.Camera.transform.DOMoveZ(camPosition.z + distance, time).SetEase(ease2));
        danceSequence.Join(base.Camera.transform.DOMoveX(camPosition.x, time).SetEase(ease));

        danceSequence.Append(base.Camera.transform.DOMoveZ(camPosition.z, time).SetEase(ease));
        danceSequence.Join(base.Camera.transform.DOMoveX(camPosition.x + distance, time).SetEase(ease2));

        danceSequence.Append(base.Camera.transform.DOMoveZ(camPosition.z - distance, time).SetEase(ease2));
        danceSequence.Join(base.Camera.transform.DOMoveX(camPosition.x, time).SetEase(ease));

    }
}
