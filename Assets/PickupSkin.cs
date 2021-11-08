using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSkin : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private     Data                    data;

    [Header("Atributtes Reward Skin")]
    [SerializeField]    private     int                     indexRock;
    [SerializeField]    private     int                     indexSkinReward;   

    void Start()
    {
    
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("RockController 1")
        || (other.gameObject.CompareTag("RockController 2")))
        {
            switch(indexRock)
            {
                case 1:
                    for(int i = 1; i < data.skinRock1.Count; i++)
                    {
                        if(data.skinsRewardRock1[indexSkinReward] == data.skinRock1[i])
                        {
                            return; 
                        }
                    }
                    data.skinRock1.Add(data.skinsRewardRock1[indexSkinReward]);
                break;

                case 2:
                    for(int i = 1; i < data.skinRock2.Count; i++)
                    {
                        if(data.skinsRewardRock2[indexSkinReward] == data.skinRock2[i])
                        {
                            return; 
                        }
                    }
                    data.skinRock2.Add(data.skinsRewardRock2[indexSkinReward]);
                break;

                case 3:
                    for(int i = 1; i < data.skinRock3.Count; i++)
                    {
                        if(data.skinsRewardRock3[indexSkinReward] == data.skinRock3[i])
                        {
                            return; 
                        }
                    }
                    data.skinRock3.Add(data.skinsRewardRock3[indexSkinReward]);
                break;
            }
                
            Destroy(gameObject, 0);
        }

    }
}

