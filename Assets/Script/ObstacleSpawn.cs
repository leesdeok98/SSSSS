using System;
using System.Collections;
using UnityEngine;


[Serializable]
public class ObstaclePatten
{
    public int spawnTime;
    public int type;
    public int imageType;

    public ObstaclePatten(int time,int obstacleType,int imageType)
    {
        spawnTime=time;
        type = obstacleType;
        this.imageType = imageType;
    }
}

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // 0: 아래용 (Obstacle), 1: 위용 (Obstacle1)
    public Transform[] spawnPoints;       // 0: 위, 1: 아래
    public ObstaclePatten[] obstaclePattens; //오브젝트 패턴 정보
    int obstaclePattenCurrentIndex;

    public float spawnInterval = 5f;      // 총 간격
    public float spawnDuration = 1f;      // 생성 시간 (1초만 생성)

    void Start()
    {
        transform.position = new Vector3(1,5,1);
        StartCoroutine(SpawnObstacleRoutine());

    }

    // IEnumerator SpawnObstacleRoutine()
    // {
    //     while (true)
    //     {
    //         int trapType = UnityEngine.Random.Range(1, 4); // 1 또는 2

    //         switch (trapType)
    //         {
    //             case 1:
    //                 SpawnObstacle(0); // 아래쪽 장애물
    //                 break;
    //             case 2:
    //                 SpawnObstacle(1);   // 위쪽 장애물
    //                 break;
    //             case 3:
    //                 SpawnObstacleUp2();   // 위쪽2 장애물
    //                 break;
    //             default:
    //                 Debug.LogWarning("Invalid trap type");
    //                 break;
    //         }

    //         yield return new WaitForSeconds(spawnDuration);                  // 생성 시간
    //         yield return new WaitForSeconds(spawnInterval - spawnDuration); // 대기 시간
    //     }
    // }

    IEnumerator SpawnObstacleRoutine()
    {
        ObstaclePatten currentPatten;
        while(true)
        {
            if(obstaclePattenCurrentIndex >= obstaclePattens.Length)
                yield break;
            currentPatten = obstaclePattens[obstaclePattenCurrentIndex];
            yield return new WaitForSeconds(currentPatten.spawnTime);
            SpawnObstacle(currentPatten.type, currentPatten.imageType);
        }
    }

    /// <summary>
    /// type = 0 : 아래 장애물 생성, type = 1 : 위 장애물 생성
    /// </summary>
    /// <param name="type"></param>
    void SpawnObstacle(int type, int imageType)
    {
        if(type != 0 && type != 1)
        {
            Debug.LogError("생성하는 장애물의 타입 값의 범위가 존재하지 않습니다");
            return;
        }
        GameObject newObstacle = Instantiate(obstaclePrefabs[type], spawnPoints[1-type].position, Quaternion.identity);
        Obstacle obs = newObstacle.GetComponent<Obstacle>();
        obs.Init(imageType);
        obstaclePattenCurrentIndex++;
    }
    void SpawnObstacleUp2()
{
    Debug.Log("삭제");
    StartCoroutine(SpawnWithDelay());
}

IEnumerator SpawnWithDelay()
{
    // 첫 번째 장애물 생성
    Instantiate(obstaclePrefabs[1], spawnPoints[0].position, Quaternion.identity);
    Debug.Log("Spawned Obstacle (First)");

    // 1초 간격 대기
    yield return new WaitForSeconds(0.2f);

    // 두 번째 장애물 생성
    Instantiate(obstaclePrefabs[1], spawnPoints[0].position, Quaternion.identity);
    Debug.Log("Spawned Obstacle (Second)");
}
}
