using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnData
{
    public Transform planet;
    public Vector3 origin = new Vector3();
    public float planetSpeed = 0f;
}
public class PlanetSpawner : MonoBehaviour
{
    public float SpawnHeightVarience = 5f;
    public GameObject PlanetBase;
    public Mesh[] PlanetMeshes = new Mesh[0];
    public float KillDistance = 25f;
    public float PlanetSpawnTime_Max = 10f;
    public float PlanetSpawnTime_Min = 2f;
    public float PlanetSpeed_Max = 5f;
    public float PlanetSpeed_Min = 1f;
    public float PlanetScale_Max = 1f;
    public float PlanetScale_Min = .25f;
    


    List<PlanetSpawnData> planets = new List<PlanetSpawnData>();
    float currentSpawnWait = 0f;
    float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnWait = Random.Range(PlanetSpawnTime_Min,PlanetSpawnTime_Max);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer+=Time.deltaTime;
        if(spawnTimer>=currentSpawnWait) SpawnNewPlanet();

        ManagePlanets();
    }

    public void SpawnNewPlanet()
    {
        spawnTimer = 0f;
        currentSpawnWait = Random.Range(PlanetSpawnTime_Min,PlanetSpawnTime_Max);

        PlanetSpawnData newData = new PlanetSpawnData();
        GameObject newPlanet = (GameObject)Instantiate(PlanetBase,GetNewSpawnLoc(),PlanetBase.transform.rotation) as GameObject;
        newPlanet.transform.parent = transform;

        newData.planet = newPlanet.transform;
        newData.planet.transform.Rotate(Vector3.up,Random.Range(0f,360f),Space.World);
        newData.origin = newPlanet.transform.position;
        newData.planetSpeed = Random.Range(PlanetSpeed_Min,PlanetSpeed_Max);   
        planets.Add(newData);

        newPlanet.GetComponent<MeshFilter>().mesh = PlanetMeshes[Random.Range(0,PlanetMeshes.Length)];
        newPlanet.transform.localScale = Vector3.one * Random.Range(PlanetScale_Min,PlanetScale_Max);
    }

    public void ManagePlanets()
    {
        for(int i=0; i<planets.Count; i++)
        {
            planets[i].planet.position += Time.deltaTime*planets[i].planetSpeed*Vector3.left;
            if(Vector3.Distance(planets[i].planet.position,planets[i].origin)>=KillDistance)
            {
                Destroy(planets[i].planet.gameObject);
                planets.RemoveAt(i);
                i--;
            }
        }
    }

    public Vector3 GetNewSpawnLoc()
    {
        return new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + Random.Range(-SpawnHeightVarience,SpawnHeightVarience)
        );
    }
}
