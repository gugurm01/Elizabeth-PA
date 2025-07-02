using UnityEngine;

public class StaticInteractableItems : MonoBehaviour, IInteractable
{
    Transform itemSpawnPoint;
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Vector3 spawnPositionOffset;
    [SerializeField] ParticleSystem particle;

    GameObject sceneObject;
    public void Interact()
    {
        if (sceneObject == null)
        {
            itemSpawnPoint = GameManager.Instance.GetItemSpawnPoint();

            sceneObject = Instantiate(itemToSpawn, itemSpawnPoint.position + spawnPositionOffset, Quaternion.identity);
            GameManager.Instance.EnableInspectItems(sceneObject);

            if (particle != null) 
            {
                particle.Stop();
            }
        }
        else
            GameManager.Instance.EnableInspectItems(sceneObject);
    }
}
