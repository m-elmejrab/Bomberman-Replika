public class RestartButton : UIButton
{
    protected override void ClickEventHandler()
    {
        base.ClickEventHandler();
        GameManager.instance.RestartGame();
    }
}
