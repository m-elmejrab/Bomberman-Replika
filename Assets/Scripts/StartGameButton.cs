using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : UIButton
{
    protected override void ClickEventHandler()
    {
        GameManager.instance.StartGame();
        UIManager.instance.StartGame();
    }
}
