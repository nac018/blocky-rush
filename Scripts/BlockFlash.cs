using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFlash : MonoBehaviour
{
    private SpriteRenderer blockRenderer;
    private float showTime = 0.9f;
    private float hideTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
    	blockRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    	StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        while (true)
        {
            blockRenderer.enabled = false;
         	yield return new WaitForSeconds(hideTime);
         	blockRenderer.enabled = true;
         	yield return new WaitForSeconds(showTime);
        }
    }
}
