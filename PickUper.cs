using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUper : MonoBehaviour {

    GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    /// <summary>
    /// When player touches a beer the player gains a point
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the trigger is an pick up, it is disabled and a point is added
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            Player.GetComponent<PlayerController>().addPoint();
        }
    }
}
