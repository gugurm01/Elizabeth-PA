using UnityEngine;

public class InteractableProvisorio : MonoBehaviour, IInteractable
{
    public GameObject panelToActivate;
    public ParticleSystem particle;
    public void Interact()
    {
        panelToActivate.SetActive(true);

        if (particle) 
        {
            particle.Stop();
        }

        ComputerManager.Instance.DisablePlayerControls();
    }
}
