using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
	private float deltaX;
    // private float deltaY;
	private Rigidbody2D rb;
    private float startY;
    // Google Chrome has bug. It does not detect began touch phase.
    // So add a bool to check manually if it is a began phase.
    private bool isBegan = true;

    // Start is called before the first frame update
    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.touchCount > 0)
    	{
    		Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began || isBegan){
            	isBegan = false;
            	deltaX = touchPos.x - transform.position.x;
                // deltaY = touchPos.y - transform.position.y;
            }
            else if (touch.phase == TouchPhase.Moved){
            	rb.MovePosition(new Vector2(touchPos.x - deltaX, startY));
            }
            else if (touch.phase == TouchPhase.Ended){
            	rb.velocity = Vector2.zero;
            	isBegan = true;
            }
    	}
    }
}
