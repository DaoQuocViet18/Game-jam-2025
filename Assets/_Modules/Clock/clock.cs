using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
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
