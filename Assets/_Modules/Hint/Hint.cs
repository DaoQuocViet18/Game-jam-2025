using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Hint : Singleton<Hint>
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject mouseCursor;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float endOffset = 1f;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float display = -3f;

    private void Awake()
    {
        hintPanel.SetActive(false);
    }

    //private void Start()
    //{
    //    TutorialDynamicController.Instance.DisableObjects();
    //    mouseCursor.transform.position = new Vector3(start.transform.position.x, start.transform.position.y, display);
    //    AlignObjectsInLine();
    //    StartCursorMovement();
    //}

    public void ActiveHint(GameObject target)
    {
        this.target = target;
        AlignObjectsInLineRandom();
        mouseCursor.transform.position = new Vector3(start.transform.position.x, start.transform.position.y, display);
        hintPanel.SetActive(true);
        StartCursorMovement();
    }

    private void AlignObjectsInLineRandom()
    {
        // Chọn ngẫu nhiên một góc trong khoảng từ 0 đến 360 độ
        float randomAngle = Random.Range(0f, 360f);

        // Chuyển góc ngẫu nhiên thành một vector hướng
        Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0f, Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;

        // Xếp vị trí của end và start quanh target theo hướng ngẫu nhiên
        end.transform.position = target.transform.position + direction * endOffset;
        start.transform.position = target.transform.position + direction * (endOffset + distance);
    }

    private void AlignObjectsInLineBeginFromStart()
    {
        Vector3 direction = (start.transform.position - target.transform.position).normalized;

        end.transform.position = target.transform.position + direction * endOffset;

        start.transform.position = end.transform.position + direction * distance;
    }

    private async void StartCursorMovement()
    {
        Vector3 direction = end.transform.position - start.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        mouseCursor.transform.rotation = Quaternion.Euler(0f, 0f, angle - 130f);

        while (target != null && target.activeInHierarchy)
        {
            mouseCursor.GetComponent<SpriteRenderer>().sortingOrder = start.GetComponent<SpriteRenderer>().sortingOrder + 1;

            await mouseCursor.transform.DOMove(end.transform.position, duration)
                .SetEase(Ease.InOutQuad).AsyncWaitForCompletion();

            await mouseCursor.transform.DOMove(start.transform.position, duration)
                .SetEase(Ease.InOutQuad).AsyncWaitForCompletion();

            if (target == null || !target.activeInHierarchy)
            {
                hintPanel.SetActive(false);
                break;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Destroy(gameObject);
    }
}
