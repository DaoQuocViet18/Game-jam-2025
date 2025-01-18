using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] GameObject btnGameObject;

    public void showhidebtn()
    {
        btnGameObject.SetActive(!btnGameObject.activeSelf);
        //float timer = TimerManager.Instance.GetCurrentTime();
        //TimerManager.Instance.SetCurrentTime(timer - 10);
        //Debug.Log("timer: " + timer);
    }

    private void Start()
    {
        setText();
    }

    void setText()
    {
        textMeshPro.text = GameManager.Instance.getCurrentHintText();
    }
}
