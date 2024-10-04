using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private float xLimit;

    [SerializeField]
    private float[] xPositions;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private Wave[] wave;

    private float currentTime;

    List<float> remainingPositions = new List<float>();
    private int waveIndex;
    private float xPos = 0;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        remainingPositions.AddRange(xPositions);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.StartMoving == true && MenuManager.instance.gameOver == false)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                SelectWave();
            }
            
        }
        
    }

    void SpawnEnemy(float xPos)
    {
        int r = Random.Range(0, 3); //3 types of enemies 
        string enemyName = "";
        if (r == 0)
        {
            enemyName = "Enemy1";
        }
        else if (r == 2)
        {
            enemyName = "Enemy2";
        }
        else if (r == 3)
        {
            enemyName = "Enemy3";
        }
        GameObject enemyObj = Instantiate(enemyPrefabs[r], new Vector3(xPos, transform.position.y, 0), Quaternion.identity);
        GameObject Enemy = ObjectPooling.instance.GetPooledObject(enemyName);
        //Enemy.transform.position = new Vector3(xPos, transform.position.y);
        enemyObj.transform.position = new Vector3(xPos, transform.position.y);
        //Enemy.SetActive(true);
        enemyObj.SetActive(true);

    }

    void SelectWave()
    {
        remainingPositions = new List<float>();
        remainingPositions.AddRange(xPositions);

        waveIndex = Random.Range(0, wave.Length);

        currentTime = wave[waveIndex].delayTime;

        if (wave[waveIndex].spawnAmount == 1)
        {
            xPos = Random.Range(-xLimit, xLimit);
        }
        else if (wave[waveIndex].spawnAmount > 1)
        {
            rand = Random.Range(0, remainingPositions.Count);
            xPos = remainingPositions[rand];
            remainingPositions.RemoveAt(rand);
        }

        for (int i = 0; i < wave[waveIndex].spawnAmount; i++)
        {
            if (MenuManager.instance.gameOver == false)
            {
                SpawnEnemy(xPos);
                rand = Random.Range(0, remainingPositions.Count);
                xPos = remainingPositions[rand];
                remainingPositions.RemoveAt(rand);


            }
            
           
        }
    }
}

[System.Serializable]
public class Wave
{
    public float delayTime;
    public float spawnAmount;
}
