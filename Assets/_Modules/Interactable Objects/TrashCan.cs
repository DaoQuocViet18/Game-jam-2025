using UnityEngine;

[RequireComponent(typeof(ActiveObject))]
public class TrashCan : MonoBehaviour, IInteractableObject
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
        ActiveObject.Active(targetObject);
        Destroy(targetObject);
        ChatBubble.Create(transform, new Vector3(0, 1), "Bye bye object");
        AudioManager.Instance.PlaySoundWithRandomPitch(GameAudioClip.POP);
        gameObject.SetActive(false);
    }
}
