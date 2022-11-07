using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] float Speed;
        public StackAssets StackAssets { get; private set; }

        public MainCharacter MainCharacter { get; private set; }
        private StackManager _stackManager;
        private InputHandler _inputHandler;
        private LevelManager _levelManager;
        private CameraManager _cameraManager;

        public void Construct(StackAssets stackAssets, MainCharacter mainCharacter, StackManager stackManager, InputHandler inputHandler, LevelManager levelManager,CameraManager cameraManager)
        {
            StackAssets = stackAssets;
            MainCharacter = mainCharacter;
            _stackManager = stackManager;
            _inputHandler = inputHandler;
            _levelManager = levelManager;
            _cameraManager = cameraManager;
        }
        private void Start()
        {
            _inputHandler.SetInputEvent(_stackManager.DoStackAction);
            _stackManager.SetStepCompleteAction(StepComplete);
            MainCharacter.SetSpeed(Speed);
            //_mainCharacter.Run();
            _cameraManager.ChangeCam(CameraManager.CamType.Dance);
            StartLevel(4);
        }
        private void StartLevel(int levelLength)
        {
            _stackManager.SetLevelLength(levelLength);
            _levelManager.SetFinish(levelLength);
        }
        private void StepComplete()
        {
            MainCharacter.SetCenter(_stackManager.GetCenterPosition());
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }
    }

}
