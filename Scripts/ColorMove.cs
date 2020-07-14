using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMove : MonoBehaviour
{
    public static float speed;
    private Vector2 screenBounds;
    private SpriteRenderer colorSwitchRenderer;
    private int colorCount;
    // Start is called before the first frame update
    void Start()
    {
        colorCount = Random.Range(0, 5);
        this.gameObject.tag = "ColorSwitcher";
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        colorSwitchRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (transform.position.y < - screenBounds.y * 1.1f ){
        	Destroy(gameObject);
        }
    }
    IEnumerator ChangeColor()
    {
        while (true)
        {
            Color nextColor = ColorSpawner.colors[colorCount];
            colorSwitchRenderer.color = nextColor;
            colorCount ++;
            if (colorCount == 5){
                colorCount = 0;
            }
            yield return new WaitForSeconds(0.6f);
        }
    }
}
