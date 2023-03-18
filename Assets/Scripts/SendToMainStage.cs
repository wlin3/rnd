using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendToMainStage : MonoBehaviour
{
    public void SendToMenu()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
