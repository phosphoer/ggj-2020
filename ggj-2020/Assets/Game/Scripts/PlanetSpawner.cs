using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnData
{
    public Transform planet;
    public Vector3 origin = new Vector3();
    public float planetSpeed = 0f;
    public float planetSpinRate = 0f;
}

[System.Serializable]
public class PlanetSpawnStats
{
    public Mesh PlanetCard;
    public float PlanetSpeed_Max = 5f;
    public float PlanetSpeed_Min = 1f;
    public float PlanetScale_Max = 1f;
    public float PlanetScale_Min = .25f;
    public float PlanetSpin_Max = 1f;
    public float PlanetSpin_Min = -1f;
    public bool RandomColor = false;
    public Color[] ColorPicker = new Color[0];

}

public class PlanetSpawner : MonoBehaviour
{
    public float SpawnHeightVarience = 5f;
    public GameObject PlanetBase;
    //public Mesh[] PlanetMeshes = new Mesh[0];
    public float KillDistance = 25f;
    public float ZVarience = 2f;
    public float PlanetSpawnTime_Max = 10f;
    public float PlanetSpawnTime_Min = 2f;
    public PlanetSpawnStats[] PlanetStats = new PlanetSpawnStats[0];


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

        int planetChoice = Random.Range(0,PlanetStats.Length);

        newData.planet = newPlanet.transform;
        newData.planet.transform.Rotate(Vector3.up,Random.Range(0f,360f),Space.World);
        newData.origin = newPlanet.transform.position;
        
        newData.planetSpeed = Random.Range(PlanetStats[planetChoice].PlanetSpeed_Min,PlanetStats[planetChoice].PlanetSpeed_Max); 
        newData.planetSpinRate = Random.Range(PlanetStats[planetChoice].PlanetSpin_Min,PlanetStats[planetChoice].PlanetSpin_Max);

        planets.Add(newData);

        MeshFilter newPlanetMesh = newPlanet.GetComponent<MeshFilter>();
        newPlanetMesh.mesh = PlanetStats[planetChoice].PlanetCard;

        if(PlanetStats[planetChoice].RandomColor)
        {
            int pickedColor = Random.Range(0,PlanetStats[planetChoice].ColorPicker.Length);
            Color[] newColors = new Color[newPlanetMesh.mesh.vertexCount];
            for(int i=0; i<newColors.Length; i++)
            {
                newColors[i] = PlanetStats[planetChoice].ColorPicker[pickedColor];
            }
            newPlanetMesh.mesh.colors = newColors;
            Debug.Log("Set new planet color to " + PlanetStats[planetChoice].ColorPicker[pickedColor]);
        }
        newPlanet.transform.localScale = Vector3.one * Random.Range(PlanetStats[planetChoice].PlanetScale_Min,PlanetStats[planetChoice].PlanetScale_Max);
    }

    public void ManagePlanets()
    {
        for(int i=0; i<planets.Count; i++)
        {
            planets[i].planet.position += Time.deltaTime*planets[i].planetSpeed*Vector3.left;
            planets[i].planet.Rotate(Vector3.up,planets[i].planetSpinRate*Time.deltaTime);
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
            transform.position.y+Random.Range(-ZVarience,ZVarience),
            transform.position.z + Random.Range(-SpawnHeightVarience,SpawnHeightVarience)
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position,new Vector3(0f,ZVarience*2f,SpawnHeightVarience*2f));
    }
}
