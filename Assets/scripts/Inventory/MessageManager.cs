using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public float displayTime = 3f;

    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        CancelInvoke();
        Invoke(nameof(HideMessage), displayTime);
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
