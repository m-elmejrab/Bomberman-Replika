using UnityEngine;

public class QuitButton : UIButton
{
    protected override void ClickEventHandler()
    {
        base.ClickEventHandler();
        Application.Quit();
        Debug.Log("Application Exit");
    }
}
