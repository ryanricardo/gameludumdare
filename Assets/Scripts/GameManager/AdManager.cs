using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{



#if UNITY_IOS
   private string gameid="4515922";
#elif UNITY_ANDROID
   private string gameid="4515923";
#endif

    // bool testmode=true;
    bool testmode=false;

   
   void Start()
   {
       Advertisement.Initialize(gameid, testmode);
   }

   public void ShowInterstitialAd()
   {
       if(Advertisement.IsReady())
       {
        Advertisement.Show();
        Debug.Log("Mostrando anuncio");
       }
       else
      {
           Debug.Log("Não está pronto o anuncio");
       }
   }

}
