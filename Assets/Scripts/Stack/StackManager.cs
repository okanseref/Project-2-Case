using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    StackFactory _stackFactory;
    int colorIndex = 0;
    float zPosition = 0;
    Tween tween;
    StackObject currentStack;
    StackObject nextStack;
    public void Construct(StackFactory stackFactory)
    {
        _stackFactory = stackFactory;
    }
    private void Start()
    {
        CreateFirstStack();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveNewStack();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CalculateMerge();
        }
    }
    public void CreateFirstStack()
    {
        currentStack = _stackFactory.GetStackObject(colorIndex);
        currentStack.transform.position = new Vector3(0, -0.5f, zPosition);
        colorIndex++;
    }
    public void MoveNewStack()
    {
        nextStack = _stackFactory.GetStackObject(colorIndex);
        zPosition += nextStack.transform.localScale.z;
        nextStack.transform.position = new Vector3(4, -0.5f, zPosition);
        tween = nextStack.transform.DOMoveX(-4, 2).SetEase(Ease.Linear);
        colorIndex++;
    }
    public void CalculateMerge()
    {
        float leftX = currentStack.transform.position.x - currentStack.StackBoxCollider.bounds.extents.x;
        float rightX = currentStack.transform.position.x + currentStack.StackBoxCollider.bounds.extents.x;
        float nextLeftX = nextStack.transform.position.x - nextStack.StackBoxCollider.bounds.extents.x;
        float nextRightX = nextStack.transform.position.x + nextStack.StackBoxCollider.bounds.extents.x;
        float fallPieceLeft;
        float fallPieceRight;
        float solidPieceLeft;
        float solidPieceRight;
        if (nextLeftX < rightX && nextRightX > leftX)
        {
            print("Valid cut");
            if (nextRightX > rightX)
            {
                fallPieceLeft = rightX;
                fallPieceRight = nextRightX;
                solidPieceLeft = nextLeftX;
                solidPieceRight = rightX;
            }
            else
            {
                fallPieceLeft = nextLeftX;
                fallPieceRight = leftX;
                solidPieceLeft = leftX;
                solidPieceRight = nextRightX;
            }
            tween.Pause();
            nextStack.transform.position = new Vector3((solidPieceLeft + solidPieceRight) / 2, nextStack.transform.position.y, nextStack.transform.position.z);
            nextStack.transform.localScale = new Vector3(solidPieceRight - solidPieceLeft, nextStack.transform.localScale.y, nextStack.transform.localScale.z);
            currentStack = nextStack;
        }

    }
}
