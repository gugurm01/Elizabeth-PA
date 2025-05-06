using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager Instance { get; private set; }

    public bool IsUsingComputer { get; private set; }
    private Computer currentComputer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EnterComputer(Computer computer)
    {
        IsUsingComputer = true;
        currentComputer = computer;
        DisablePlayerControls();
    }

    public void ExitComputer()
    {
        IsUsingComputer = false;
        if (currentComputer != null)
        {
            currentComputer.ExitComputerExternally();
            currentComputer = null;
        }
        EnablePlayerControls();
    }

    private void DisablePlayerControls()
    {
        PlayerManager.Instance.playerMove.enabled = false;
    }

    private void EnablePlayerControls()
    {
        PlayerManager.Instance.playerMove.enabled = true;
    }
}
