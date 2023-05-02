using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float tileSize; // сделать константу для всех
    public void TryInteract(Quaternion playerDirection)
    {
        Debug.Log("looking for interactable");
        RaycastHit hit;
        // raycast in 4 sides, if find interactable
        if (Physics.Raycast(transform.position, playerDirection * Vector3.forward, out hit, tileSize, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            var interactable = hit.collider.gameObject.GetComponent<Interactable>();
            if (interactable != null)
            {
                Debug.Log("found interactable");
                interactable.Interact(transform.position);
            }
        }
    }
}
