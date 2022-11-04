using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackArranger : MonoBehaviour
{
    private StackFactory _stackFactory;
    private int _colorIndex = 0;
    private float _zPosition = 0;
    private Tween _tween;
    public StackObject CurrentStack { get; private set; }
    public StackObject NextStack { get; private set; }
    public void Construct(StackFactory stackFactory)
    {
        _stackFactory = stackFactory;
    }
    private void Failed()
    {
        _stackFactory.DestroyStackObject(NextStack);
        NextStack = null;
    }
    private void CreateFallStack(float leftBound, float rightBound)
    {
        StackObject fallStack = _stackFactory.GetStackObject(_colorIndex - 1);
        fallStack.transform.position = new Vector3((leftBound + rightBound) / 2, NextStack.transform.position.y, NextStack.transform.position.z);
        fallStack.transform.localScale = new Vector3(Mathf.Abs(leftBound - rightBound), NextStack.transform.localScale.y, NextStack.transform.localScale.z);
        fallStack.transform.DOMoveY(-10, 2f).OnComplete(() => {
            _stackFactory.DestroyStackObject(fallStack);
        });
    }
    public void CreateFirstStack()
    {
        CurrentStack = _stackFactory.GetStackObject(_colorIndex);
        CurrentStack.transform.position = new Vector3(0, -0.5f, _zPosition);
        _colorIndex++;
        MoveNewStack();
    }
    public void MoveNewStack()
    {
        NextStack = _stackFactory.GetStackObject(_colorIndex);
        _zPosition += NextStack.transform.localScale.z;
        NextStack.transform.position = new Vector3(4, -0.5f, _zPosition);
        _tween = NextStack.transform.DOMoveX(-4, 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Failed
            Failed();
        });
        _colorIndex++;
    }
    public void CalculateMerge()
    {
        float leftX = CurrentStack.transform.position.x - CurrentStack.StackBoxCollider.bounds.extents.x;
        float rightX = CurrentStack.transform.position.x + CurrentStack.StackBoxCollider.bounds.extents.x;
        float nextLeftX = NextStack.transform.position.x - NextStack.StackBoxCollider.bounds.extents.x;
        float nextRightX = NextStack.transform.position.x + NextStack.StackBoxCollider.bounds.extents.x;
        float solidPieceLeft;
        float solidPieceRight;

        _tween.Pause();

        if (nextLeftX < rightX && nextRightX > leftX)
        {
            if (nextLeftX < leftX && nextRightX > rightX) // multiple cut
            {
                solidPieceLeft = leftX;
                solidPieceRight = rightX;
                CreateFallStack(rightX, nextRightX);
                CreateFallStack(nextLeftX, leftX);

            }
            else // single cut
            {
                if (nextRightX > rightX)
                {
                    solidPieceLeft = nextLeftX;
                    solidPieceRight = rightX;
                    CreateFallStack(rightX, nextRightX);
                }
                else
                {
                    solidPieceLeft = leftX;
                    solidPieceRight = nextRightX;
                    CreateFallStack(nextLeftX, leftX);
                }
            }
            print("Valid cut");

            NextStack.transform.position = new Vector3((solidPieceLeft + solidPieceRight) / 2, NextStack.transform.position.y, NextStack.transform.position.z);
            NextStack.transform.localScale = new Vector3(Mathf.Abs(solidPieceRight - solidPieceLeft), NextStack.transform.localScale.y, NextStack.transform.localScale.z);
            CurrentStack = NextStack;
        }
        else // Failed to merge
        {
            CreateFallStack(nextLeftX, nextRightX);
            Failed();
        }

    }
}
