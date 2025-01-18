using DG.Tweening;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    [SerializeField] float timeDelayInterval = 0.2f;
    [SerializeField] GameObject showObject;

    bool isClick = false;

    void OnMouseUp()
    {
        Sequence sequence = DOTween.Sequence();

        // ChatBubble.Create(transform, new Vector3(0, 1), "Click and detroy this");

        if (!isClick)
        {
            isClick = true;

            sequence.AppendInterval(timeDelayInterval);
            sequence.Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));
            if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                sequence.Join(spriteRenderer.DOFade(0, 0.5f));
            }
            sequence.OnComplete(() =>
            {
                Destroy(gameObject);
            });

            EventDispatcher.Dispatch(new EventDefine.OnIncreasePoint());

            AudioManager.Instance.PlaySoundWithRandomPitch(GameAudioClip.POP);

            showObject?.SetActive(true);
        }

    }
}
