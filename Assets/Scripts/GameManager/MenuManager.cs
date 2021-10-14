using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]    private GameObject  gameObjectScenes;

    [Header("Atributtes Scenes")]
    [SerializeField]    private bool        open;

    private void Start()
    {
        
    }
    
    public void ButtonPlay(){
        SceneManager.LoadScene(1);   
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
