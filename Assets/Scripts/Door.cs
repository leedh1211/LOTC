using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private AnimationCurve enableCurve;
    
    private void Start()
    {
        renderer.color = Color.clear;
        
        ProgressTweener tweener = new(this);

        tweener.Play((ratio) => renderer.color = new Color(1, 1, 1, ratio), 0.5f).SetCurve(enableCurve);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        FindAnyObjectByType<MainGameController>().NewGame();
        
        Destroy(gameObject);
    }
}
