using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DataStruct;
/*
 * author: Timothy Kwan
 * purpose: This is the manager class to manage the game cycle.
 * date: 22/10/2022
 */
public class GameManager : Singleton<GameManager>
{
    #region Fields
    /*To get the TextAsset path: AssetDatabase.GetAssetPath(dataFiles.fistNameFile)*/
    [SerializeField]
    private Dataset dataFiles;
    [SerializeField]
    private int Difficulty;
    [SerializeField]
    private int daysForRound;
    [SerializeField]
    private int Rounds;
    [SerializeField]
    private int warningNumber;
    [SerializeField]
    private float dayDuration;
    [SerializeField]
    private bool isEndless;
    [SerializeField]
    private float maxScore;
    [SerializeField]
    private float startingScore;
    [SerializeField]
    private float minimumScore;
    [SerializeField]
    private float phoneLoadingTime;

    private ObjectiveGenerator objectiveGenerator;
    private ResumeHolder resumeHolder;
    private Objective currentObjective;
    private PlayerRound playerRound;

    [HideInInspector]
    public int resumeCount;
    [HideInInspector]
    public int dayCount;


    public Resume currentResume;
    public Action OnDayEndCallBack;
    public Action OnDayStartCallBack;
    public Action OnResumeAccepted;
    public Action OnNextRoundCallBack;
    public Action OnStartGameCallBack;
    public Action OnEndGameCallBack;
    public Action<bool> OnGameOverCallBack;
    #endregion

    #region Methods
    private void Awake()
    {
        objectiveGenerator = new ObjectiveGenerator(dataFiles, 1);
        currentObjective = objectiveGenerator.generateObjective();
        resumeHolder = new ResumeHolder(dataFiles, 1);
        playerRound = new PlayerRound();
        //playerRound.dayCount=0;
        //playerRound.dayUntilNextRound = daysForRound;
        //playerRound.currentScore = startingScore;
        //playerRound.maxScore = maxScore;
        //gameStartSetUp();
        //currentObjective = objectiveGenerator.generateObjective();
        //resumeHolder = new ResumeHolder(dataFiles, currentObjective, 1);
        //resumeHolder.generateResume(10);
        
        OnDayStartCallBack += manageDayStart;
        OnDayEndCallBack += manageDayEnd;
        OnStartGameCallBack += gameStartSetUp;
        OnGameOverCallBack += gameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentResume = resumeHolder.getNewResume();
        //OnDayStartCallBack.Invoke();
    }

    public float getPhoneLoadingTime()
    {
        return phoneLoadingTime + Difficulty;
    }
    private void gameStartSetUp()
    {
        //objectiveGenerator = new ObjectiveGenerator(dataFiles, 1);
        currentObjective = objectiveGenerator.generateObjective();
        //resumeHolder = new ResumeHolder(dataFiles, 1);
        //currentResume = await resumeHolder.generateResume(10);
        resumeHolder.generateResume(10);
        playerRound = new PlayerRound();
        playerRound.dayCount=0;
        playerRound.dayUntilNextRound = daysForRound;
        playerRound.currentScore = startingScore;
        playerRound.maxScore = maxScore;
        playerRound.maxRound = Rounds;
        playerRound.currentRound = 1;
        playerRound.maxWarning = warningNumber;
        StartCoroutine(startGameHandler());
        //OnDayStartCallBack.Invoke();
    }

    IEnumerator startGameHandler()
    {
        //resumeHolder.generateResume(10);
        currentResume = resumeHolder.getNewResume();
        while(currentResume.firstName == null)
        {
            currentResume = resumeHolder.getNewResume();
            yield return null;
        }
        Debug.Log("GameManager: Start day");
        OnDayStartCallBack.Invoke();
    }

    public Resume getNewResume(bool isAccepted)
    {
        playerRound.resumeCount++;
        if (isAccepted)
        {
            var returnMessage = "check Resume Score";
            ResumeHolder.calculateResumeScore(currentResume, currentObjective, returnMessage);
            playerRound.currentScore += currentResume.score;
            Debug.Log($"GM: Resume Score is {currentResume.score}");
            currentObjective.peopleHired++;
            if(currentObjective.peopleHired>= currentObjective.peopleToHire)
            {
                currentObjective.peopleHired = currentObjective.peopleToHire;
                playerRound.currentScore -= 0.5f;
            }
            OnResumeAccepted.Invoke();
        }
        currentResume = resumeHolder.getNewResume();
        return currentResume;
    }

    public Objective getObjective()
    {
        return currentObjective;
    }

    public PlayerRound getPlayerRound()
    {
        return playerRound;
    }

    public float getDayDuration()
    {
        return dayDuration;
    }

    public void gameOver(bool isWinning)
    {
        OnEndGameCallBack.Invoke();
    }

    private void manageDayStart()
    {
        //resumeHolder.generateResume(UnityEngine.Random.Range(10, 15) - Difficulty * 2);
        playerRound.dayCount++;
        playerRound.resumeCount = 0;
        playerRound.dayUntilNextRound--;
        if(playerRound.dayUntilNextRound < 0)
        {
            playerRound.dayUntilNextRound = daysForRound;
            playerRound.currentRound++;
            startNewRound();
        }
    }

    private void manageDayEnd()
    {
        //playerRound.resumeCount = 0;
        Debug.Log("GM: Day End! Generate Resume");
        resumeHolder.generateResume(UnityEngine.Random.Range(10, 15) - Difficulty * 2);
        if(playerRound.resumeCount == 0)
        {
            playerRound.currentScore -= 0.3f;
        }
        if(currentObjective.peopleHired == 0)
        {
            playerRound.currentScore -= 0.1f;
        }
        if(playerRound.currentScore < minimumScore)
        {
            playerRound.warning++;
        }
        playerRound.currentScore = Mathf.Clamp(playerRound.currentScore, 0, maxScore);
        currentResume = resumeHolder.getNewResume();
    }


    private void startNewRound()
    {
        currentObjective = objectiveGenerator.generateObjective();
        OnNextRoundCallBack.Invoke();
    }


    //TODO: initialize score, player round, update objective, generate new resume for next game
    private void gameEnd()
    {
        
    }
    #endregion
}

/*This is for the result and keep track of day round*/
public struct PlayerRound{
    public int dayCount;
    public float currentScore;
    public int resumeCount;
    public int dayUntilNextRound;
    public int currentRound;
    public int warning;
    public float maxScore;
    public int maxRound;
    public int maxWarning;
}