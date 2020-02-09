using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
	ShipHealthComponent shipHealth;
	
	public List<EnemySpawner> enemySpawners;
	
	public float easyTimeMin;
	public float easyTimeMax;
	public float easyShipHealth;
	public float medTimeMin;
	public float medTimeMax;
	public float medShipHealth;
	public float hardTimeMin;
	public float hardTimeMax;
	public float hardShipHealth;

    void Start()
    {
		shipHealth = GetComponent<ShipHealthComponent>();
    }
	
	public void SetEasy ()
	{
		print("EASY PEASEY!");
		shipHealth.TotalShipHealth = easyShipHealth;
		foreach (EnemySpawner x in enemySpawners)
		{
			x.SpawnTimerMin = easyTimeMin;
			x.SpawnTimerMax = easyTimeMax;
			x.RandomizeSpawnTime();
		}
	}
	
	public void SetMed ()
	{
		print("NOT TOO ROUGH!");
		shipHealth.TotalShipHealth = medShipHealth;
		foreach (EnemySpawner x in enemySpawners)
		{
			x.SpawnTimerMin = medTimeMin;
			x.SpawnTimerMax = medTimeMax;
			x.RandomizeSpawnTime();
		}
	}
	
	public void SetHard ()
	{
		print("HURT ME PLENTY!!!");
		shipHealth.TotalShipHealth = hardShipHealth;
		foreach (EnemySpawner x in enemySpawners)
		{
			x.SpawnTimerMin = hardTimeMin;
			x.SpawnTimerMax = hardTimeMax;
			x.RandomizeSpawnTime();
		}
	}

}
