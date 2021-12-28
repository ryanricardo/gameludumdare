using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;

public class MarketManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private Data                dataPrefab;
    [SerializeField]    private GameObject[]        skins;
    [SerializeField]    private Button[]            buttons;
    [SerializeField]    private TextMeshProUGUI[]   textsPrices;
    [SerializeField]    private TextMeshProUGUI     textTotalRuby;
    [HideInInspector]   private Data                dataLocal;
    [HideInInspector]   private LevelManager        lv;
    
    [Header("Atributtes")]
    [SerializeField]    private int[]           pricesSkins;
    [SerializeField]    private int[]           index;    
    

    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
        dataLocal = FindObjectOfType<Data>();
        index[0] = 1;
        index[1] = 1;

        textTotalRuby.text = PlayerPrefs.GetInt("RubyTotal").ToString();
        
        for(int i = 1; i < textsPrices.Length; i++)
        {
            textsPrices[i].text = pricesSkins[i].ToString();
        }

        do
        {
            if(skins[index[0]].GetComponent<Image>().sprite == dataPrefab.skinRock1[index[1]])
            {
                buttons[index[0]].interactable = false;
                index[0]++;
                index[1] = 1;
                
            }else 
            {
                index[1]++;
                if(index[0] != skins.Length &&
                index[1] >= dataPrefab.skinRock1.Count)
                {
                    index[0]++;
                    index[1] = 1;
                }
            }
            
        }while(index[0] != skins.Length);


    }

    void Update()
    {

            

        

    }


    public void BuySkin(int index)
    {
        lv.PlayClipClickButton();

        if(PlayerPrefs.GetInt("RubyTotal") >= pricesSkins[index])
        {
            switch(PlayerPrefs.GetInt("valueLanguage"))
            {
                case 0:
                    buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = "Buy skin";
                break;

                case 1:
                    buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = "Comprar";
                break;
            }

            buttons[index].interactable = false;
            dataPrefab.skinRock1.Add(skins[index].GetComponent<Image>().sprite);
            dataPrefab.skinRock2.Add(skins[index].GetComponent<Image>().sprite);
            dataPrefab.skinRock3.Add(skins[index].GetComponent<Image>().sprite);
            PlayerPrefs.SetInt("RubyTotal", PlayerPrefs.GetInt("RubyTotal") - pricesSkins[index]);
            textTotalRuby.text = PlayerPrefs.GetInt("RubyTotal").ToString();


        }else 
        {
            switch(PlayerPrefs.GetInt("valueLanguage"))
            {
                case 0:
                    buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = "No rubies";
                break;

                case 1:
                    buttons[index].GetComponentInChildren<TextMeshProUGUI>().text = "Sem rubys";
                break;
            }
        }
        

    }

    public void BuyRuby(int rubyToAdd)
    {
        lv.PlayClipClickButton();
        Debug.Log(PlayerPrefs.GetInt("RubyTotal"));
        PlayerPrefs.SetInt("RubyTotal", PlayerPrefs.GetInt("RubyTotal") + rubyToAdd);
        Debug.Log(PlayerPrefs.GetInt("RubyTotal"));
    }

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id.Equals("rubypack1"))
        {
            BuyRuby(100);
            textTotalRuby.text = PlayerPrefs.GetInt("RubyTotal").ToString();
        }

        if(product.definition.id.Equals("rubypack2"))
        {
            BuyRuby(500);
            textTotalRuby.text = PlayerPrefs.GetInt("RubyTotal").ToString();
        }

        if(product.definition.id.Equals("rubypack3"))
        {
            BuyRuby(1000);
            textTotalRuby.text = PlayerPrefs.GetInt("RubyTotal").ToString();
        }

        if(product.definition.id.Equals("adsfree"))
        {
         PlayerPrefs.GetInt("anuncioFree");
         PlayerPrefs.SetInt("anuncioFree",1);   
        }

        
    }

}
