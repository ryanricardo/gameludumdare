using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonLvl : MonoBehaviour
{
    public int lvl;

    
    public void LoadLevel(){
        SceneManager.LoadScene(lvl);            
    }
}
