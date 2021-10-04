using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Canvas : MonoBehaviour
{

    private PlayerController playerController;
    private bool             menuOpen;
    [SerializeField]private GameObject imageMenu;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
        if(playerController.getKeyDownEsc)
        {
            menuOpen ^= true;
            if(menuOpen)
            {
                imageMenu.gameObject.SetActive(false);
            }else 
            {
                imageMenu.gameObject.SetActive(true);
            }
            
        }
    }
}
