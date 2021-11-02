using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    [Header("Components")]
    
    [SerializeField]    private GameObject[]    gameObjectsRocks;
    [SerializeField]    private Button[]        arrowsRocks;  
    [HideInInspector]   private Data            data;

    [Header("Tools")]
    [SerializeField]    private int             indexSkin;

    void Start()
    {

        data = FindObjectOfType<Data>();

        indexSkin = 1;

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
                Debug.Log("Left");
            break;

            case 1:
                Debug.Log("Right");
            break;

            case 2:
                if(indexSkin > 1)
                { 
                    indexSkin -= 1;
                    gameObjectsRocks[1].GetComponent<Image>().sprite = data.skinRock1[indexSkin];
                    PlayerPrefs.SetInt("SkinRock1", indexSkin);
                }
            break;

            case 3:
                if(indexSkin < data.skinRock1.Count - 1)
                { 
                    indexSkin += 1;
                    gameObjectsRocks[1].GetComponent<Image>().sprite = data.skinRock1[indexSkin];
                    PlayerPrefs.SetInt("SkinRock1", indexSkin);
                }
            break;

            case 4:
                Debug.Log("Left");
            break;

            case 5:
                Debug.Log("Right");
            break;
        }
    }


}
