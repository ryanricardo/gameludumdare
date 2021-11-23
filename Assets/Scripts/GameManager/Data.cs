using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
   




    void Start()
    {
        // Função para colocar dentro dos arrays as pedras apartir da tag
        rocks = new GameObject[3];
        for(int i = 1; i < rocks.Length; i++)
        {
            rocks[i] = GameObject.FindGameObjectWithTag("RockController " + i);
        }

        // Abaixo, segue todos PlayerPrefs utilizados

        /*
            PlayerPrefs.GetInt("Bonus1");
            PlayerPrefs.GetInt("Bonus2");
            PlayerPrefs.SetInt("Bonus");
            PlayerPrefs.GetInt("DiamondsLvl");
            PlayerPrefs.SetInt("ScenesPassed");
            PlayerPrefs.SetInt("LvlsWon");
            PlayerPrefs.GetFloat("VolumeMusicGame");
            PlayerPrefs.GetFloat("VolumeEffectsGame");
            PlayerPrefs.GetFloat("VolumeGeneral");
            PlayerPrefs.SetFloat("PastVolumeMusic");
            PlayerPrefs.SetFloat("PastVolumeEffects");
            PlayerPrefs.GetInt("SkinRock1");
            PlayerPrefs.GetInt("SkinRock2");
            PlayerPrefs.GetInt("SkinRock3
            PlayerPrefs.GetInt("Diamonds");
            PlayerPrefs.SetInt("MuteGeneral", -1);
            PlayerPrefs.SetInt("MuteMusic", -1);
            PlayerPrefs.SetInt("MuteEffects", -1);
            PlayerPrefs.GetInt("Diamonds1")
            PlayerPrefs.GetInt("Diamonds2")
        */
    }
}
