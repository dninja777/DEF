using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    next, play, gameover, win
}

public class GameController : Singleton<GameController> {


    [SerializeField] private int totalWaves = 10;                   //private type of int for totalWaves = 10
    [SerializeField] private Text totalMoneyLbl;                    //Private type of Text named totalMoneyLbl UI Text
    [SerializeField] private Text currentWaveLbl;                   //Private type of Text named currentWaveLbl UI Text
    [SerializeField] private Text totalEscapeLbl;                   //Private type of Text named totalEscapeLbl UI Text

    [SerializeField] private Text playBtnLbl;                       //Private type of Text named playBtnLbl UI Text
    [SerializeField] private Button playBtn;                        //Private type of Button named platBtn UI Button


    private int waveNumber = 0;                                     //Private type of int named waveNumber = 0
    private int totalMoney = 10;                                    //Private type of int named totalMoney = 10
    private int totalEscaped = 0;                                   //Private type of int named totalEscaped = 0
    private int roundEscaped = 0;                                   //Private type of int named roundEscaped = 0
    private int totalKilled = 0;                                    //Private type of int named totalKilled = 0
    private int whichEnemiesToSpawn = 0;                            //Private type of int named whichEnmiesoSpawn = 0
    private GameStatus currentState = GameStatus.play;              //Private type of GameStatus named currentState = GmeStatus.plaaaaaaa;


    private AudioSource audioSource;


    public int TotalEscaped                                         
    {
        get { return totalEscaped; }                                //public getter to return the totalEscaped
        set { totalEscaped = value; }                               //Set totalEscaped value
    }

    public int RoundEscaped
    {
        get { return roundEscaped; }                               //public getter to return roundEscaped
        set { roundEscaped = value; }
    }

    public int TotalKilled
    {
        get { return totalKilled; }
        set { totalKilled = value; }
    }

    public int TotalMoney
    {
        get { return totalMoney; }
        set { totalMoney = value;
            totalMoneyLbl.text = totalMoney.ToString();
        }
    }

    public AudioSource AudioSource
    {
        get { return audioSource; }
    }

    
    

    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private int totalEnemies = 10;
    [SerializeField]
    private int enemiesPerSpawn;

    public List<Enemy> EnemyList = new List<Enemy>();
    
    

    
    const float spawnDelay = 0.5f;

    


    // Use this for initialization
    void Start () {
        playBtn.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        ShowMenu();
	}

    private void Update()
    {
        HandelEscape();
    }




    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    GameObject newEnemy = Instantiate(enemies[Random.Range(0, 2)]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    
                }
                yield return new WaitForSeconds(spawnDelay);
                StartCoroutine(Spawn());
            }
        }

    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies()
    {
        foreach(Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();

    }

   
   

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void SubtractMoney(int amount)
    {
        TotalMoney -= amount;
    }

    public void IsWaveOver()
    {
        totalEscapeLbl.text = "Escaped: " + TotalEscaped + "/10";
        if((RoundEscaped + TotalKilled) == totalEnemies)
        {
            SetCurrentGameState();
            ShowMenu();

        }
    }

    public void SetCurrentGameState()
    {
        if(TotalEscaped >= 10)
        {
            currentState = GameStatus.gameover;
        } else if(waveNumber == 0 && (TotalKilled + RoundEscaped) == 0)
        {
            currentState = GameStatus.play;
        } else if (waveNumber >= totalWaves)
        {
            currentState = GameStatus.win;
        }
        else
        {
            currentState = GameStatus.next;
        }
    }

    public void ShowMenu()
    {
        switch (currentState)
        {
            case GameStatus.gameover:
                playBtnLbl.text = "Play Again";
                //Game over SFX
                GameController.Shared.AudioSource.PlayOneShot(SoundController.Shared.GameOver);
                break;

            case GameStatus.next:
                playBtnLbl.text = "Next Wave";
                break;
            case GameStatus.play:
                playBtnLbl.text = "Start Game";
                break;
            case GameStatus.win:
                playBtnLbl.text = "You Have Won Play Again?";
                    break;
        }
        playBtn.gameObject.SetActive(true);
    }
  
    public void PlayBtnPressed()
    {
        switch (currentState)
        {
            case GameStatus.next:
                waveNumber += 1;
                totalEnemies += waveNumber;
                break;
            default:
                totalEnemies = 3;
                TotalEscaped = 0;
                totalMoney = 10;
                TowerController.Shared.DestroyAllTowers();
                TowerController.Shared.RenametagsBuildSite();
                totalMoneyLbl.text = TotalMoney.ToString();
                totalEscapeLbl.text = "Escaped: " + TotalEscaped.ToString();
                audioSource.PlayOneShot(SoundController.Shared.NewGame);
                break;
        }

        DestroyAllEnemies();
        totalKilled = 0;
        RoundEscaped = 0;
        currentWaveLbl.text = "Wave " + (waveNumber + 1);
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }

    private void HandelEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerController.Shared.DisableDragSprite();
            TowerController.Shared.towerBtnPressed = null;
        }
    }
    
}
