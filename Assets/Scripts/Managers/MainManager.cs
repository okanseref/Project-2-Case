using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;

        public StackManager StackManager { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public CameraManager CameraManager { get; private set; }
        public SoundManager SoundManager { get; private set; }
        public GameManager GameManager { get; private set; }
        public PoolManager PoolManager { get; private set; }
        public UIManager UIManager { get; private set; }

        public void Construct(StackManager stackManager, LevelManager levelManager, CameraManager cameraManager, SoundManager soundManager, GameManager gameManager, PoolManager poolManager, UIManager uIManager)
        {
            StackManager = stackManager;
            LevelManager = levelManager;
            CameraManager = cameraManager;
            SoundManager = soundManager;
            GameManager = gameManager;
            PoolManager = poolManager;
            UIManager = uIManager;
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