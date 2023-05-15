using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendToStage : MonoBehaviour
{
    public string TargetStage;
    public bool forceUnpause;
    public void SendToTargetStage()
    {
        if (forceUnpause)
        {
            GameManager.Instance.SystemPause(false);
        }

        SceneManager.LoadScene(TargetStage);
        
    }
}
