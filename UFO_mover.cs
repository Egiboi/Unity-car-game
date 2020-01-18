using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_mover : MonoBehaviour {
    //Serializefields are adjustable in unity editor
    [SerializeField]
    GameObject Player, UfoLight, Ufo;
    [SerializeField]
    float circleRadius, speed, circleSpeedDivider, spinSeconds, secondsBeforeAttack, howCloseToPlayer; 
    // other values are used as privately
    bool targetSet, spinner, circleStart, waitStart, startMoving;
    Vector2 target, circleStartPoint;
    float angle;
    Animator flash;
    

    //Sets ufo's light unactive so it always starts in the right state
    //Gets the ufo's animator component
    private void Start()
    {
        UfoLight.SetActive(false);
        flash = Ufo.GetComponent<Animator>();
    }

    // update happens between every frame
    void Update()
    {
        // a constant variable that counts time, used in calculating the angle of the circle of movement. Used in SpinningVector() method. circleSpeedDivider is used to change the speed of doing the circle
        angle += Time.deltaTime/circleSpeedDivider;
        //Checks if spinner boolean is false. Means we don't want the ufo to be currently doing the circle movement. We want it to move straight to the next location where to circle
        if (spinner == false)
        {
            // Sets a a target for the staright movement. Boolean makes sure the target is set only once and not on every frame (as update works)
            if (targetSet == false)
            {
                // uses player gameobjects location and the targetPosition method to set a goal for the straight movement.
                target = targetPosition(Player.transform.position);
                // sets target set boolean to true, so the earlier if statement doesn't repeat
                targetSet = true;
            }
            //Moves to the earlier set target at a straight course. The set target is altered slightly, so it will be circleRadius amount to positive on the X-axis. This will make sure the circle will be started from it's it's target location. 
            //Otherwhise would the target would be the middle of the circle and it would have to jump jaggedly to the circle rim
            transform.position = Vector2.Lerp(transform.position, new Vector2(target.x + circleRadius, target.y), speed*Time.deltaTime);

            
 
            //If the distance between the ufo and the earlier defined target+circle radius is zero. The spinner is set true. Meaning we want the ufo to do it's circle movement it the next update.
            if (Vector2.Distance(new Vector2(transform.position.x,transform.position.y), new Vector2(target.x + circleRadius, target.y)) <0.01)
            {
                spinner = true;
            }
        }
        //Start of the circle movemnt, checks if it's true.
        else if(spinner == true)
        {
            // THis if statement which starts the coroutine. The coroutine acts as countdown to certain commands. The boolean check makes sure it is iniated only once per circle movement.
            if (waitStart == false)
            {
                // StartCouroutine starts the IEnumerator. Wait() is the IEnumerator method that is defined later in the code.
                StartCoroutine(wait());
                //Boolean makes sure it is iniated only once per circle movement
                waitStart = true;
            }
            //This boolean is activated it the coroutine, meaning after a defined amount of seconds (in the wait method) this boolean will be set true. 
            if (startMoving == true)
            {
                //Set's the warning flasher to unactive as the "attack" is started
                flash.enabled = false;
                //this movement is the circle movement, so the attack
                transform.position = Vector2.MoveTowards(transform.position, SpinningVector(), speed);
                // Sets the ufo's game ending collider on and the light that visualizes this area.
                UfoLight.SetActive(true);
            }
            // this statement is activated before the earlier if statement. It activates in between the straight movement to target and the circle movement. It is used a grace period before the "attack" is iniated
            else
            {
                //flash is a animation the is used as a visual cue for the player that the ufo is about to start it's attack. Only active during grace period.
                flash.enabled=true;
                
            }

            
        }
       

    }
    // this is the IEnumerator. It used to wait for a certain amount of seconds before starting the next  row of code with the "yield return new WaitForSeconds()" commands.
    IEnumerator wait()
    {
        // method is called when the spinner is set true and the coroutine is started
        // the initial wait is the time between stopping at target and starting the circle attack
        yield return new WaitForSeconds(secondsBeforeAttack);
        //After secondBeforeAttack amount of seconds, ssets boolean value of startMoving to true. This will initiate the circle movement next update
        startMoving = true;
        //Sets angle to zero. This is used to make sure the circle is always started from the same parameters. (Necessary?)
        angle = 0;
        // Second Wait is the time we want the ufo to circle. Does not effect the speed of the circle
        yield return new WaitForSeconds(spinSeconds);
        //Following boolean value changes are used the clear to the boolean values so the code starts from a clear slate again. Meaning this code will run on a loop for the whole game. 
        targetSet = false;
        UfoLight.SetActive(false);
        spinner = false;
        waitStart = false;
        startMoving = false;

    }
    // This method is used to set a target for the straight movement
    Vector2 targetPosition(Vector2 position)
    {
        // sets a random point inside player's 1 size circle times the howCloseToPlayer (Breaks if set to 1, because of while loop?)
        position = position + UnityEngine.Random.insideUnitCircle*howCloseToPlayer;
        // this code makes sure the ufo doesn't land at 1 distance from the player. So the player should always have time to react. Repeats position setting until distance is over 1
        while (Vector2.Distance(Player.transform.position, new Vector2 (position.x+circleRadius, position.y)) < 1)
            position = position + UnityEngine.Random.insideUnitCircle * howCloseToPlayer;
        //returns the first position that gets out of the while loop
        return position;
    }
    Vector2 SpinningVector()
    {
        //Sets and returns a spinning vector from target location when called
        float x = target.x + Mathf.Cos(angle)*circleRadius;
        float y = target.y + Mathf.Sin(angle)*circleRadius;
        return new Vector2(x, y);
    }



}
