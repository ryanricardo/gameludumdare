using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public GameObject[] rocksSkins;
    public Data         data;
    


    void Start()
    {

        Debug.Log(PlayerPrefs.GetInt("SkinRock1"));
        Debug.Log(PlayerPrefs.GetInt("SkinRock2"));
        Debug.Log(PlayerPrefs.GetInt("SkinRock3"));

        rocksSkins[1].GetComponent<Image>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
        rocksSkins[2].GetComponent<Image>().sprite = data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];
        rocksSkins[3].GetComponent<Image>().sprite = data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];


        
    }

    void Update()
    {
        
    }

    public void Arrows(int index)
    {
        Debug.Log(PlayerPrefs.GetInt("SkinRock1"));
        Debug.Log(PlayerPrefs.GetInt("SkinRock2"));
        Debug.Log(PlayerPrefs.GetInt("SkinRock3"));

        switch(index)
        {
            case 1:
                if(PlayerPrefs.GetInt("SkinRock1") > 1)
                {
                    PlayerPrefs.SetInt("SkinRock1", PlayerPrefs.GetInt("SkinRock1") - 1);
                    rocksSkins[1].GetComponent<Image>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
                }
            break;

            case 2:
                if(PlayerPrefs.GetInt("SkinRock1") < data.skinRock1.Count - 1)
                {
                    PlayerPrefs.SetInt("SkinRock1", PlayerPrefs.GetInt("SkinRock1") + 1);
                    rocksSkins[1].GetComponent<Image>().sprite = data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];
                }
            break;

            case 3:
                if(PlayerPrefs.GetInt("SkinRock2") > 1)
                {
                    PlayerPrefs.SetInt("SkinRock2", PlayerPrefs.GetInt("SkinRock2") - 1);
                    rocksSkins[2].GetComponent<Image>().sprite = data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];
                }
            break;

            case 4:
                if(PlayerPrefs.GetInt("SkinRock2") < data.skinRock2.Count - 1)
                {
                    PlayerPrefs.SetInt("SkinRock2", PlayerPrefs.GetInt("SkinRock2") + 1);
                    rocksSkins[2].GetComponent<Image>().sprite = data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];
                }
            break;

            case 5:
                if(PlayerPrefs.GetInt("SkinRock3") > 1)
                {
                    PlayerPrefs.SetInt("SkinRock3", PlayerPrefs.GetInt("SkinRock3") - 1);
                    rocksSkins[3].GetComponent<Image>().sprite = data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];
                }
            break;

            case 6:
                if(PlayerPrefs.GetInt("SkinRock3") < data.skinRock3.Count - 1)
                {
                    PlayerPrefs.SetInt("SkinRock3", PlayerPrefs.GetInt("SkinRock3") + 1);
                    rocksSkins[3].GetComponent<Image>().sprite = data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];
                }
            break;
        }
    }


}
