using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamController : MonoBehaviour
{
    public GameObject creamMaker;
    public GameObject lineMaker;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("creamMaker On!");
            lineMaker.SetActive(false);
            creamMaker.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("lineMaker On!");
            creamMaker.SetActive(false);
            lineMaker.SetActive(true);
        }
    }
}
