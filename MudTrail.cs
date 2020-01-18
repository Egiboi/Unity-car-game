using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudTrail : MonoBehaviour
{
    //Class is used to toggle Mud trail from the car when hitting a mud puddle
    TrailRenderer trail;
    /// <summary>
    /// Sets variables
    /// </summary>
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;

    }
    /// <summary>
    /// Checks when the car hits a collider and checks it's tag. If the tag is "mud" the certain trail which hit the collider will start emitting the mud trail for the predefined amount in TimeKiller method
    /// </summary>
    /// <param name="other"></param>

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mud"))
        {
            trail.emitting = true;
            StartCoroutine(TimeKiller());
            
            

        }
    }
    
    /// <summary>
    /// This method makes the trail turn off after 3 in game seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeKiller()
    {
        yield return new WaitForSeconds(3);
        trail.emitting = false;
    }
}

