using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class loadmain : MonoBehaviour {

    public void launchGame()
    {
        SceneManager.LoadScene("main");
    }
}
