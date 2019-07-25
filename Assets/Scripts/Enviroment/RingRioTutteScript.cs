using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RingRioTutteScript : MonoBehaviour
{
    public RioTutteMainScript mainScript;
    private void OnTriggerExit(Collider other)
    {
        if (mainScript.phase == 1)
        {
            if (other.gameObject.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (other.gameObject.tag == "Enemie")
            {
                mainScript.ChangePhase(mainScript.phase + 1);
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }
}
