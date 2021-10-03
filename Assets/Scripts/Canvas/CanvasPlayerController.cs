using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlayerController : MonoBehaviour
{

    [Header("Components")]
    [SerializeField]private PlayerController playerController;
    [SerializeField]private Slider[]         sliderResistance;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ResistenceController();
    }

    void ResistenceController()
    {
       
       
        
       if(playerController.rocksResistancesEnd[0])
       {
           sliderResistance[0].interactable = false;
       }else if (!playerController.rocksResistancesEnd[0])
       {
           sliderResistance[0].interactable = true;
           sliderResistance[0].value = playerController.rocksResistances[0];
       }

        
       if(playerController.rocksResistancesEnd[1])
       {
           sliderResistance[1].interactable = false;
       }if (!playerController.rocksResistancesEnd[1])
       {
           sliderResistance[1].interactable = true;
           sliderResistance[1].value = playerController.rocksResistances[1];
       }

        
    }
}
