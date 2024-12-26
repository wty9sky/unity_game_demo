using UnityEngine;
using Cinemachine;

public class AutoSetupCamera : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        CinemachineVirtualCamera vcam = GetComponent<CinemachineVirtualCamera>();

        player = FindObjectOfType<Player>();

        if (player != null)
        {
            vcam.Follow = player.transform;
        }
    }
}
