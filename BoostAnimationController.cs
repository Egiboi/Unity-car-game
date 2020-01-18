using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostAnimationController : MonoBehaviour {
    // Player object created
    GameObject Player;

    /// <summary>
    /// Player object is assinged to an instance
    /// </summary>
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    /// <summary>
    /// Checks when player is boosting
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Player.GetComponent<PlayerController>().GetBoost()> 0)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
