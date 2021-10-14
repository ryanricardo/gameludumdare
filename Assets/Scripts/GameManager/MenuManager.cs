using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private GameObject      gameObjectScenes;
    [SerializeField]    private Button[]        scenesInMenu;

    [Header("Atributtes Scenes")]
    [SerializeField]    private bool            open;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("ScenesPassed"));
        for(int i = 0; i < scenesInMenu.Length; i++)
        {
            
            if(i <= PlayerPrefs.GetInt("ScenesPassed"))
            {
                scenesInMenu[i].interactable = true;
            }else 
            {
                scenesInMenu[i].interactable = false;
            }
            
        }
    }
    
    public void ButtonPlay(int scene){
        SceneManager.LoadScene(scene);   
    }

    public void ButtonQuit(){
        Application.Quit();
    }

    public void ButtonScenes()
    {
        open ^= true;

        if(!open)
        {
            gameObjectScenes.gameObject.SetActive(false); 
        }else 
        {
            gameObjectScenes.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        
    }
}
