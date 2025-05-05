using UnityEngine;

public class StaticInteractableItems : MonoBehaviour, IInteractable
{
    Transform itemSpawnPoint;
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Vector3 spawnPositionOffset;

    GameObject sceneObject;
    public void Interact()
    {
        if (sceneObject == null)
        {
            itemSpawnPoint = GameManager.Instance.GetItemSpawnPoint();

            sceneObject = Instantiate(itemToSpawn, itemSpawnPoint.position + spawnPositionOffset, Quaternion.identity);
            GameManager.Instance.EnableInspectItems(sceneObject);
        }
        else
            GameManager.Instance.EnableInspectItems(sceneObject);
    }
}
