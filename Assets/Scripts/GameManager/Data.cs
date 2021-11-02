using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]    public  List<Sprite>    skinRock1 = new List<Sprite>();
    [HideInInspector]   public  GameObject[]    rocks;

    void Start()
    {
        rocks = new GameObject[3];
        for(int i = 1; i < rocks.Length; i++)
        {
            rocks[i] = GameObject.FindGameObjectWithTag("RockController " + i);
        }
    }
}
