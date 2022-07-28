using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : UIButton
{
    protected override void ClickEventHandler()
    {
        GameManager.instance.RestartGame();
    }
}
