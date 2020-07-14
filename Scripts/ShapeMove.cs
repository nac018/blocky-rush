using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMove : MonoBehaviour
{
    public static float speed;
    private ShapeSpawner shapeSpawner;
    private Sprite[] shapeSprites;
    private Vector2 screenBounds;
    private SpriteRenderer switcherRenderer;
    private int shapeCount;
    // Start is called before the first frame update
    void Start()
    {
        shapeCount = Random.Range(0, 5);
        this.gameObject.tag = "ShapeSwitcher";
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        switcherRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        shapeSpawner = GameObject.Find("Shape Spawner").GetComponent<ShapeSpawner>();
        shapeSprites = shapeSpawner.shapeSprites;
        StartCoroutine(ChangeShape());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (transform.position.y < - screenBounds.y * 1.1f ){
        	Destroy(gameObject);
        }
    }

    IEnumerator ChangeShape()
    {
        while (true)
        {
            Sprite nextSprite = shapeSprites[shapeCount];
            switcherRenderer.sprite = nextSprite;
            shapeCount ++;
            if (shapeCount == 5){
                shapeCount = 0;
            }
            yield return new WaitForSeconds(0.6f);
        }
    }
}
