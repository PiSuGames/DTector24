using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiritController : MonoBehaviour
{
    // Este es el m�todo que se llamar� cuando la animaci�n termine
    public void OnVictoryAnimationEnd()
    {
        // Cambiar de escena cuando termine la animaci�n
        SceneManager.LoadScene("MainScene");
    }
}