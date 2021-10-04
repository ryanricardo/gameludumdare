using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]private PlayerController playerController;
    [SerializeField]private GameManager      gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.gameOver = true;
        }
    }
}
