using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndSize : MonoBehaviour
{
    public GameObject StartCanvas;
    // Start is called before the first frame update
    void Start()
    {
        // Adjust the screen.
        RectTransform rt = StartCanvas.GetComponent<RectTransform>();
        float scale = (float)StartCanvas.transform.localScale.y;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = rt.rect.width / rt.rect.height;
        if (screenRatio >= targetRatio)
        {
        	Camera.main.orthographicSize = rt.rect.height * scale/ 2;
        }
        else
        {
        	float differenceInSize = targetRatio / screenRatio;
        	Camera.main.orthographicSize = rt.rect.height * scale/ 2 * differenceInSize;
        }
    }

}
