using UnityEditor;
using UnityEngine;

/*
 * DEPRECATED. DO NOT USE.
 * Tool used for prototype. Does not work with GitHub builds.
 */

public class RandomMap : EditorWindow   //TOOL BY FILIPPO CORTESE
{
    private Object spawnPrefab;
    private int team1SpawnPoints = 4;
    private int team2SpawnPoints = 4;
    private float mapWidth = 60f;
    private float mapLength = 60f;
    private SpawnDisplay display = null;
    private string spawntag = "SpawnPoint";

    private bool checkedForPoints = false;


    [MenuItem("Window/Map Tools[DEPRECATED]")]
    public static void OpenWindow()
    {
        GetWindow<RandomMap>("Map Tools[DEPRECATED]");
    }

    void OnGUI() 
    {

        if (display == null)
        {
            display = GameObject.FindGameObjectWithTag("MapDef").GetComponent<SpawnDisplay>();
        }

        GUILayout.Label("Random Map Setup", EditorStyles.largeLabel);
        GUILayout.Label("This is a simple custom tool to generate random spawn point without having to manually insert all the empty game objects." + 
                        " This tool is limited to in-engine use and the script has been placed in the \"Editor\" folder so that it is excluded from all builds.", EditorStyles.helpBox);
        EditorGUILayout.Space();

        //

        spawntag = EditorGUILayout.TextField("Spawn point Tag :", spawntag);
        EditorGUILayout.Space();

        //

        GUILayout.Label("Spawn Area", EditorStyles.boldLabel);
        mapWidth = EditorGUILayout.Slider("Map Width", mapWidth, 1f, 150f);
        mapLength = EditorGUILayout.Slider("Map Length", mapLength, 1f, 150f);
        display.MapLength = mapLength;
        display.MapWidth = mapWidth;
        EditorGUILayout.Space();

        //

        GUILayout.Label("Spawn Controls", EditorStyles.boldLabel);
        team1SpawnPoints = EditorGUILayout.IntSlider("Team 1 spawn points", team1SpawnPoints, 1, 10);
        team2SpawnPoints = EditorGUILayout.IntSlider("Team 2 spawn points", team2SpawnPoints, 1, 10);
        EditorGUILayout.Space();

        //

        if (GUILayout.Button("Find and Remove Current Spawn Points"))
        {
            checkedForPoints = true;
            GameObject[] spawns;
            GameObject[] objects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            int spawnCount = 0;
            foreach (GameObject obj in objects)
            {
                if (obj.layer == 9)
                {
                    spawnCount++;
                }
            }
            spawns = new GameObject[spawnCount];
            spawnCount = 0;
            foreach (GameObject obj in objects)
            {
                if (obj.layer == 9)
                {
                    spawns[spawnCount] = obj;
                    spawnCount++;
                }
            }

            foreach (GameObject obj in spawns)
            {
                DestroyImmediate(obj);
            }
        }

        //

        if (!checkedForPoints) { GUI.enabled = false; } else { GUI.enabled = true; }

        if (GUILayout.Button("Randomize Spawn Points"))
        {
            for (int i = 0; i < team1SpawnPoints; i++)
            {
                Vector3 spawnPosition = GenerateRandomPosition();
                NewSpawnPoint(spawnPosition, "1");
            }
            for (int i = 0; i < team2SpawnPoints; i++)
            {
                Vector3 spawnPosition = GenerateRandomPosition();
                NewSpawnPoint(spawnPosition, "2");
            }
            checkedForPoints = false;
        }

        //

    }

    private Vector3 GenerateRandomPosition()
    {
        float ranX, ranZ;
        ranX = Random.Range((-mapWidth / 2 ) + 1, (mapWidth / 2) - 1);
        ranZ = Random.Range((-mapLength / 2) + 1, (mapLength / 2) - 1);

        return new Vector3(ranX, display.yPos+ 0.05f, ranZ);
    }

    private void NewSpawnPoint(Vector3 pos, string teamID)
    {
        spawnPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SpawnPoint.prefab", typeof(GameObject));   //edit path as needed
        GameObject point = (GameObject)Object.Instantiate(spawnPrefab, pos, Quaternion.identity);

        point.tag = "Team" + teamID;
        point.layer = 9;

        RaycastHit hit;
        if ((Physics.Raycast(point.transform.position, Vector3.down, out hit, 3f) && hit.transform.tag != "Ground" || !(Physics.Raycast(point.transform.position, Vector3.down, out hit, 3f))))
        {
            DestroyImmediate(point);
            NewSpawnPoint(GenerateRandomPosition(), teamID);
            return;
        }
    }
}
