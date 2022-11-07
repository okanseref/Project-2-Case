using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MainCharacter
{
    public void Run();
    public void Dance();
    public void Stop();
    public bool IsRunning();
    public void SetCenter(float xPosition);
    public Vector3 GetPosition();
    public void SetSpeed(float speed);
}
