using TMPro;
using UnityEngine;

public class ClockTimerUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update() {
        timerText.text = ((int)TimerManager.Instance.GetCurrentTime()).ToString();
    }
}
