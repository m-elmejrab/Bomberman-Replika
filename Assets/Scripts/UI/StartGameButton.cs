public class StartGameButton : UIButton
{
    protected override void ClickEventHandler()
    {
        base.ClickEventHandler();
        GameManager.instance.StartGame();
        UIManager.instance.StartGame();
    }
}
