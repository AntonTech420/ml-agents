using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHandler : MonoBehaviour
{
    [SerializeField] Pipe pipePrefab;
    [SerializeField] float gapSize = 4f;
    [SerializeField] float secondsBetweenSpawns = 2f;
    private float spawnTimer;
    private readonly List<Pipe> pipes = new List<Pipe>();

    private void Update()
    {
        RemoveOldPipes();

        SpawnNewPipes();
    }

    public void ResetPipes(){
        foreach (var pipe in pipes){
            Destroy(pipe.gameObject);
        }
        pipes.Clear();
        
        spawnTimer = 0f;
    }
    void RemoveOldPipes(){
        for (int i = pipes.Count - 1; i >= 0; i--){
            if (pipes[i].transform.position.x < -15f){
                Destroy(pipes[i].gameObject);

                pipes.RemoveAt(i);
            }
        }
    }
    void SpawnNewPipes(){
        spawnTimer -= Time.deltaTime;

        if (spawnTimer > 0) {return;}

        Pipe topPipe = Instantiate(pipePrefab, transform.position, Quaternion.Euler(0f, 0f, 180f));
        Pipe bottomPipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);

        float centerHeight = UnityEngine.Random.Range(-1.5f, 4);

        topPipe.transform.Translate(Vector3.up * (centerHeight + (gapSize * 1.25f)), Space.World);
        bottomPipe.transform.Translate(Vector3.up * (centerHeight - (gapSize * 1.25f)), Space.World);

        pipes.Add(topPipe);
        pipes.Add(bottomPipe);

        spawnTimer = secondsBetweenSpawns;
    }
}
