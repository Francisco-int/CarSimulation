using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // M�todo para cargar una escena espec�fica por su nombre
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // M�todo para salir del juego (opcional, �til para el men� principal)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego..."); // Esto solo se ver� en el editor de Unity.
    }
}

