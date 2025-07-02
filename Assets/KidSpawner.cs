using UnityEngine;

public class KidSpawner : MonoBehaviour
{
    public GameObject[] kidPrefabs; // Array de prefabs (asegúrate de que el 1° es Takuya y el 2° es JP)
    public Transform spawnPoint; // Punto donde se instanciará el prefab


    void Start()
    {
        if (GameManager.instance != null) // Verifica que el GameManager existe
        {
            string choosenKid = GameManager.instance.choosenKid;
            SpawnKid(choosenKid);
        }
        else
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }
    }

    void SpawnKid(string kidName)
    {
        GameObject prefabToSpawn = null;

        if (kidPrefabs.Length == 0) // Verifica que haya prefabs en el array
        {
            Debug.LogError("El array de kidPrefabs está vacío.");
            return;
        }

        if (kidName.Contains("takuya") && kidPrefabs.Length > 0)
        {
            prefabToSpawn = kidPrefabs[0]; // Primer prefab (Takuya)
        }
        else if (kidName.Contains("jp") && kidPrefabs.Length > 1)
        {
            prefabToSpawn = kidPrefabs[1]; // Segundo prefab (JP)
        }
        else if (kidName.Contains("koji") && kidPrefabs.Length > 1)
        {
            prefabToSpawn = kidPrefabs[2]; // Segundo prefab (Koji)
        }

        // Instancia el prefab si se encontró
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"No se encontró un prefab para: {kidName}");
        }
    }
}