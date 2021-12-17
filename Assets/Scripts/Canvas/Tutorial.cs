using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] string tutorial = "tutorialON";
    [SerializeField] GameObject[] panelTutorial;
    [SerializeField] CanvasPlayerController canvas;
    int panelNumber;

    void Start()
    {
        tutorial = PlayerPrefs.GetString("Tutorial");
        if(tutorial=="tutorialON"){
            panelTutorial[0].SetActive(true);
            panelNumber = 1;
        }else{
            panelTutorial[0].SetActive(false);
        }
    } 

    public void ButtomArrow(){
        canvas.sourceEffectsMenu.PlayOneShot(canvas.clipClickButtonMenu);
        switch (panelNumber){            
            case 1:
                panelTutorial[1].SetActive(false);
                panelTutorial[2].SetActive(true);
                panelNumber++;
            break;
            case 2:
                panelTutorial[0].SetActive(false);
                panelNumber = 1;
                PlayerPrefs.SetString("Tutorial", "tutorialOFF");
                canvas.LevelState(GameManager.State.PLAY);
                break;
        }
    }
}
