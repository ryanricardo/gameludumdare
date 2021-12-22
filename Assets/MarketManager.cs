using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private GameObject      currentBuySkin;
    [SerializeField]    private Button          buttonBuy;
    [SerializeField]    private Data            dataPrefab;
    [HideInInspector]   private Data            dataLocal;
    
    [Header("Atributtes")]
    [SerializeField]   private int             index;    

    void Start()
    {
        index = 1;
        dataLocal = FindObjectOfType<Data>();
        currentBuySkin.GetComponent<Image>().sprite = dataLocal.allSkinsForBuy[index];

        for(int i = 1; i < dataLocal.allSkinsForBuy.Count; i++)
        {
            if(dataPrefab.skinRock1[i] == currentBuySkin.GetComponent<Image>().sprite ||
            dataPrefab.skinRock2[i] == currentBuySkin.GetComponent<Image>().sprite ||
            dataPrefab.skinRock3[i] == currentBuySkin.GetComponent<Image>().sprite )
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

    public void ArrowLeft()
    {
        if(index > 1)
        {
            currentBuySkin.GetComponent<Image>().sprite = dataLocal.allSkinsForBuy[index = index - 1];
        }

        for(int i = 1; i < dataLocal.allSkinsForBuy.Count; i++)
        {
            if(dataPrefab.skinRock1[i] == currentBuySkin.GetComponent<Image>().sprite ||
            dataPrefab.skinRock2[i] == currentBuySkin.GetComponent<Image>().sprite ||
            dataPrefab.skinRock3[i] == currentBuySkin.GetComponent<Image>().sprite )
            {
                buttonBuy.interactable = false;
            }else 
            {
                buttonBuy.interactable = true;
            }
        }
    }

    public void ArrowRight()
    {
        if(index < dataLocal.allSkinsForBuy.Count)
        {
            currentBuySkin.GetComponent<Image>().sprite = dataLocal.allSkinsForBuy[index = index + 1];
        }

        for(int i = 1; i < dataLocal.allSkinsForBuy.Count; i++)
        {
            if(dataPrefab.skinRock1[i] == currentBuySkin.GetComponent<Image>().sprite ||
            dataPrefab.skinRock2[i] == currentBuySkin.GetComponent<Image>().sprite ||
            dataPrefab.skinRock3[i] == currentBuySkin.GetComponent<Image>().sprite )
            {
                buttonBuy.interactable = false;
            }else 
            {
                buttonBuy.interactable = true;
            }
        }
    }


}
