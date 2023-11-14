using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Jacob Meyer
 * 
 * This script is for the Score Board UI element. it should be placed on the element of the same name.
 * this will allow the score to be counted and, when modifyed, it will update the UI element.
 * 
 * Created 10/27/2023
 * 
 * changelog: 
 * Jacob Meyer 10/30/2023: changed adjustScore to public to allow elements to update the score.
 * changed adjustScore to return the total score at conclusion.
 */

//connect to this with using static ScoreBoard;

public class ScoreBoard : MonoBehaviour
{
    private int scoreValue = 0;             //the total score of the player. do not mess with this outside this program.

    public SpriteRenderer[] place;          //the location that will be changed. 0 is ones 1 is tens... 6 is millions
    public Sprite[] digits;                 //the diget that will be displayed. this is maped 1 to 1.

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This method will update the score element of the UI
    void UpdateScore()
    {
        int score = scoreValue;     //this will hold the temporary score to be checked.

        for (int i = 0; i < place.Length; i++) 
        {
            int temporaryInt = score % 10;      //the the lowest place and save it.
            place[i].sprite = digits[temporaryInt];
            score = score / 10;
        }
    }

    //This method will add the given int to the score and then update the UI. Use this method to add or subtract score.
    public int adjustScore(int valueToAdd)
    {
        scoreValue += valueToAdd;
        UpdateScore();
        return scoreValue;
    }

    // getter method for the current score
    public int getScore()
    {
        return scoreValue;
    }
}
