using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class PeletData
    {
        public Vector3 position;
        public GameObject prefab;
        public PeletItem itemData;
        public float respawnTimer;
        public bool isWaitingToRespawn;
    }

    private List<PeletData> peletPositions = new List<PeletData>();
    [SerializeField] private float respawnDelay = 5f;
    [SerializeField] private GameObject defaultPeletPrefab; // Add this field

    void Start()
    {
        StorePeletObjects();
    }

    void Update()
    {
        // Check and update respawn timers
        for (int i = 0; i < peletPositions.Count; i++)
        {
            if (peletPositions[i].isWaitingToRespawn)
            {
                peletPositions[i].respawnTimer -= Time.deltaTime;
                if (peletPositions[i].respawnTimer <= 0)
                {
                    RespawnPelet(i);
                    peletPositions[i].isWaitingToRespawn = false;
                }
            }
        }
    }

    public void StartRespawnTimer(Vector3 peletPosition)
    {
        for (int i = 0; i < peletPositions.Count; i++)
        {
            if (Vector3.Distance(peletPositions[i].position, peletPosition) < 0.1f)
            {
                peletPositions[i].respawnTimer = respawnDelay;
                peletPositions[i].isWaitingToRespawn = true;
                break;
            }
        }
    }

    void StorePeletObjects()
    {
        PeletObject[] peletObjects = FindObjectsOfType<PeletObject>();

        foreach (PeletObject pelet in peletObjects)
        {
            GameObject prefabToUse = null;

#if UNITY_EDITOR
            // Try to get the original prefab this object was created from
            prefabToUse = PrefabUtility.GetCorrespondingObjectFromSource(pelet.gameObject);

            // If that fails, try to get the nearest prefab parent
            if (prefabToUse == null)
            {
                GameObject prefabRoot = PrefabUtility.GetNearestPrefabInstanceRoot(pelet.gameObject);
                if (prefabRoot != null)
                {
                    prefabToUse = PrefabUtility.GetCorrespondingObjectFromSource(prefabRoot);
                }
            }
#endif

            // If we still don't have a prefab, create a template object
            if (prefabToUse == null)
            {
                // Create a template object that won't be destroyed
                GameObject template = Instantiate(pelet.gameObject);
                template.name = $"Template_{pelet.name}";
                template.SetActive(false); // Hide it
                DontDestroyOnLoad(template); // Keep it alive
                prefabToUse = template;

                Debug.Log($"Created template object for pelet at {pelet.transform.position}");
            }

            PeletData data = new PeletData
            {
                position = pelet.transform.position,
                prefab = prefabToUse,
                itemData = pelet.itemData,
                respawnTimer = 0f,
                isWaitingToRespawn = false
            };
            peletPositions.Add(data);
        }

        if (peletPositions.Count == 0)
        {
            Debug.LogWarning("No pelets found in scene!");
        }
        else
        {
            Debug.Log($"Stored {peletPositions.Count} pelets with their templates/prefabs");
        }
    }

    public void RespawnPelet(int index)
    {
        if (index >= 0 && index < peletPositions.Count)
        {
            if (peletPositions[index].prefab != null)
            {
                GameObject newPelet = Instantiate(peletPositions[index].prefab,
                                               peletPositions[index].position,
                                               Quaternion.identity);

                newPelet.SetActive(true); // Ensure the new pelet is active

                PeletObject peletComponent = newPelet.GetComponent<PeletObject>();
                if (peletComponent != null)
                {
                    peletComponent.itemData = peletPositions[index].itemData;
                }
            }
            else
            {
                Debug.LogError($"Prefab reference is null for pelet at index {index}");
            }
        }
    }

    public void RespawnAllPelets()
    {
        foreach (PeletData data in peletPositions)
        {
            GameObject newPelet = Instantiate(data.prefab,
                                           data.position,
                                           Quaternion.identity);

            newPelet.SetActive(true); // Ensure the new pelet is active

            PeletObject peletComponent = newPelet.GetComponent<PeletObject>();
            if (peletComponent != null)
            {
                peletComponent.itemData = data.itemData;
            }
        }
    }
}
