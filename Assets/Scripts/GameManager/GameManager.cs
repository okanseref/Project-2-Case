using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] float Speed;
        public MainCharacter MainCharacter { get; private set; }
        private InputHandler _inputHandler;

        public void Construct(MainCharacter mainCharacter, InputHandler inputHandler)
        {
            MainCharacter = mainCharacter;
            _inputHandler = inputHandler;
        }
        private void Start()
        {
            _inputHandler.SetInputEvent(MainManager.Instance.StackManager.DoStackAction);
            MainManager.Instance.StackManager.SetStepCompleteAction(StepComplete);
            MainCharacter.SetSpeed(Speed);
            MainCharacter.Run();
            MainManager.Instance.CameraManager.ChangeCam(CameraManager.CamType.Play);
            StartLevel(4);
        }
        private void StartLevel(int levelLength)
        {
            MainManager.Instance.StackManager.SetLevelLength(levelLength);
            MainManager.Instance.LevelManager.SetFinish(levelLength);
        }
        private void StepComplete()
        {
            MainCharacter.SetCenter(MainManager.Instance.StackManager.GetCenterPosition());
        }
        private void LevelComplete()
        {
            
        }

    }

}
