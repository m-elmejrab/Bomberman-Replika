public class ContinueButton : UIButton
{
    protected override void ClickEventHandler()
    {
        base.ClickEventHandler();
        GameManager.instance.ResumeGame();
    }
}
