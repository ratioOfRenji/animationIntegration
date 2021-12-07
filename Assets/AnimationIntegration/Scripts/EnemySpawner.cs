
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    [SerializeField] private Transform lovestX;
    [SerializeField] private Transform highestX;
    [SerializeField] private Transform lovestZ;
    [SerializeField] private Transform highestZ;
    [SerializeField] private float offsetFromBorder = 0.3f;
    [SerializeField] private GameObject enemyPrefab;

    public delegate void findEnemy();
    public  event findEnemy _findEnemy;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        //задает случайную позицию для нового врага
       Vector3 enemyPosition = new Vector3(Random.Range(lovestX.position.x + offsetFromBorder, highestX.position.x - offsetFromBorder), 0, Random.Range(lovestZ.position.z + offsetFromBorder, highestZ.position.z - offsetFromBorder));
        // создает нового врага в указаной точке
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = enemyPosition; 
        // вызов эвента, который вызывает у игрока метод поиска врага
        _findEnemy();
    }

}
