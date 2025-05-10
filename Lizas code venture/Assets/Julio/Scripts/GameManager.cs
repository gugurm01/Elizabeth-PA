using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        itemOverlayCam.enabled = true;
        _activeItem.SetActive(true);

        ComputerManager.Instance.DisablePlayerControls();
    }

    public void DisableInspectItems() 
    {
        if (_activeItem == null)
        {
            Debug.LogWarning("No Item to deactivate!");
            return;
        }

        _activeItem.SetActive(false);
        itemOverlayCam.enabled = false;
        _activeItem = null;

        ComputerManager.Instance.EnablePlayerControls();

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
