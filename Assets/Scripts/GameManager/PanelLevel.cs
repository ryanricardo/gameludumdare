using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelLevel : MonoBehaviour
{
    public GameObject textPanelLoading;

    private void Start()
    {
        textPanelLoading.SetActive(false);
    }
}
