using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentStage;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if(_instance == null)
                {
                    Debug.Log("해당 인스턴스가 존재하지 않습니다.");
                }
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    public void stageChange(GameObject g)
    {
        currentStage = g;
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
