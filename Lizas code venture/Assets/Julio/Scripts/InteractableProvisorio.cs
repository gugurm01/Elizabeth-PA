using UnityEngine;

public class InteractableProvisorio : MonoBehaviour, IInteractable
{
    public GameObject panelToActivate;
    public void Interact()
    {
        panelToActivate.SetActive(true);
        ComputerManager.Instance.DisablePlayerControls();
    }
}
