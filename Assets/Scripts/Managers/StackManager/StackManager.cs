using DG.Tweening;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public StackAssets StackAssetsObject { get; private set; }
    public StackGarbageCollector StackGarbageCollector { get; private set; }

    private StackArranger _stackArranger;
    private int _remainingStacks = 0;
    private Action _stepCompleteAction;
    public void Construct(StackAssets stackAssetsObject, StackArranger stackArranger,StackGarbageCollector stackGarbageCollector)
    {
        StackAssetsObject = stackAssetsObject;
        StackGarbageCollector = stackGarbageCollector;
        _stackArranger = stackArranger;
    }
    public void SetLevelLength(int levelLength, bool isFirstLevel)
    {
        if (isFirstLevel)
        {
            _stackArranger.CreateFirstStack();
        }
        StackGarbageCollector.ClearGarbages(); // clear old level
        _remainingStacks = levelLength;
        _stackArranger.MoveNewStack();
        MainManager.Instance.SoundManager.RefreshPitch();
    }
    public void SetStepCompleteAction(Action action)
    {
        _stepCompleteAction = action;
    }
    public float GetZPosition()
    {
        return _stackArranger.GetZPosition();
    }
    public void DoStackAction()
    {
        bool isValidCut = false;
        _remainingStacks--;
        if (_remainingStacks >= 0)
        {
            isValidCut = _stackArranger.CalculateMerge();
            _stepCompleteAction();
        }
        if (_remainingStacks > 0 && isValidCut)
        {
            _stackArranger.MoveNewStack();
        }

        if (_remainingStacks == 0)
        {
            _stackArranger.PrepareForNewLevel();
        }
    }
    public float GetCenterPosition()
    {
        return _stackArranger.GetStackCenterPosition();
    }
}
