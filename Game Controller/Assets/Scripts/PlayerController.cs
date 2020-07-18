using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource running;
    public AudioSource jumping;

    //Obstacle Variable
    public GameObject obstacleGenerator;
    public float ObstacleGeneratingTimeDecrease = 15f;
    //HighScore Variable
    public HighScoreCollector highScoreCollector;
    public GameObject scoreDisplay;

    // Ball Variables
    private float BodyHeight = 1f;
    public float JumpHeight = 2f;
    private Rigidbody _body;
    public float Speed = 4f;
    public float GameOverSpeedThreshold = 2f;
    public float MaxTimeUnderGameOverSpeed = 1f;
    public float MaxSpeedThreshold = 20f;
    public float SpeedIncreasePerTimeLength = 0.2f;
    public float SpeedupTimeLength = 10f;
    public float EPS = 0.01f;

    // Lane Variables
    public float LanesLength = 12f;
    public int LaneNum = 3;
    public float LaneChangeTime = 0.05f;
    public int CurLane = 2;
    public float UnderSpeedTimer = 3000f;

    float TimeCnt = 0;
    bool inMovement = false;
    float TimeSinceLastSpeedup = 0;
    float starting_y;
    Vector3 shift;
    bool[] LevelsReached = new bool[11];

    void Start()
    {
        _body = gameObject.GetComponent<Rigidbody>();
        starting_y = _body.position.y;
        for (int i = 0; i <= 10; i++)
            LevelsReached[i] = false;
    }


    void Update()
    {
        CheckLevelUp();
        SpeedUpPerNSeconds();
        if (inMovement == false && (_body.position.y <= starting_y  + EPS)  && (starting_y - EPS <= _body.position.y))
        {
            if (Input.GetButtonDown("Jump"))
                Jump();
            if (Input.GetButtonDown("Left"))
                MoveLeft();
            if (Input.GetButtonDown("Right"))
                MoveRight();
        }
        MoveForward();
        GoToDestination();
        GameOver();
    }

    void CheckLevelUp() {
        int curScore = scoreDisplay.GetComponent<ScoreScript>().points;
        if (curScore < 11000 && curScore > 999 && !LevelsReached[curScore / 1000]) {
            LevelsReached[curScore / 1000] = true;
            obstacleGenerator.GetComponent<ObstacleGeneratorEfficient>().ObjectGeneratingTime -= ObstacleGeneratingTimeDecrease;
        }
    }

    void SpeedUpPerNSeconds()
    {
        TimeSinceLastSpeedup += Time.deltaTime;
        if (TimeSinceLastSpeedup >= SpeedupTimeLength)
        {
            TimeSinceLastSpeedup = 0;
            Speed = Mathf.Min(Speed + SpeedIncreasePerTimeLength, MaxSpeedThreshold);
        }
    }

    void Jump()
    {
        jumping.Play();
        _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    //Set a vector to move to the left
    void MoveLeft()
    {
        if (CurLane != 1)
        {
            inMovement = true;
            shift = new Vector3(-LanesLength / (LaneNum + 1), 0, 0);
            CurLane--;
        }
    }

    //Set a vector to move to the right
    void MoveRight()
    {
        if (CurLane != LaneNum)
        {
            inMovement = true;
            shift = new Vector3(LanesLength / (LaneNum + 1), 0, 0);
            CurLane++;
        }
    }

    //Keep moving forward
    void MoveForward()
    {
        _body.MovePosition(_body.position + (Time.deltaTime * new Vector3(0, 0, Speed)));
    }
    void GameOver()
    {
        if (Speed <= GameOverSpeedThreshold)
        {
            if (UnderSpeedTimer <= MaxTimeUnderGameOverSpeed)
            {

                highScoreCollector.UpdatePlayerPref(scoreDisplay.GetComponent<ScoreScript>().points);
                SceneManager.LoadScene(1);
            }
            UnderSpeedTimer = UnderSpeedTimer - Time.deltaTime * 1000f;
        }
    }

    //Apply the vectors of MoveLeft and MoveRight on the game object _body
    void GoToDestination()
    {
        if (inMovement)
        {
            running.Play();
            _body.MovePosition(_body.position + (Mathf.Min(LaneChangeTime - TimeCnt, Time.deltaTime) * shift / LaneChangeTime));
            TimeCnt += Time.deltaTime;
            if (TimeCnt >= LaneChangeTime)
            {
                inMovement = false;
                shift = Vector3.zero;
                TimeCnt = 0;
            }
        }
    }
}
