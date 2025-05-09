using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private PlayerVisual playerVisual;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerController.Init(playerVisual);
    }
}
