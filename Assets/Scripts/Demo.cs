using UnityEngine;
using EasyUI.Dialogs;

public class Demo : MonoBehaviour
{
    [SerializeField] private DialogUI dialogPrefab;  // Assign your DialogUI prefab here

    void Start()
    {
        // Instantiate the DialogUI prefab
        DialogUI dialogInstance = Instantiate(dialogPrefab, transform);

        // First Dialog -----------------------------
        dialogInstance
            .SetTitle("[OS Notification]]")
            .SetMessage("Bot.ai Installation Succesful")
            .SetButtonColor(DialogButtonColor.White)
            .OnClose(() => Debug.Log("Closed 1"))
            .Show();

        // Second Dialog ----------------------------
        dialogInstance
            .SetTitle("Bot.ai")
            .SetMessage("Just setting things up for you... scanning preferences, cache, and biometric data :)")
            .SetButtonColor(DialogButtonColor.White)
            .SetButtonText("Close")
            .OnClose(() => Debug.Log("Closed 2"))
            .Show();

        // Third Dialog -----------------------------
        dialogInstance
            .SetTitle("Bot.ai")
            .SetMessage("Hey there, you can call Bot. I am your personal assistant, and I am here to help you with anything you need.")
            .SetFadeInDuration(1f)
            .SetButtonColor(DialogButtonColor.White)
            .SetButtonText("Cool!")
            .OnClose(() => Debug.Log("Closed 3"))
            .Show();
    }
}