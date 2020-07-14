using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private float deltaX;
    // private float deltaY;
    private Rigidbody2D rb;
    private float startY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)){
                deltaX = mousePos.x - transform.position.x;
            }
            else if (Input.GetAxisRaw("Mouse X") != 0){
                rb.MovePosition(new Vector2(mousePos.x - deltaX, startY));
            }
            else if (Input.GetMouseButtonUp(0)){
                rb.velocity = Vector2.zero;
            }
        }
    }
}
