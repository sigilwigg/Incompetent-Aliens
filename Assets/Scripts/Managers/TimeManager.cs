using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float deltaTime = 0;
    public bool isGamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!isGamePaused)
        {
            deltaTime = Time.deltaTime;
            
        }
        else
        {
            deltaTime = 0;
        }

    }
}
