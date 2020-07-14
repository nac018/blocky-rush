using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSpawner : MonoBehaviour
{
    public GameObject colorSwitcher; // Bubble prefab.
    public static Color[] colors; // No need to assign manually
    public BlockSpawner blockSpawner;
    // Start is called before the first frame update
    void Start()
    {
        colors = blockSpawner.colors;
    }
    public void SpawnColorSwitcher()
    {        
        GameObject nextSwitcher = (GameObject)Instantiate(colorSwitcher);
        nextSwitcher.AddComponent<ColorMove>();
    }
}
