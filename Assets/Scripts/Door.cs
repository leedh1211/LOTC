using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        SceneManager.LoadScene("NEWSampleGameScene");
    }
}
