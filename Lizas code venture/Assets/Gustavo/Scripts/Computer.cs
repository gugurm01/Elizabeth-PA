using Cinemachine;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    private bool isInUse = false;

    [SerializeField] private CinemachineVirtualCamera computerCamera;

    public void Interact()
    {
        if (isInUse)
            ExitComputer();
        else
            EnterComputer();
    }

    private void EnterComputer()
    {
        isInUse = true;
        Debug.Log("Entrou no computador.");
        ComputerManager.Instance.EnterComputer(this, computerCamera);
    }

    private void ExitComputer()
    {
        isInUse = false;
        Debug.Log("Saiu do computador.");
        ComputerManager.Instance.ExitComputer();
    }

    public void ExitComputerExternally()
    {
        isInUse = false;
        Debug.Log("Saiu do computador (externo).");
    }
}
