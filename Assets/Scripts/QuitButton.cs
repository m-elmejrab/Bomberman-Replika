using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : UIButton
{
    protected override void ClickEventHandler()
    {
        Application.Quit();
        Debug.Log("Application Exit");
    }
}
