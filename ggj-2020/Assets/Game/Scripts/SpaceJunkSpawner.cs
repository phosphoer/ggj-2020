using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawnData
{
    public Transform Junk_T;
    public Vector3 origin = new Vector3();
    public float Speed = 0f;
    public float SpinRate = 0f;
}

[System.Serializable]
public class JunkSpawnStats
{
    public GameObject Prefab;
    public float Speed_Max = 5f;
    public float Speed_Min = 1f;
    public float Scale_Max = 1f;
    public float Scale_Min = 1f;
    public float Spin_Max = 1f;
    public float Spin_Min = -1f;

}

public class SpaceJunkSpawner : MonoBehaviour
{
    public float SpawnHeightVarience = 5f;
    public float KillDistance = 100f;
    public float ZVarience = 2f;
    public float SpawnTime_Max = 30f;
    public float SpawnTime_Min = 10f;
    public JunkSpawnStats[] JunkStats = new JunkSpawnStats[0];


    List<JunkSpawnData> junk = new List<JunkSpawnData>();
    float currentSpawnWait = 0f;
    float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnWait = Random.Range(SpawnTime_Min,SpawnTime_Max);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.Instance.CurrentStage == GameStateManager.GameStage.Game)
        {
            spawnTimer+=Time.deltaTime;
            if(spawnTimer>=currentSpawnWait) SpawnNewJunk();

            ManageJunk();
        }
    }

    public void SpawnNewJunk()
    {
        spawnTimer = 0f;
        currentSpawnWait = Random.Range(SpawnTime_Min,SpawnTime_Max);

        //Create the struct that'll house our junk's data to manage it later..
        JunkSpawnData newData = new JunkSpawnData();

        //Spawn the physical object
        int selection = Random.Range(0,JunkStats.Length);
        GameObject newJunk = (GameObject)Instantiate(JunkStats[selection].Prefab,GetNewSpawnLoc(),JunkStats[selection].Prefab.transform.rotation) as GameObject;
        newJunk.transform.parent = transform;

        newData.Junk_T = newJunk.transform;
        newData.origin = newJunk.transform.position;
        
        newData.Speed = Random.Range(JunkStats[selection].Speed_Min,JunkStats[selection].Speed_Max); 
        newData.SpinRate = Random.Range(JunkStats[selection].Spin_Min,JunkStats[selection].Spin_Max);

        junk.Add(newData);

        newJunk.transform.localScale = Vector3.one * Random.Range(JunkStats[selection].Scale_Min,JunkStats[selection].Scale_Max);
    }

    public void ManageJunk()
    {
        for(int i=0; i<junk.Count; i++)
        {
            junk[i].Junk_T.position += Time.deltaTime*junk[i].Speed*Vector3.left;
            junk[i].Junk_T.Rotate(Vector3.up,junk[i].SpinRate*Time.deltaTime,Space.World);
            if(Vector3.Distance(junk[i].Junk_T.position,junk[i].origin)>=KillDistance)
            {
                Destroy(junk[i].Junk_T.gameObject);
                junk.RemoveAt(i);
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
