using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float scale1 = .8f, scale2 = .6f, interpolation = .5f, disProp = 1.8f, scroll_pos;
    float[] pos;
    
    private void Start()
    {
        scrollbar.value = scroll_pos;
    }

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0)){
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }       
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / disProp) && scroll_pos > pos[i] - (distance / disProp))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], interpolation);
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / disProp) && scroll_pos > pos[i] - (distance / disProp))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(scale1, scale1), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j!=i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(scale2, scale2), 0.1f);
                    }
                }
            }
        }
    }
}