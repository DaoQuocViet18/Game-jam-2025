using System.Threading.Tasks;
using UnityEngine;

public class Interactor : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out IInteractableObject interactableObject)) {
            interactableObject.Interact(gameObject);
        }
    }
}
