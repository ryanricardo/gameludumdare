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
    [SerializeField]    private     Data                    data;
    [HideInInspector]   private     NewPlayerController     playerController;


    [Header("Atributtes Pickup SubmitBalancePlayer")]
    [SerializeField]    private     float                   valueSubmitBalancePickup;  
 

    [Header("Atributtes Pickup MoreJump")]
    [SerializeField]    private     float                   valueMoreJumpPickup;   

    [Header("Atributtes Reward Skin")]
    [SerializeField]    private     bool                    giveReward;
    [SerializeField]    private     int                     indexRock;
    [SerializeField]    private     int                     indexSkinReward;   

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

            RewardSkin();
            Destroy(gameObject, 0);
        }
    }

    void RewardSkin()
    {
        if(giveReward)
        {
            switch(indexRock)
            {
                case 1:
                    data.skinRock1.Add(data.skinsRewardRock1[indexSkinReward]);
                break;

                case 2:
                    data.skinRock2.Add(data.skinsRewardRock2[indexSkinReward]);
                break;

                case 3:
                    data.skinRock3.Add(data.skinsRewardRock3[indexSkinReward]);
                break;
            }
        }
    }
}

