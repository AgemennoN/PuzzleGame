using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private static BackGround instance = null;


    public GameObject squarePrefab;
    public AudioSource audioSource;
    private float spawnRangeX = 5;
    private float startDelay = 1;
    private float spawnInternal = 3;

    public static BackGround Instance { get { return instance; } }
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
        InvokeRepeating("SpawnSquareRandomly", startDelay, spawnInternal);
        
        PlayMusic();
    }

    void SpawnSquareRandomly()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), -10, 0);
        Instantiate(squarePrefab, spawnPos, squarePrefab.transform.rotation);
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

}
