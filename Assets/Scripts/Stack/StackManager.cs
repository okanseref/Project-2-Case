using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour 
{
    public StackAssets StackAssetsObject { get; private set; }
    private StackArranger _stackArranger;
    private int remainingStacks=0;
    private Action stepCompleteAction;
    public void Construct(StackAssets stackAssetsObject, StackArranger stackArranger)
    {
        StackAssetsObject = stackAssetsObject;
        _stackArranger = stackArranger;
    }
    public void SetLevelLength(int levelLength)
    {
        _stackArranger.CreateFirstStack();
        remainingStacks = levelLength;
    }
    public void SetStepCompleteAction(Action action)
    {
        stepCompleteAction = action;
    }
    public void DoStackAction()
    {
        remainingStacks--;
        if (remainingStacks >= 0)
        {
            _stackArranger.CalculateMerge();
            stepCompleteAction();
        }
        if (remainingStacks > 0)
        {
            _stackArranger.MoveNewStack();
        }
    }
    public float GetCenterPosition()
    {
        return _stackArranger.CurrentStack.transform.position.x;
    }
}
