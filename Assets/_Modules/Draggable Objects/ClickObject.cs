using DG.Tweening;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    [SerializeField] float timeDelayInterval = 2.0f;

    void OnMouseUp()
    {
        Debug.Log("UPPPPP");

        Sequence sequence = DOTween.Sequence();

        ChatBubble.Create(transform, new Vector3(0, 1), "Click and detroy this");

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

        AudioManager.Instance.PlaySoundWithRandomPitch(GameAudioClip.POP);
    }
}
