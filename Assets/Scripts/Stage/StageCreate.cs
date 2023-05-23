using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    private StageTemplates Stage;
    public int opneingDirection;
    private int rand;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        Stage = GameObject.Find("Stage").GetComponent<StageTemplates>();
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if(!spawned)
        {
            if (opneingDirection == 1)
            {
                // need to spawn top
                rand = Random.Range(0, Stage.top.Length);
                Instantiate(Stage.top[rand], transform.position, Stage.top[rand].transform.rotation);
            }
            else if (opneingDirection == 2)
            {
                // need to spawn bottom
                rand = Random.Range(0, Stage.bottom.Length);
                Instantiate(Stage.bottom[rand], transform.position, Stage.bottom[rand].transform.rotation);
            }
            else if (opneingDirection == 3)
            {
                // need to spawn left
                rand = Random.Range(0, Stage.left.Length);
                Instantiate(Stage.left[rand], transform.position, Stage.left[rand].transform.rotation);
            }
            else if (opneingDirection == 4)
            {
                // need to spawn right
                rand = Random.Range(0, Stage.right.Length);
                Instantiate(Stage.right[rand], transform.position, Stage.right[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint") && other.GetComponent<StageCreate>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}
