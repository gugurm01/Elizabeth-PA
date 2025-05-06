using StarterAssets;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public FirstPersonController playerMove;

    private void Awake()
    {
        Instance = this;
    }
}
