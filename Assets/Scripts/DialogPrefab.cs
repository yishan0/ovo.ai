using UnityEngine;
using TMPro;

public class DialogPrefab : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    void Awake()
    {
        // Automatically find the TMP_InputField if not assigned in Inspector
        if (inputField == null)
        {
            inputField = GetComponentInChildren<TMP_InputField>();
        }
    }
}