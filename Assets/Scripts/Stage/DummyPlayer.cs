using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{

}
public class DummyPlayer : MonoBehaviour
{
    public UnityEventInt StageSpawn;
    public bool spawn = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
        if(spawn)
        {
            StageSpawn.Invoke(0);
            spawn = false;
        }
    }
}
