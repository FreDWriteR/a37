using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    private float heightAR;

    private void Awake()
    {

    }

    void Start()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)

        heightAR = Screen.height / Screen.width * 9;

        if (heightAR != 19.5f && heightAR != 20)
        {
            float targetaspect = 1080.0f / 1920.0f;

            // determine the game window's current aspect ratio
            float windowaspect = (float)Screen.width / (float)Screen.height;

            // current viewport height should be scaled by this amount
            float scaleheight = windowaspect / targetaspect;

            // obtain camera component so we can modify its viewport
            Camera camera = gameObject.GetComponent<Camera>();

            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                camera.rect = rect;
            }
            else
            { // add pillarbox
                float scalewidth = 1.0f / scaleheight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
