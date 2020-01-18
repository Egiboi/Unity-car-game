using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseLightControl : MonoBehaviour
{

 
    /// <summary>
    /// Checks if the key S is being clicked and activates/disables reverse light
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
        {
            this.GetComponent<Light>().enabled = true;
        }
        else
        {
            this.GetComponent<Light>().enabled = false;
        }
    }
}
