using StarterAssets;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public FirstPersonController playerMove;

    private void Awake()
    {
        Instance = this;
        LockCursor(true);
    }

    public void LockCursor(bool state) 
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void PlayerControls(bool state) 
    {
        Instance.playerMove.enabled = state;
        Instance.LockCursor(state);
        PlayerInteractions.Instance.CanInteract(state);
    }
}
