using UnityEngine;

[RequireComponent(typeof(ActiveObject))]
public class Person : MonoBehaviour, IInteractableObject
{
    [SerializeField] private ActiveObject ActiveObject;

    private void Reset()
    {
        LoadActiveObject();
    }

    private void Awake()
    {
        LoadActiveObject();
    }


    private void LoadActiveObject()
    {
        if (this.ActiveObject != null) return;
        this.ActiveObject = GetComponent<ActiveObject>();
        Debug.Log(transform.name + ": Load ActiveObject", gameObject);
    }

    public void Interact(GameObject targetObject)
    {
        Debug.Log("targetObject: " + targetObject);
        ActiveObject.Active(targetObject, gameObject);
    }
}
