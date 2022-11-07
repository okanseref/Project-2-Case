using DG.Tweening;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackArranger : MonoBehaviour
{
    public float Tolerance = 0.5f;
    private StackObject _currentStack;
    private StackObject _referenceStack;
    private StackObject _nextStack;
    private StackFactory _stackFactory;
    private int _colorIndex = 0;
    private float _zPosition = 0;
    private Tween _tween;

    public void Construct(StackFactory stackFactory)
    {
        _stackFactory = stackFactory;
    }
    private void Failed()
    {
        MainManager.Instance.StackManager.StackGarbageCollector.DestroyImmediate(_nextStack);
        _nextStack = null;
    }
    public void PrepareForNewLevel()
    {
        _zPosition += MainManager.Instance.LevelManager.GetZLengthOfFinish();
        _currentStack = _referenceStack;
        _colorIndex = 0;
    }
    public float GetZPosition()
    {
        return _zPosition;
    }
    public float GetStackCenterPosition()
    {
        return _currentStack.transform.position.x;
    }
    private void CreateFallStack(float leftBound, float rightBound)
    {
        StackObject fallStack = _stackFactory.GetStackObject(_colorIndex - 1);
        fallStack.transform.position = new Vector3((leftBound + rightBound) / 2, _nextStack.transform.position.y, _nextStack.transform.position.z);
        fallStack.transform.localScale = new Vector3(Mathf.Abs(leftBound - rightBound), _nextStack.transform.localScale.y, _nextStack.transform.localScale.z);
        fallStack.transform.DOMoveY(-10, 1.5f).OnComplete(() => {
            MainManager.Instance.StackManager.StackGarbageCollector.DestroyImmediate(fallStack);
        });
    }
    public void CreateFirstStack()
    {
        _currentStack = _stackFactory.GetStackObject(_colorIndex);
        _currentStack.transform.position = new Vector3(0, -0.5f, _zPosition);
        _colorIndex++;
        _referenceStack = _currentStack;
    }
    public void MoveNewStack()
    {
        _nextStack = _stackFactory.GetStackObject(_colorIndex);
        _nextStack.transform.localScale = _currentStack.transform.localScale;
        _zPosition += _nextStack.transform.localScale.z;
        _nextStack.transform.position = new Vector3(4, -0.5f, _zPosition);
        _tween = _nextStack.transform.DOMoveX(-4, 2.25f).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Failed
            _nextStack.gameObject.SetActive(false);
            MainManager.Instance.GameManager.LevelFailed();
        });
        MainManager.Instance.StackManager.StackGarbageCollector.ThrowToGarbage(_nextStack);
        _colorIndex++;
    }
    public bool CalculateMerge()
    {
        float leftX = _currentStack.transform.position.x - _currentStack.StackBoxCollider.bounds.extents.x;
        float rightX = _currentStack.transform.position.x + _currentStack.StackBoxCollider.bounds.extents.x;
        float nextLeftX = _nextStack.transform.position.x - _nextStack.StackBoxCollider.bounds.extents.x;
        float nextRightX = _nextStack.transform.position.x + _nextStack.StackBoxCollider.bounds.extents.x;
        float solidPieceLeft;
        float solidPieceRight;

        _tween.Pause();

        if (nextLeftX < rightX && nextRightX > leftX)
        {
            if (Mathf.Abs(nextLeftX - leftX) < Tolerance)// check perfect merge
            {
                print("perfect merge");
                solidPieceLeft = leftX;
                solidPieceRight = rightX;
                //perfect anim
                MainManager.Instance.SoundManager.PlayNoteSound(0);
            }
            else
            {
                MainManager.Instance.SoundManager.RefreshPitch();
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
            }
            
            print("Valid cut");

            _nextStack.transform.position = new Vector3((solidPieceLeft + solidPieceRight) / 2, _nextStack.transform.position.y, _nextStack.transform.position.z);
            _nextStack.transform.localScale = new Vector3(Mathf.Abs(solidPieceRight - solidPieceLeft), _nextStack.transform.localScale.y, _nextStack.transform.localScale.z);
            _currentStack = _nextStack;
            return true;
        }
        else // Failed to merge
        {
            CreateFallStack(nextLeftX, nextRightX);
            Failed();
            return false;
        }

    }
}
