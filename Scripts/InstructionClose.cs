using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionClose : MonoBehaviour
{
	public GameObject pauseButton;
    void OnMouseDown()
    {
        // Input.simulateMouseWithTouches = false;
        // Input.ResetInputAxes();
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        StartCoroutine(InstrucFade());
    }
    IEnumerator InstrucFade()
    {
        yield return new WaitForSecondsRealtime(0.1f);
    	gameObject.SetActive(false);
    }
}
