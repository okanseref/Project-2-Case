using DG.Tweening;
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
            StartLevel(3,true);
        }
        private void StartLevel(int levelLength,bool isFirstLevel)
        {
            MainManager.Instance.StackManager.SetLevelLength(levelLength, isFirstLevel);
            MainManager.Instance.LevelManager.SetFinish(levelLength, isFirstLevel);
        }
        private void StepComplete()
        {
            MainCharacter.SetCenter(MainManager.Instance.StackManager.GetCenterPosition());
        }
        public void LevelComplete()
        {
            MainCharacter.Stop();
            MainCharacter.Dance();
            MainManager.Instance.CameraManager.ChangeCam(CameraManager.CamType.Dance);
            Sequence waitAndPlay = DOTween.Sequence();
            waitAndPlay.AppendInterval(MainManager.Instance.CameraManager.danceTime+0.3f);
            waitAndPlay.AppendCallback(() =>
            {
                MainManager.Instance.CameraManager.ChangeCam(CameraManager.CamType.Play);
                StartLevel(2, false) ;
                MainCharacter.SetSpeed(Speed);
                MainCharacter.Run();
            });

        }

    }

}
