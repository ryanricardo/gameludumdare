using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum TypePowerUps
    {
        SubmitBalancePlayer,
        MoreJump,

    }

    [Header("Components")]
    [SerializeField]    private     TypePowerUps            typePowerUps;
    [HideInInspector]   private     NewPlayerController     playerController;

    [Header("Atributtes Pickup SubmitBalancePlayer")]
    [SerializeField]    private     float                   valueSubmitBalancePickup;      

    [Header("Atributtes Pickup MoreJump")]
    [SerializeField]    private     float                   valueMoreJumpPickup;   

    void Start()
    {
        playerController = FindObjectOfType<NewPlayerController>();
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            switch(typePowerUps)
            {
                case TypePowerUps.SubmitBalancePlayer:
                    playerController.speedSubmitBalance /= valueSubmitBalancePickup;
                break;

                case TypePowerUps.MoreJump:
                    playerController.forceJump += valueMoreJumpPickup;
                break;
            }
            Destroy(gameObject, 0);
        }
    }
}