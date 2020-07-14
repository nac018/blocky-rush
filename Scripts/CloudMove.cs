using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float speed;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.1f;
        this.gameObject.tag = "Cloud";
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (transform.position.y < - screenBounds.y * 1.1f ){
        	Destroy(gameObject);
        }
    }
}
