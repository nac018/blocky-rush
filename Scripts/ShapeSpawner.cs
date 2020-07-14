using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    public GameObject switcher;
    public Sprite[] shapeSprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnShapeSwitcher()
    {        
        int shapeIndex = Random.Range(0, shapeSprites.Length);

        GameObject nextSwitcher = (GameObject)Instantiate(switcher);
        SpriteRenderer switcherRenderer = nextSwitcher.transform.GetChild(0).GetComponent<SpriteRenderer>();
        switcherRenderer.sprite = shapeSprites[shapeIndex];
        nextSwitcher.AddComponent<ShapeMove>(); 
    }
}
