using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathfall : MonoBehaviour {
    GameObject Player;
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// Detects when player goes of the map and enters the border collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            Debug.Log("Fall death");
            Player.GetComponent<PlayerController>().PlayerKiller();
        }
    }
}
