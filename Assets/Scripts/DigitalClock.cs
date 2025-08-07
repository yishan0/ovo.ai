using UnityEngine;
using TMPro;
public class DigitalClock : MonoBehaviour
{
    public TextMeshProUGUI clockText;

    void Update()
    {
        System.DateTime now = System.DateTime.Now;
        string time = now.ToString("HH:mm:ss");                    // 24-hour format
        string date = now.ToString("ddd, MMM d");               // e.g., Tue, Aug 6
        clockText.text = $"{time}   {date}";
    }
}
