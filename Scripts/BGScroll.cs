using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed; // Background move speed.
    private MeshRenderer meshRenderer;
    private int currentBG;
    public Material[] materials;

    
    // Start is called before the first frame update
    void Start()
    {
        currentBG = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2 (0, Time.time * speed);
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
    public void BGChange()
    {
        if (currentBG < 5){
            currentBG += 1;
        }
        else
        {
            currentBG = 0;
        }
        meshRenderer.material = materials[currentBG];
    }
}
