using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SwitchControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(9.0f, GoToNext);
    }

    void GoToNext() {
        SceneManager.LoadScene("StartScene");
    }

}
