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
        ResistenceController();
        
    }

    void ResistenceController()
    {
       sliderResistance[0].interactable = true;
       if(sliderResistance[0].value > 50) { sliderResistance[0].value = playerController.rocksResistances[0] + 50; }
       else sliderResistance[0].value = playerController.rocksResistances[1];
    }
}
