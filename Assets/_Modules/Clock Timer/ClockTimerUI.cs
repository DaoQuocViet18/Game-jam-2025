using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimerUI : MonoBehaviour
{
    [SerializeField] Image progress;


    void Start()
    {
        setClock(0);
    }
    void setClock(float amount)
    {
        DOVirtual.Float(progress.fillAmount, amount, 90f, value =>
        {
            progress.fillAmount = value;
        });
    }
}
