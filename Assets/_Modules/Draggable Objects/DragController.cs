using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DragController : MonoBehaviour
{
    private const float DRAG_SPEED = 40f;
    private static int currentMaxSortingOrder = 30;

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        currentMaxSortingOrder++;
        spriteRenderer.sortingOrder = currentMaxSortingOrder;

        AudioManager.Instance.PlaySoundWithRandomPitch(GameAudioClip.POP_SOUND_EFFECT);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = Vector3.Lerp(transform.position, curPosition, Time.deltaTime * DRAG_SPEED);
    }

    void OnMouseUp()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        });
    }
}