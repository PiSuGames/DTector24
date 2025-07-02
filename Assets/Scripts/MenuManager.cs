using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject stepsPanel;      // El panel que se mostrar�
    public GameObject spiritPanel;     // Panel de Spirits
    public Text stepsTakenText;        // Texto que muestra los pasos que llevas
    public Text stepsRemainingText;    // Texto que muestra los pasos restantes
    public int totalStepsForCombat = 100; // Los pasos totales para llegar al combate
    public int currentSteps = 0;      // Los pasos actuales que llevas dados

    public GameObject chooseBtn;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public List<Texture> imageList; // Lista de im�genes
    public RawImage targetObjectImages; // Objeto Image en el que se mostrar�n las im�genes
    public int currentIndex = 0; // �ndice actual en la lista

    public Animator playerAnimator;  // Animator para la animaci�n de transici�n
    public string battleSceneName = "BattleScene";  // Nombre de la escena de batalla


    private bool isPanelVisible = false;  // Controla la visibilidad del panel

    void Start()
    {
        StartCoroutine(FindPlayerAfterDelay()); // Inicia la corrutina

        if (GameManager.instance.choosenKid.Contains("jp"))
        {
            imageList.RemoveAt(5);
            imageList.RemoveAt(4);
            imageList.RemoveAt(3);

        }else if(GameManager.instance.choosenKid.Contains("takuya"))
        {
            imageList.RemoveAt(2);
            imageList.RemoveAt(1);
            imageList.RemoveAt(0);
            
        }
        else if (GameManager.instance.choosenKid.Contains("koji"))
        {
            imageList.RemoveAt(5);
            imageList.RemoveAt(4);
            imageList.RemoveAt(3);
            imageList.RemoveAt(2);
            imageList.RemoveAt(1);
            imageList.RemoveAt(0);

        }

    }

    IEnumerator FindPlayerAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo

        GameObject player = GameObject.Find("Player(Clone)"); // Busca el objeto en la escena
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>(); // Obtiene el Animator

            


            if (playerAnimator != null)
            {
                Debug.Log("Animator encontrado y asignado.");
            }
            else
            {
                Debug.LogError("El objeto Player(Clone) no tiene Animator.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� un objeto llamado Player(Clone) en la escena.");
        }
    }



    // M�todo para abrir/cerrar el men�
    public void ToggleMenu()
    {
        isPanelVisible = !isPanelVisible;
        stepsPanel.SetActive(isPanelVisible);  // Muestra u oculta el panel
        UpdateStepsText();  // Actualiza los textos de pasos
    }

    // M�todo para abrir/cerrar el men� spirits
    public void ToggleMenuSpirits()
    {
        isPanelVisible = !isPanelVisible;
        spiritPanel.SetActive(isPanelVisible);  // Muestra u oculta el panel

        ResetToDefault();

        if (GameManager.instance.spirits.Count == 1)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            chooseBtn.SetActive(false);
        }
    }

    public void ChooseSpirit() {

        if (GameManager.instance.choosenKid.Contains("jp"))
        {
            if (currentIndex == 0)
            {
                GameManager.instance.currentSpirit = "Beetlemon";

            }
            else if (currentIndex == 1)
            {
                GameManager.instance.currentSpirit = "Metalkabuterimon";

            }
            else if (currentIndex == 2)
            {
                GameManager.instance.currentSpirit = "AncientBeetlemon";

            }

        }else if (GameManager.instance.choosenKid.Contains("takuya"))
        {
            if (currentIndex == 0)
            {
                GameManager.instance.currentSpirit = "Agunimon";

            }
            else if (currentIndex == 1)
            {
                GameManager.instance.currentSpirit = "Burninggreymon";

            }
            else if (currentIndex == 2)
            {
                GameManager.instance.currentSpirit = "Aldamon";

            }
        }else if (GameManager.instance.choosenKid.Contains("koji"))
        {
            if (currentIndex == 0)
            {
                GameManager.instance.currentSpirit = "Lobomon";

            }
            else if (currentIndex == 1)
            {
                GameManager.instance.currentSpirit = "Kendogarurumon";

            }
            else if (currentIndex == 2)
            {
                GameManager.instance.currentSpirit = "Beowolfmon";

            }
        }
    }

    // Cambiar la imagen hacia adelante
    public void NextImage()
    {
        if (GameManager.instance.spirits.Count == 2)
        {
            if(currentIndex == 1)
            {

            }
            else
            {
                currentIndex = (currentIndex + 1) % imageList.Count;
                targetObjectImages.texture = imageList[currentIndex];
            }
        }
        else
        {
            currentIndex = (currentIndex + 1) % imageList.Count;
            targetObjectImages.texture = imageList[currentIndex];
        }
    }

    // Cambiar la imagen hacia atr�s
    public void PreviousImage()
    {
        if (GameManager.instance.spirits.Count == 2)
        {
            if(currentIndex == 0)
            {

            }
            else
            {
                currentIndex = (currentIndex - 1 + imageList.Count) % imageList.Count;
                targetObjectImages.texture = imageList[currentIndex];
            }

        }
        else
        {
            currentIndex = (currentIndex - 1 + imageList.Count) % imageList.Count;
            targetObjectImages.texture = imageList[currentIndex];
        }
    }

    // Resetear a la imagen inicial
    public void ResetToDefault()
    {
        currentIndex = 0;
        targetObjectImages.texture = imageList[currentIndex];
    }

    // M�todo que se llama cuando el personaje se mueve
    public void AddSteps(int steps)
    {
        currentSteps += steps;
        Debug.Log("Pasos "+ currentSteps);
        // Asegurarse de que no se superen los pasos totales
        if (currentSteps >= totalStepsForCombat)
        {
            currentSteps = totalStepsForCombat;
            // Aqu� puedes a�adir l�gica para iniciar el combate si quieres
            Debug.Log("�Es hora de combatir!");
            playerAnimator.SetBool("isWalking", false);
            StartBattleTransition();  // Iniciar la animaci�n y cambiar de escena

        }

        UpdateStepsText();  // Actualiza los textos en la UI
    }

    // M�todo para establecer los pasos totales seg�n la zona seleccionada
    public void SetStepsForZone(int steps)
    {
        totalStepsForCombat = steps + currentSteps;
        //currentSteps = 0;  // Reinicia los pasos actuales al seleccionar una nueva zona
        UpdateStepsText();  // Actualiza los textos de pasos en la UI
    }

    // Actualiza los textos que muestran los pasos
    private void UpdateStepsText()
    {
        stepsTakenText.text = currentSteps.ToString();
        stepsRemainingText.text = (totalStepsForCombat - currentSteps).ToString();
    }

    public void StartBattleTransition()
    {
        // Iniciar la animaci�n del player

        Debug.Log(GameManager.instance.currentSpirit);
        Debug.Log(GameManager.instance.currentBoss);

        if (GameManager.instance.currentSpirit == "Beetlemon" && GameManager.instance.currentBoss == "Ranamon")
        {
            playerAnimator.SetTrigger("StartBattle");
        }

        if (GameManager.instance.currentSpirit == "Agunimon" && GameManager.instance.currentBoss == "Ranamon")
        {
            playerAnimator.SetTrigger("StartBattle");
        }

        if (GameManager.instance.currentSpirit == "Lobomon" && GameManager.instance.currentBoss == "Ranamon")
        {
            playerAnimator.SetTrigger("StartBattle");
        }

        if (GameManager.instance.currentSpirit == "Metalkabuterimon" && GameManager.instance.currentBoss == "Mercurymon")
        {
            playerAnimator.SetTrigger("StartBattle2");
        }

        if (GameManager.instance.currentSpirit == "Burninggreymon" && GameManager.instance.currentBoss == "Mercurymon")
        {
            playerAnimator.SetTrigger("StartBattle2");
        }

        if (GameManager.instance.currentSpirit == "Kendogarurumon" && GameManager.instance.currentBoss == "Mercurymon")
        {
            playerAnimator.SetTrigger("StartBattle2");
        }

        if (GameManager.instance.currentSpirit == "Beetlemon" && GameManager.instance.currentBoss == "Mercurymon")
        {
            playerAnimator.SetTrigger("StartBattle3");
        }

        if(GameManager.instance.currentSpirit == "Agunimon" && GameManager.instance.currentBoss == "Mercurymon")
        {
            playerAnimator.SetTrigger("StartBattle3");
        }

        if (GameManager.instance.currentSpirit == "Lobomon" && GameManager.instance.currentBoss == "Mercurymon")
        {
            playerAnimator.SetTrigger("StartBattle3");
        }

        if (GameManager.instance.currentSpirit == "AncientBeetlemon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle4");
        }

        if(GameManager.instance.currentSpirit == "Aldamon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle4");
        }

        if (GameManager.instance.currentSpirit == "Beowolfmon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle4");
        }

        if (GameManager.instance.currentSpirit == "Beetlemon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle5");
        }

        if(GameManager.instance.currentSpirit == "Agunimon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle5");
        }

        if (GameManager.instance.currentSpirit == "Lobomon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle5");
        }

        if (GameManager.instance.currentSpirit == "Metalkabuterimon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle6");
        }

        if(GameManager.instance.currentSpirit == "Burninggreymon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle6");
        }

        if (GameManager.instance.currentSpirit == "Kendogarurumon" && GameManager.instance.currentBoss == "Lucemon")
        {
            playerAnimator.SetTrigger("StartBattle6");
        }
    }

    // Este m�todo ser� llamado por el evento de la animaci�n
    public void OnBattleAnimationEnd()
    {
        // Cambiar a la escena de batalla
        SceneManager.LoadScene(battleSceneName);
    }
}

