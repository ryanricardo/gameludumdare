using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]    public  List<Sprite>    skinRock1 = new List<Sprite>();
    [SerializeField]    public  List<Sprite>    skinRock2 = new List<Sprite>();
    [SerializeField]    public  List<Sprite>    skinRock3 = new List<Sprite>();
    [SerializeField]    public  Sprite[]        skinsRewardRock1;
    [SerializeField]    public  Sprite[]        skinsRewardRock2;
    [SerializeField]    public  Sprite[]        skinsRewardRock3;
    [HideInInspector]   public  GameObject[]    rocks;
    // public Sprite[]                         

    [Header("Functions")]
    [SerializeField]    private bool            restartPrefs;

    void Start()
    {
        
        if(restartPrefs)
        {
            PlayerPrefs.SetInt("SkinRock1", 0);
            PlayerPrefs.SetInt("SkinRock2", 0);
            PlayerPrefs.SetInt("SkinRock3", 0);
        }



        // Função para colocar dentro dos arrays as pedras apartir da tag
        rocks = new GameObject[3];
        for(int i = 1; i < rocks.Length; i++)
        {
            rocks[i] = GameObject.FindGameObjectWithTag("RockController " + i);
        }
    }
}
