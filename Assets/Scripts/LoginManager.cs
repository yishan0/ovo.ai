using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private string correctPassword = "secret123";

    [SerializeField] private GameObject fadeOut;

    private GameObject fadeOutObject;

    public void TestClick()
    {
        Debug.Log("TestClick worked");
    }
    public void CheckPasswordA()
    {
        Debug.Log("CheckPassword called"); // This MUST appear
        string input = passwordInputField.text.Trim();

        if (input == correctPassword)
        {
            Debug.Log("Password correct. Logging in...");
            // Load the next scene
            FadeOutStart();
            SceneManager.LoadScene("Desktop");
            GameEventSystem.Instance.SetGameState(GameState.DesktopIdle); 
        }
        else
        {
            Debug.Log("Incorrect password.");
            StartCoroutine(ShakeInput(passwordInputField.GetComponent<RectTransform>()));
        }
    }

    void Start()
    {
        Debug.Log("LoginManager started");
    }

    public void FadeOutStart()
    {
        fadeOutObject = Instantiate(fadeOut, new Vector3(0, 0, 0), Quaternion.identity);
    }


    private IEnumerator ShakeInput(RectTransform rect)
    {
        Vector3 originalPos = rect.anchoredPosition;
        float duration = 0.3f;
        float strength = 10f;
        float time = 0f;

        while (time < duration)
        {
            float x = Random.Range(-1f, 1f) * strength;
            rect.anchoredPosition = originalPos + new Vector3(x, 0, 0);
            time += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPos;
    }
}