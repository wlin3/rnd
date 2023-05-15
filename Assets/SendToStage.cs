using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendToStage : MonoBehaviour
{
    public string TargetStage;
    public void SendToTargetStage()
    {
        SceneManager.LoadScene(TargetStage);
    }
}
