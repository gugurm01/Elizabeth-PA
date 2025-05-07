using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private Transform rayOrigin;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ComputerManager.Instance != null && ComputerManager.Instance.IsUsingComputer)
            {
                ComputerManager.Instance.ExitComputer();
            }
            else
            {
                TryInteract();
            }
        }
    }

    private void TryInteract()
    {
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, interactRange))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null && !Physics.Raycast(rayOrigin.position, rayOrigin.forward, hit.distance, obstructionMask))
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (rayOrigin == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward * interactRange);
    }
}
