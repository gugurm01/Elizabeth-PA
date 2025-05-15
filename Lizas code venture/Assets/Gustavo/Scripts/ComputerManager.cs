using Cinemachine;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager Instance { get; private set; }

    public bool IsUsingComputer { get; private set; }
    private Computer currentComputer;
    private CinemachineVirtualCamera currentComputerCamera;
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (playerCamera == null)
        {
            Debug.LogWarning("PlayerCamera não encontrada!");
        }
    }

    public void EnterComputer(Computer computer, CinemachineVirtualCamera computerCam)
    {
        if (playerCamera == null || computerCam == null) return;

        IsUsingComputer = true;
        currentComputer = computer;
        currentComputerCamera = computerCam;

        playerCamera.Priority = 10;
        currentComputerCamera.Priority = 20;

        DisablePlayerControls();
    }

    public void ExitComputer()
    {
        if (playerCamera == null || currentComputerCamera == null) return;

        IsUsingComputer = false;

        playerCamera.Priority = 20;
        currentComputerCamera.Priority = 10;

        if (currentComputer != null)
        {
            currentComputer.ExitComputerExternally();
            currentComputer = null;
            currentComputerCamera = null;
        }

        EnablePlayerControls();
    }

    public void DisablePlayerControls()
    {
        PlayerManager.Instance.PlayerControls(false);
    }

    public void EnablePlayerControls()
    {
        PlayerManager.Instance.PlayerControls(true);
    }
}
