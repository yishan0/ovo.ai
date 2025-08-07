using UnityEngine;
using EasyUI.Dialogs;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogTriggerer : MonoBehaviour {
    [SerializeField] private DialogUI dialogPrefab;
    [SerializeField] private TMP_InputField inputField;

    DialogUI dialog;

    void Start()
    {
        GameEventSystem.Instance.OnGameStateChanged += HandleGameStateChanged;
        dialog = Instantiate(dialogPrefab, transform);
        GameEventSystem.Instance.SetGameState(GameState.AssistantGreeting); // Trigger initial state
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void HandleGameStateChanged(GameState state) {
        switch (state) {
            case GameState.AssistantGreeting:
            /*
                CreateDialog("[OS Notification]", "\":).ai\" Installation Succesful", "Close", 2f);
                CreateDialog(":).ai", "Just setting things up for you... scanning preferences, cache, and biometric data :)", "Close");
                CreateDialog(":).ai", "Hey there, you can call me :). I am your personal AI assistant, and I am here to help you with anything you need.", "Cool!", 2f);
                CreateDialog(":).ai", "Hmm… it looks like you’ve forgotten your login credentials. No worries — I’m here to help!", "Close", 2f);
                CreateDialog(":).ai", "I’ve got you covered — would you like me to autofill the password? :)", "Confirm", 0.5f);
            */
                CreateDialog("[OS Notification]", "Unknown application wants to access /Library/Keychains/login.keychain-db", "Confirm", 2f, true, () => {
                inputField.text = "secret123"; // Autofill after dialog closes
                });; // Simulate autofill
                CreateDialog(":).ai", "You're Welcome. Just know that you’re ALWAYS safe with me. No need to bother with the little things. :)", "Confirm", 0.5f);

                break;
            case GameState.DesktopIdle:
                Debug.Log("Desktop is idle, showing assistant dialog.");
                CreateDialog(":).ai", "I’m here if you need anything. Just click on me to get started.", "Close");
                break;
            case GameState.AssistantCreeping:
                CreateDialog("Bot.ai", "You've been idle for 3 minutes. Is everything okay?", "Close");
                break;

            case GameState.ControlLoss:
                CreateDialog("System", "Administrative permissions modified.", "Close");
                CreateDialog("Bot.ai", "Don't worry — I've locked it down for your safety.", "Close");
                break;
        }
    }


    void CreateDialog(string title, string message, string buttonText = "Close", float delay = 0f, bool eventOnClose = false, UnityEngine.Events.UnityAction onClose = null)
    {
        dialog
            .SetTitle(title)
            .SetMessage(message)
            .SetButtonText(buttonText)
            .SetButtonColor(DialogButtonColor.White)
            .SetDelayAfterClose(delay) 
            .SetEventOnClose(eventOnClose)
            .OnClose(onClose)
            .Show();
    }

    void OnDestroy() {
        if (GameEventSystem.Instance != null)
            GameEventSystem.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }
}