using UnityEngine;
using UnityEngine.UI;
public class UIButton : MonoBehaviour //Base class for UI buttons, all buttons inherit and override click event handler
{
    Button thisButton;
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(ClickEventHandler);
    }

    protected virtual void ClickEventHandler()
    {
        SoundManager.instance.PlayClickSound();
    }
}
