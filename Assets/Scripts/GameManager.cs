using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Sprite> spriteList; // Lista de sprites
    public SpriteRenderer targetSpriteRenderer; // Referencia al SpriteRenderer del objeto vac�o
    private int currentSpriteIndex = 0; // �ndice del sprite actual
    public string choosenKid;


    public List<string> spirits = new List<string> { };
    public int currentMap;  // Variable para almacenar el mapa actual
    public List<string> bosses = new List<string> { "Ranamon", "Mercurymon", "Lucemon" };
    public string currentBoss = "Ranamon";
    public string currentSpirit;

    void Awake()
    {
        // Implementar el patr�n Singleton para asegurarse de que solo haya una instancia de GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // No destruir al cambiar de escena
        }
        else
        {
            Destroy(gameObject);  // Si ya hay un GameManager, destruir este
        }
    }

    private void Start()
    {
        // Aseg�rate de que el SpriteRenderer est� asignado
        if (targetSpriteRenderer == null)
        {
            Debug.LogError("No se ha asignado un SpriteRenderer al GameManager.");
        }

        
        // Mostrar el primer sprite al inicio
        UpdateSprite();
    }

    public void setKidSpirit()
    {
        if (choosenKid.Contains("takuya"))
        {
            spirits.Add("Agunimon");
            currentSpirit = "Agunimon";
        }
        else if (choosenKid.Contains("jp"))
        {
            spirits.Add("Beetlemon");
            currentSpirit = "Beetlemon";
        }
        else if (choosenKid.Contains("koji"))
        {
            spirits.Add("Lobomon");
            currentSpirit = "Lobomon";
        }
    }

    // M�todo para actualizar el sprite que se muestra
    private void UpdateSprite()
    {
        if (spriteList.Count > 0)
        {
            targetSpriteRenderer.sprite = spriteList[currentSpriteIndex];
        }
    }

    // M�todo para cambiar al siguiente sprite
    public void NextSprite()
    {
        if (spriteList.Count > 0)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % spriteList.Count;
            UpdateSprite();
        }
    }

    // M�todo para cambiar al sprite anterior
    public void PreviousSprite()
    {
        if (spriteList.Count > 0)
        {
            currentSpriteIndex = (currentSpriteIndex - 1 + spriteList.Count) % spriteList.Count;
            UpdateSprite();
        }
    }

    public void ChooseChoosenKid()
    {
        choosenKid = targetSpriteRenderer.sprite.name;
        StartCoroutine(EnterDigitalWorld(1f)); // Espera 1 segundo y cambia de escena
    }

    private IEnumerator EnterDigitalWorld(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainScene"); // Aseg�rate de que el nombre de la escena es correcto
        setKidSpirit();
    }

    // M�todo para obtener el string asociado al sprite actual
    public string GetCurrentSpriteName()
    {
        if (spriteList.Count > 0)
        {
            return spriteList[currentSpriteIndex].name;
        }
        return "No hay sprites en la lista";
    }

    // M�todo para obtener el mapa actual
    public int GetCurrentMap()
    {
        return currentMap;
    }

    // M�todo para establecer el mapa actual (cuando cambies de mapa)
    public void SetCurrentMap(int mapIndex)
    {
        currentMap = mapIndex;

    }

    public void AddTextToList(string newText)
    {
        if (!spirits.Contains(newText))
        {
            spirits.Add(newText);
        }
    }
}