using System.Threading.Tasks;
using UnityEngine;

public class Interactor : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out IInteractableObject interactableObject)) {
            DustAndShrinkEffectController.Instance.StretchAndShrinkAnimation(gameObject, 0.5f, 0.5f, new Vector3(1f, 2f, 1f));
            interactableObject.Interact(gameObject);
        }
    }
}
