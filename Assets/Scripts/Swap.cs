using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    // Start is called before the first frame update

    private float heightAR;

    [SerializeField] private GameObject AspectRatio20, AspectRatio16;

    void Start()
    {
        heightAR = (float)Screen.height / (float)Screen.width * 9;
        if (heightAR != 19.5f && heightAR != 20)
        {
            if (AspectRatio16 != null)
            {
                AspectRatio16.SetActive(true);
                AspectRatio20.SetActive(false);
            }
        }
        else
            if (AspectRatio20 != null)
            {
                AspectRatio16.SetActive(false);
                AspectRatio20.SetActive(true);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
