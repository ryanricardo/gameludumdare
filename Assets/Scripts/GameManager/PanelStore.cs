using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStore : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]    private Button[]        arrowsRocks; 
    [SerializeField]    private Button          buttonBuy; 
    [SerializeField]    private GameObject[]    gameObjectsRocks;
    [SerializeField]    private Data            data;

    [Header("Atributtes Arrows")]
    [SerializeField]    private int[]           indexSkin;
    

    void Start()
    {
        arrowsRocks[0].onClick.AddListener(delegate{Arrows(0);});
        arrowsRocks[1].onClick.AddListener(delegate{Arrows(1);});

        for(int i = 1; i < data.skinRock1.Count; i++)
        {
            if(data.skinsRewardRock1[indexSkin[0]] == data.skinRock1[i])
            {
                buttonBuy.interactable = false;
            }else 
            {
                buttonBuy.interactable = true;
            }
        }
    }

    void Update()
    {

    }

    void Arrows(int index)
    {
        switch(index)
        {
            case 0:
                if(indexSkin[0] > 0)
                { 
                    indexSkin[0] -= 1;
                    gameObjectsRocks[0].GetComponent<Image>().sprite = data.skinsRewardRock1[indexSkin[0]];

                    for(int i = 1; i < data.skinRock1.Count; i++)
                    {
                        if(data.skinsRewardRock1[indexSkin[0]] == data.skinRock1[i])
                        {
                            buttonBuy.interactable = false;
                        }else 
                        {
                            buttonBuy.interactable = true;
                        }
                    }
                }
            break;

            case 1:
                if(indexSkin[0] < data.skinsRewardRock1.Length - 1)
                { 
                    indexSkin[0] += 1;
                    gameObjectsRocks[0].GetComponent<Image>().sprite = data.skinsRewardRock1[indexSkin[0]];

                    for(int i = 1; i < data.skinRock1.Count; i++)
                    {
                        if(data.skinsRewardRock1[indexSkin[0]] == data.skinRock1[i])
                        {
                            buttonBuy.interactable = false;
                        }else 
                        {
                            buttonBuy.interactable = true;
                        }
                    }
                }
            break;
        }
    }

    void BuyNewSkin()
    {

    }
}
