using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    private StageTemplates Stage; 
    [SerializeField] private GameObject[] spawnPoint;
    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new GameObject[4];
        Stage = GameObject.Find("Stage").GetComponent<StageTemplates>();
        StageChange();
    }

    void StageChange()
    {
        for (int i = 0; i <= 3; i++)
        {
            spawnPoint[i] = GameManager.Instance.currentStage.transform.GetChild(i).gameObject;
        }
    }
    // Update is called once per frame
    public void Spawn(int opneingDirection)
    {
        if (opneingDirection == 1)
        {
            // need to spawn top
            rand = Random.Range(0, Stage.top.Length);
            Instantiate(Stage.top[rand], spawnPoint[0].transform.position, Stage.top[rand].transform.rotation);
        }
        else if (opneingDirection == 2)
        {
            // need to spawn bottom
            rand = Random.Range(0, Stage.bottom.Length);
            Instantiate(Stage.bottom[rand], spawnPoint[1].transform.position, Stage.bottom[rand].transform.rotation);
        }
        else if (opneingDirection == 3)
        {
            // need to spawn left
            rand = Random.Range(0, Stage.left.Length);
            Instantiate(Stage.left[rand], spawnPoint[2].transform.position, Stage.left[rand].transform.rotation);
        }
        else if (opneingDirection == 4)
        {
            // need to spawn right
            rand = Random.Range(0, Stage.right.Length);
            Instantiate(Stage.right[rand], spawnPoint[3].transform.position, Stage.right[rand].transform.rotation);
        }

    }
}
