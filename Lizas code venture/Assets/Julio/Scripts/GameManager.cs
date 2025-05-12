using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Related")]
    [SerializeField] Camera playerCam;
    [SerializeField] GameObject newUi;

    [Header("Inspect items Variables")]
    [SerializeField] Camera itemOverlayCam;
    [SerializeField] Transform ItemsSpawnPoint;
    GameObject _activeItem;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void EnableInspectItems(GameObject itemToActivate) 
    {
        _activeItem = itemToActivate;
        _activeItem.SetActive(true);

        ComputerManager.Instance.DisablePlayerControls();
        playerCam.enabled = false;

        newUi.SetActive(true);
        itemOverlayCam.enabled = true;
    }

    public void DisableInspectItems() 
    {
        if (_activeItem == null)
        {
            Debug.LogWarning("No Item to deactivate!");
            return;
        }

        _activeItem.SetActive(false);
        _activeItem = null;

        ComputerManager.Instance.EnablePlayerControls();
        playerCam.enabled = true;

        newUi.SetActive(false);
        itemOverlayCam.enabled = false;

    }
    public Transform GetItemSpawnPoint() 
    {
        return ItemsSpawnPoint;
    }

    public Camera GetItemCamera()
    {
        return itemOverlayCam;
    }
}
