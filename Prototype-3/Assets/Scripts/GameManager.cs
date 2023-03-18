using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables for starting animation
    public Transform startingPoint;
    public float lerpSpeed;
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerScript.gameOver = true;
        StartCoroutine("PlayIntro");
    }

    //A coroutine does something over time instead of in a single frame.
    //Allows start animation to play based on lerp speed
    private IEnumerator PlayIntro()
    {
        //Start and end positions for start animation
        Vector3 startPos = playerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered;
        //Where player is between start and end position
        float fractionOfJourney = 0;
        //Slow down animation to look like walking
        playerScript.GetComponent<Animator>().speed = 0.5f;

        //While journery isn't done
        while (fractionOfJourney < 1)
        {
            //Speed times total time since start gives current distance travelled
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            //Lerp returns a number that is the fraction between the start and end position
            //So a fractionOfJournery of 0.5f would return the halfway point between startPos and endPos
            playerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        playerScript.gameOver = false;
        playerScript.GetComponent<Animator>().speed = 1;
        playerScript.dirtParticle.Play();
    }
}
