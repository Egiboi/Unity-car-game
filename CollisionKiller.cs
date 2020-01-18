using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionKiller : MonoBehaviour {

    GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    /// <summary>
    /// When the player collides with a wall head on while boosting this calls the PlayerKiller method that kills the player and brings up the game over menu scene
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && Player.GetComponent<PlayerController>().GetBoost() > 0)
        {
            Debug.Log("Collision Death");
            Player.GetComponent<PlayerController>().PlayerKiller();
        }
        if (collision.gameObject.CompareTag("Cow"))
        {
            Debug.Log("Collision with cow - death");
            Player.GetComponent<PlayerController>().PlayerKiller();
        }
    }
}
