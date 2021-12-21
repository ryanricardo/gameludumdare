using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotaocomTimer : MonoBehaviour
{
    public bool ativo=false;
    public Animator anim;
    public Chave chave;
    public GameObject text;
    public bool funciona=false;
    public float tempo;
    public AudioSource sourceButton;
    public AudioClip clipOpenButton;
    private bool playClipOneTime;
    private float t = 0;
    private bool countDown = false;

    void Start(){
        playClipOneTime = true;
        t = tempo+1;
    }

    void Update()
    {
        
        if(chave.chave)
        {
            funciona=true;
        }

        if(ativo && countDown){
            t -= Time.deltaTime;
            text.SetActive(true);
            text.GetComponent<TextMeshPro>().text = Mathf.FloorToInt(t).ToString();
        }else{
            text.SetActive(false);
            t = tempo+1;
        }
        if(t<1){
            Fechar();
            countDown = false;
        }
        
    }

    public void EnterRockButton(Transform t, GameObject g)
    {
        t.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f);
        g.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        sourceButton.PlayOneShot(clipOpenButton);
        Debug.Log("d");
    }

    void OnTriggerEnter2D(Collider2D col){

        if(funciona && !ativo){
            if(col.gameObject.tag=="Player" || col.gameObject.tag=="RockController 1" || col.gameObject.tag == "RockController 2"){                
                ativo=true;
                sourceButton.PlayOneShot(clipOpenButton);
                anim.SetTrigger("Pre");
            } 
        }       

    }

    void OnTriggerExit2D(Collider2D col){
        if(ativo){
            if(col.gameObject.tag=="Player" || col.gameObject.tag=="RockController 1" || col.gameObject.tag == "RockController 2"){
            //    StartCoroutine("Abrir");
                countDown = true;
            }
        }        
    }

    void Fechar(){
        sourceButton.PlayOneShot(clipOpenButton);
        ativo=false;
        anim.SetTrigger("Normal");
    }

    //  private IEnumerator Abrir()
    //  {
    //      yield return new WaitForSeconds(tempo);
    //     sourceButton.PlayOneShot(clipOpenButton);
    //      ativo=false;
    //      anim.SetTrigger("Normal");
    //  }
}
