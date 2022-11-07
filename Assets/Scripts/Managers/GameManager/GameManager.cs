using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] float Speed;
        public MainCharacter MainCharacter { get; private set; }
        private InputHandler _inputHandler;
        private bool _gameRunning;

        public void Construct(MainCharacter mainCharacter, InputHandler inputHandler)
        {
            MainCharacter = mainCharacter;
            _inputHandler = inputHandler;
        }
        private void Start()
        {
            _gameRunning = true;
            _inputHandler.SetInputEvent(MainManager.Instance.StackManager.DoStackAction);
            MainManager.Instance.StackManager.SetStepCompleteAction(StepComplete);
            MainCharacter.SetSpeed(Speed);
            MainManager.Instance.CameraManager.ChangeCam(CameraManager.CamType.Play);
            StartLevel(Random.Range(11,13),true);
        }
        private void StartLevel(int levelLength,bool isFirstLevel)
        {
            MainManager.Instance.StackManager.SetLevelLength(levelLength, isFirstLevel);
            MainManager.Instance.LevelManager.SetFinish(levelLength, isFirstLevel);
            _inputHandler.EnableInput(true);
        }
        private void StepComplete()
        {
            if (!MainCharacter.IsRunning())
            {
                MainCharacter.Run();
            }
            MainCharacter.SetCenter(MainManager.Instance.StackManager.GetCenterPosition());
        }
        public void LevelComplete()
        {
            MainManager.Instance.UIManager.ShowScreen(UIManager.ScreenType.Success);
            _inputHandler.EnableInput(false);
            MainCharacter.Stop();
            MainCharacter.Dance();
            MainManager.Instance.CameraManager.ChangeCam(CameraManager.CamType.Dance);
            Sequence waitAndPlay = DOTween.Sequence();
            waitAndPlay.AppendInterval(MainManager.Instance.CameraManager.danceTime+0.3f);
            waitAndPlay.AppendCallback(() =>
            {
                MainManager.Instance.CameraManager.ChangeCam(CameraManager.CamType.Play);
                StartLevel(Random.Range(11, 13), false) ;
                MainManager.Instance.UIManager.ShowScreen(UIManager.ScreenType.None);

            });

        }
        public void LevelFailed()
        {
            if (!_gameRunning)
            {
                return;
            }
            _inputHandler.EnableInput(false);
            _gameRunning = false;
            MainManager.Instance.UIManager.ShowScreen(UIManager.ScreenType.Fail);
            Sequence waitAndRestart = DOTween.Sequence();
            waitAndRestart.AppendInterval(1f);
            waitAndRestart.AppendCallback(() =>
            {
                MainManager.Instance.UIManager.ShowScreen(UIManager.ScreenType.None);
                SceneManager.LoadScene(0);
            });
        }

    }

}
