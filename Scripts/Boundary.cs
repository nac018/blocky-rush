using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
	private float screenWidth;
	private float objWidth;

    // Start is called before the first frame update
    void Start()
    {
    	screenWidth = WindowManager.instance.GetWidth();
    	objWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x/2;
    }
    // Update is called once per frame
    void LateUpdate()
    {
		transform.position = new Vector2(
			Mathf.Clamp(transform.position.x, screenWidth * -1 + objWidth, screenWidth - objWidth),
			transform.position.y);
    }
}
