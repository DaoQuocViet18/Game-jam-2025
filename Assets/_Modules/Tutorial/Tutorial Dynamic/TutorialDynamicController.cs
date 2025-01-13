using UnityEngine;

public class TutorialDynamicController : Singleton<TutorialDynamicController>
{
    [SerializeField] private GameObject[] objects;

    private void Reset()
    {
        LoadObjects();
    }

    private void Awake()
    {
        LoadObjects();
    }

    private void LoadObjects()
    {
        // Tìm tất cả các BoxCollider2D trong scene
        BoxCollider2D[] colliders = Object.FindObjectsByType<BoxCollider2D>(FindObjectsSortMode.None);

        // Danh sách tạm để lưu các GameObject
        var filteredObjects = new System.Collections.Generic.List<GameObject>();

        foreach (BoxCollider2D collider in colliders)
        {
            GameObject obj = collider.gameObject;

            // Kiểm tra nếu obj không phải là con của GameObject hiện tại
            if (!IsChildOfCurrentObject(obj))
            {
                filteredObjects.Add(obj);
            }
        }

        // Gán danh sách vào mảng objects
        objects = filteredObjects.ToArray();

        // Log kết quả
        Debug.Log($"Tìm thấy {objects.Length} GameObjects có BoxCollider2D (không phải là child của GameObject hiện tại).");
    }
    private bool IsChildOfCurrentObject(GameObject obj)
    {
        Transform currentTransform = obj.transform;

        while (currentTransform != null)
        {
            if (currentTransform == this.transform)
            {
                return true; // GameObject là con hoặc chính GameObject hiện tại
            }
            currentTransform = currentTransform.parent;
        }

        return false; // Không phải là con
    }

    public void DisableObjects()
    {
        foreach (GameObject obj in objects)
        {
            if (obj.TryGetComponent<BoxCollider2D>(out BoxCollider2D collider))
            {
                collider.enabled = false; // Vô hiệu hóa BoxCollider2D
            }
        }
        EventDispatcher.Dispatch(new EventDefine.OnTutorialGame { isTutorial = true });
    }

    public void EnableObjects()
    {
        foreach (GameObject obj in objects)
        {
            if (obj.TryGetComponent<BoxCollider2D>(out BoxCollider2D collider))
            {
                collider.enabled = true; // Bật lại BoxCollider2D
            }
        }
        EventDispatcher.Dispatch(new EventDefine.OnTutorialGame { isTutorial = false });
    }
}
