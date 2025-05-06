using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    private bool isInUse = false;

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
        ComputerManager.Instance.EnterComputer(this);
    }

    private void ExitComputer()
    {
        isInUse = false;
        Debug.Log("Saiu do computador.");
        ComputerManager.Instance.ExitComputer();
    }

    // Chamado pelo ComputerManager para forçar saída do PC
    public void ExitComputerExternally()
    {
        isInUse = false;
        Debug.Log("Saiu do computador (externo).");
    }
}
