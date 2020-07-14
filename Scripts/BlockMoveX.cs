using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveX : MonoBehaviour
{
    private float speedX;
    private bool dirRight;
    private float startPosX;
    // private Vector2 screenBounds;
    private float screenWidth;
    // Start is called before the first frame update
    void Start()
    {
        speedX = 1.0f;
        dirRight = true;
        startPosX = transform.position.x;
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenWidth = WindowManager.instance.GetWidth();
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRight) // Move towards right
        {
        	transform.Translate(Vector2.right * Time.deltaTime * speedX);
        }
        else // Move towards left
        {
        	transform.Translate(Vector2.left * Time.deltaTime * speedX);
        }

        float distance = transform.position.x - startPosX;
        if (distance >= 4 * screenWidth /5)
        {
        	dirRight = false;
        }
        if (distance <= 0)
        {
        	dirRight = true;
        }

    }
}
