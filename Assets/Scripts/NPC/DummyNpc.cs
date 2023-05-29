using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyNpc : MonoBehaviour
{
    public bool show = false;
    public Material m;
    // Start is called before the first frame update
    void Start()
    {
        m = gameObject.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(show)
        {
            m.SetFloat("_ShowOutline", 1f);
        }
        else
        {
            m.SetFloat("_ShowOutline", 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log(show);
            show = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
            show = false;
    }
}
