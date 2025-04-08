using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Camera itemOverlayCam;
    [SerializeField] Transform ItemsSpawnPoint;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    public void OverlayCamState(bool isActive) 
    {
        itemOverlayCam.enabled = isActive;
    }

    public Transform GetItemSpawnPoint() 
    {
        return ItemsSpawnPoint;
    }
}
