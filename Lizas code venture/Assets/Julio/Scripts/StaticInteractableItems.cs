using UnityEngine;

public class StaticInteractableItems : MonoBehaviour, IInteractable
{
    Transform itemSpawnPoint;
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Vector3 spawnOffset;
    public void Interact()
    {
        itemSpawnPoint = GameManager.Instance.GetItemSpawnPoint();

        Instantiate(itemToSpawn, itemSpawnPoint.position + spawnOffset,Quaternion.identity);
    }
}
