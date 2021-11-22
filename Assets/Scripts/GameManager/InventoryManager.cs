using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    [Header("Components")]
    
    [SerializeField]    private GameObject[]    gameObjectsRocks;
    [SerializeField]    private Button[]        arrowsRocks;  
    [SerializeField]    private Data            data;
    

    [Header("Tools")]
    [HideInInspector]   private int[]            indexSkin;

    void Start()
    {

        indexSkin = new int[3];
        indexSkin[0] = PlayerPrefs.GetInt("SkinRock1");
        indexSkin[1] = PlayerPrefs.GetInt("SkinRock2");
        indexSkin[2] = PlayerPrefs.GetInt("SkinRock3");

        gameObjectsRocks[0].GetComponent<Image>().sprite = 
        data.skinRock1[PlayerPrefs.GetInt("SkinRock1")];

        gameObjectsRocks[1].GetComponent<Image>().sprite = 
        data.skinRock2[PlayerPrefs.GetInt("SkinRock2")];

        gameObjectsRocks[2].GetComponent<Image>().sprite = 
        data.skinRock3[PlayerPrefs.GetInt("SkinRock3")];
        /* Os botões seguem a sequencia das pedras que estão no inventario de 0 a 5 */
        
        arrowsRocks[0].onClick.AddListener(delegate{Arrows(0);});
        arrowsRocks[1].onClick.AddListener(delegate{Arrows(1);});

        arrowsRocks[2].onClick.AddListener(delegate{Arrows(2);});
        arrowsRocks[3].onClick.AddListener(delegate{Arrows(3);});

        arrowsRocks[4].onClick.AddListener(delegate{Arrows(4);});
        arrowsRocks[5].onClick.AddListener(delegate{Arrows(5);});

        
        
    }

    void Update()
    {
        
    }

    void Arrows(int index)
    {
        switch(index)
        {
            case 0:
                if(indexSkin[0] > 1)
                { 
                    indexSkin[0] -= 1;
                    gameObjectsRocks[0].GetComponent<Image>().sprite = data.skinRock1[indexSkin[0]];
                    PlayerPrefs.SetInt("SkinRock1", indexSkin[0]);
                }
            break;

            case 1:
                if(indexSkin[0] < data.skinRock1.Count - 1)
                { 
                    indexSkin[0] += 1;
                    gameObjectsRocks[0].GetComponent<Image>().sprite = data.skinRock1[indexSkin[0]];
                    PlayerPrefs.SetInt("SkinRock1", indexSkin[0]);
                }
            break;

            case 2:
                if(indexSkin[1] > 1)
                { 
                    indexSkin[1] -= 1;
                    gameObjectsRocks[1].GetComponent<Image>().sprite = data.skinRock2[indexSkin[1]];
                    PlayerPrefs.SetInt("SkinRock2", indexSkin[1]);
                }
            break;

            case 3:
                if(indexSkin[1] < data.skinRock2.Count - 1)
                { 
                    indexSkin[1] += 1;
                    gameObjectsRocks[1].GetComponent<Image>().sprite = data.skinRock2[indexSkin[1]];
                    PlayerPrefs.SetInt("SkinRock2", indexSkin[1]);
                }
            break;


            case 4:
                if(indexSkin[2] > 1)
                { 
                    indexSkin[2] -= 1;
                    gameObjectsRocks[2].GetComponent<Image>().sprite = data.skinRock3[indexSkin[2]];
                    PlayerPrefs.SetInt("SkinRock3", indexSkin[2]);
                }
            break;

            case 5:
                if(indexSkin[2] < data.skinRock3.Count - 1)
                { 
                    indexSkin[2] += 1;
                    gameObjectsRocks[2].GetComponent<Image>().sprite = data.skinRock3[indexSkin[2]];
                    PlayerPrefs.SetInt("SkinRock3", indexSkin[2]);
                }
            break;

        }
    }


}
