using UnityEngine;
using EasyUI.Dialogs;

public class NotificationMachine : MonoBehaviour
{
    [SerializeField] private DialogUI dialogPrefab;  // Assign your DialogUI prefab here
    DialogUI dialogInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogUI dialogInstance = Instantiate(dialogPrefab, transform);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void newNotification(string title, string message, string closeText)
    {
        dialogInstance
            .SetTitle(title)
            .SetMessage(message)
            .SetButtonColor(DialogButtonColor.White)
            .SetButtonText(closeText)
            .OnClose(() => Debug.Log("Closed Notification"))
            .Show();
    }
}
