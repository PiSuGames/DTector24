using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneManager : MonoBehaviour
{
    public GameObject zoneSelectionPanel;  // Panel de selecci�n de zonas
    public Text selectedZoneText;  // Texto donde se mostrar� la zona seleccionada
    private string selectedZone;  // Variable para almacenar la zona seleccionada
    public MenuManager menuManager;
    public GameManager gameManager;
    public int zoneNumber = 0;

    public GameObject botonZona1;
    public GameObject botonZona2;
    public GameObject botonZona3;

    // Diccionario para almacenar el n�mero de pasos por zona
    private Dictionary<string, int> zoneSteps = new Dictionary<string, int>()
    {
        { "1-1", 100 },  // Zona 1 requiere 100 pasos
        { "1-2", 150 },  // Zona 2 requiere 150 pasos
        { "1-3", 200 }   // Zona 3 requiere 200 pasos
    };


    void Start()
    {

        gameManager = GameManager.instance;
        // Inicialmente ocultar el panel de selecci�n de zonas
        zoneSelectionPanel.SetActive(false);

        botonZona1.GetComponent<Button>().onClick.AddListener(() => SelectZone("1-1"));
        botonZona2.GetComponent<Button>().onClick.AddListener(() => SelectZone("1-2"));
        botonZona3.GetComponent<Button>().onClick.AddListener(() => SelectZone("1-3"));
    }

    // Funci�n para mostrar el panel de selecci�n de zonas
    public void ShowZoneSelectionPanel()
    {
        zoneSelectionPanel.SetActive(true);
        if(GameManager.instance.spirits.Count == 2)
        {
            botonZona1.SetActive(false);
        }else if(GameManager.instance.spirits.Count == 3)
        {
            botonZona1.SetActive(false);
            botonZona2.SetActive(false);
        }
    }

    // Funci�n para ocultar el panel de selecci�n de zonas
    public void HideZoneSelectionPanel()
    {
        zoneSelectionPanel.SetActive(false);
    }

    // Funci�n para seleccionar una zona
    public void SelectZone(string zone)
    {
        selectedZone = zone;

        // Obtener los pasos requeridos seg�n la zona seleccionada
        int stepsRequired = 100;  // Valor por defecto
        if (zoneSteps.ContainsKey(zone))
        {
            stepsRequired = zoneSteps[zone];
        }

        

        if(selectedZone == "1-1")
        {
            zoneNumber = 0;
            gameManager.currentBoss = "Ranamon";
        }else if(selectedZone == "1-2")
        {
            zoneNumber = 1;
            gameManager.currentBoss = "Mercurymon";
        }
        else if (selectedZone == "1-3")
        {
            zoneNumber = 2;
            gameManager.currentBoss = "Lucemon";
        }

        gameManager.SetCurrentMap(zoneNumber);

        // Asignar el n�mero de pasos al MenuManager
        menuManager.SetStepsForZone(stepsRequired);

        selectedZoneText.text = "MAP " + selectedZone;

        // Ocultar el panel de selecci�n despu�s de elegir la zona
        HideZoneSelectionPanel();
    }

    // Funci�n para obtener la zona seleccionada
    public string GetSelectedZone()
    {
        return selectedZone;
    }
}