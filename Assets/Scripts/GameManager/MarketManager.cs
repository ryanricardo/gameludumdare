using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private TextMeshProUGUI textPriceSkins;
    [SerializeField]    private GameObject      currentBuySkin;
    [SerializeField]    private Button          buttonBuy;
    [SerializeField]    private Data            dataPrefab;
    [HideInInspector]   private Data            dataLocal;
    
    [Header("Atributtes")]
    [SerializeField]    private int[]           pricesSkins;
    [SerializeField]    private int             index;    
    [HideInInspector]   private bool            buy;

    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Diamonds1"));

        buy = true;
        index = 1;
        textPriceSkins.text = pricesSkins[index].ToString();
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
        for(int i = 1; i < 4; i++)
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

    public void ArrowLeft()
    {
        buy = true;
        if(index > 1)
        {
            currentBuySkin.GetComponent<Image>().sprite = dataLocal.allSkinsForBuy[index = index - 1];
            textPriceSkins.text = pricesSkins[index].ToString();
        }


    }

    public void ArrowRight()
    {
        buy = true;
        if(index < dataLocal.allSkinsForBuy.Count)
        {
            currentBuySkin.GetComponent<Image>().sprite = dataLocal.allSkinsForBuy[index = index + 1];
            textPriceSkins.text = pricesSkins[index].ToString();
        }

    }

    public void BuySkin()
    {
        
        if(PlayerPrefs.GetInt("Diamonds1") + PlayerPrefs.GetInt("Diamonds2") >= pricesSkins[index] && buy)
        {
            int totalDiamonds = PlayerPrefs.GetInt("Diamonds1") + PlayerPrefs.GetInt("Diamonds2");
            Debug.Log("Diamonds: " + totalDiamonds);
            dataPrefab.skinRock1.Add(currentBuySkin.GetComponent<Image>().sprite);
            dataPrefab.skinRock2.Add(currentBuySkin.GetComponent<Image>().sprite);
            dataPrefab.skinRock3.Add(currentBuySkin.GetComponent<Image>().sprite);
            totalDiamonds -= pricesSkins[index];          
            Debug.Log("Diamonds: " + totalDiamonds);
            buttonBuy.interactable = false;
            buy = false;
            
        }
       


    }


}
