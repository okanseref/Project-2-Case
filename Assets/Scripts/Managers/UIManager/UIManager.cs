using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject failScreen, successScreen;
    public void ShowScreen(ScreenType screenType)
    {
        failScreen.SetActive(screenType == ScreenType.Fail);
        successScreen.SetActive(screenType == ScreenType.Success);
    }
    public enum ScreenType
    {
        None,
        Success,
        Fail
    }
}
