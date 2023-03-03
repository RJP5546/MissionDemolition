using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition s;

    [Header("Inscribed")]
    public Text uitLevel; //The UITest_Level text
    public Text uitShots; //The UIText_Shots text
    public Vector3 castlePos; //The place to put castles
    public GameObject[] castles; //An array of castles

    [Header("Dynamic")]
    public int level; //The current level
    public int levelMax; //The number of levels
    public int shotsTaken;
    public GameObject castle; //the current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam mode

    
    void Start()
    {
        s = this; //Define the singleton

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        //Get rid of any old castle
        if (castle != null)
        {
            Destroy(castle);
        }

        //Destroy old projectiles is they exist
        Projectile.DESTROY_PROJECTILES();

        //Instantiate new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        //Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        //show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        //Check for level end
        if ( (mode == GameMode.playing) && Goal.goalMet)
        {
            //Channge mode to stop checking for level end
            mode = GameMode.levelEnd;

            //Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel ()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    //Static method that allows code anywhere to increment shotsTaken
    static public void SHOT_FIRED ()
    {
        s.shotsTaken++;
    }

    //Static method that allows code anywhere to refrence S.castle
    static public GameObject GET_Castle()
    {
        return s.castle;
    }
}
