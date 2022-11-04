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

        private MainCharacter _mainCharacter;
        private StackManager _stackManager;
        private InputHandler _inputHandler;
        private LevelManager _levelManager;

        public void Construct(StackAssets stackAssets, MainCharacter mainCharacter, StackManager stackManager, InputHandler inputHandler, LevelManager levelManager)
        {
            StackAssets = stackAssets;
            _mainCharacter = mainCharacter;
            _stackManager = stackManager;
            _inputHandler = inputHandler;
            _levelManager = levelManager;
        }
        private void Start()
        {
            _inputHandler.SetInputEvent(_stackManager.DoStackAction);
            _stackManager.SetStepCompleteAction(StepComplete);
            _mainCharacter.SetSpeed(Speed);
            _mainCharacter.Run();
            StartLevel(4);
        }
        private void StartLevel(int levelLength)
        {
            _stackManager.SetLevelLength(levelLength);
            _levelManager.SetFinish(levelLength);
        }
        private void StepComplete()
        {
            _mainCharacter.SetCenter(_stackManager.GetCenterPosition());
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
