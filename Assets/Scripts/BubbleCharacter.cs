using DG.Tweening;
using UnityEngine;

public class BubbleCharacter : MonoBehaviour
{
    [SerializeField] GameObject head;
    [SerializeField] Sprite nextHeadSprite;
    SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = head.GetComponent<SpriteRenderer>();
    }

    public void animGoUp()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(head.transform.DOScale(2, 2).SetEase(Ease.OutBack).OnComplete(() =>
        {
            spriteRenderer.sprite = nextHeadSprite;
        }));

        mySequence.Append(gameObject.transform.DOMoveY(7.5f, 2).SetEase(Ease.OutFlash));

        mySequence.Play();

        // mySequence
    }
}
