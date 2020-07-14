using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
	public static WindowManager instance;

    public Renderer background;
    public GameObject gameCanvas;
	private float gameWindow; // The half width of the actual game window.
	private Vector2 screenBounds;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // Adjust the screen.
        RectTransform rt = gameCanvas.GetComponent<RectTransform>();
        float scale = (float)gameCanvas.transform.localScale.y;
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

        // Get the appropriate width of spawner
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float BgWidth_half = background.bounds.size.x / 2;
        if (BgWidth_half <= screenBounds.x){
            gameWindow = BgWidth_half;
        }
        else{
            gameWindow = screenBounds.x;
        }
    }
    public float GetWidth()
    {
        return gameWindow;
    }
}
