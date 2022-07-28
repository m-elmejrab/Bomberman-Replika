using UnityEngine;
using TMPro;

public class WinMessage : MonoBehaviour
{
    [SerializeField] TMP_Text winText;
    bool initialized = false;

    private void OnEnable()
    {
        if (initialized)
            winText.text = "Congratulations, you won with a score of " + GameManager.instance.GetScore();
    }

    private void OnDisable()
    {
        initialized = true; //this is done to avoid null reference when game starts/restarts
    }
}
