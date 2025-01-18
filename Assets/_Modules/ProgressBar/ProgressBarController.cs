using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] Image progress;
    [SerializeField] float progressScale = 0;

    private void OnEnable()
    {
        // Đăng ký sự kiện OnLoseGame
        EventDispatcher.Add<EventDefine.OnUpdateProgressBar>(OnUpdateProgressBar);
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị hủy
        EventDispatcher.Remove<EventDefine.OnUpdateProgressBar>(OnUpdateProgressBar);
    }

    void setProgress(float amount)
    {
        DOVirtual.Float(progress.fillAmount, amount, 0.2f, value =>
        {
            progress.fillAmount = value;
        });
    }

    void OnUpdateProgressBar(IEventParam param)
    {
        setProgress(GameManager.Instance.getCurrenProgress());
    }
}