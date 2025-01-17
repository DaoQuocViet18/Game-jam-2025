using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TutorialDynamic : MonoBehaviour
{
    [SerializeField] private GameObject drag;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject mouseCursor;
    [SerializeField] private GameObject tutorialDynamicPanel;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float display = -3f;

    private void Start()
    {
        TutorialDynamicController.Instance.DisableObjects();
        mouseCursor.transform.position = new Vector3(drag.transform.position.x, drag.transform.position.y, display);
        StartCursorMovement();
        Hint.Instance.ActiveHint(target);
    }

    private async void StartCursorMovement()
    {
        while (drag != null && drag.activeInHierarchy)
        {
            mouseCursor.GetComponent<SpriteRenderer>().sortingOrder = drag.GetComponent<SpriteRenderer>().sortingOrder + 1;

            var tween = mouseCursor.transform.DOMove(target.transform.position, duration)
                .SetEase(Ease.InOutQuad);

            await tween.AsyncWaitForCompletion();

            if (drag == null || !drag.activeInHierarchy)
            {
                break; 
            }    

            mouseCursor.transform.position = new Vector3(drag.transform.position.x, drag.transform.position.y, display);
        }

        StartCoroutine(EndTutorial(1f));
    }

    private IEnumerator EndTutorial(float delay)
    {
        yield return new WaitForSeconds(delay);
        TutorialDynamicController.Instance.EnableObjects();

        if (tutorialDynamicPanel != null && tutorialDynamicPanel.activeInHierarchy)
            tutorialDynamicPanel.SetActive(false);
    }
}
