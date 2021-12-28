using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

public int anuncioFree;

#if UNITY_IOS
   private string gameid="4515922";
#elif UNITY_ANDROID
   private string gameid="4515923";
#endif

    // bool testmode=true;
    bool testmode=false;

   
   void Start()
   {
       anuncioFree=PlayerPrefs.GetInt("anuncioFree");
       Advertisement.Initialize(gameid, testmode);
   }

   public void ShowInterstitialAd()
   {
       if(Advertisement.IsReady() && PlayerPrefs.GetInt("anuncioFree")!=0)
       {
            Advertisement.Show();
            Debug.Log("Mostrando anuncio"); 
            Debug.Log("Mortes: " + PlayerPrefs.GetInt("mortes"));
            // Debug.Log(PlayerPrefs.GetInt("mortesParaAnuncio"));
            Debug.Log("Vitorias: " + PlayerPrefs.GetInt("vitorias"));
            // Debug.Log(PlayerPrefs.GetInt("vitoriasParaAnuncio"));
       }
       else
      {
           Debug.Log("Não está pronto o anuncio");

       }
   }

}
