using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] GameObject[] gems;
    Vector2[] directions = { Vector2.right, Vector2.down, Vector2.up, Vector2.up };
    bool goingUP;
    // Start is called before the first frame update
    void Start()
    {
       gems = GameObject.FindGameObjectsWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    [ContextMenu("GurenLagan!!")]
    public void TengenToppa()
    {
        if (goingUP == false)
        {
            goingUP = true;
            gems = GameObject.FindGameObjectsWithTag("Gem");
            for (int i = 0; i < gems.Length; i++)
            {
                Vector2 vectoria = directions[Random.Range(0, directions.Length)];
                gems[i].GetComponent<Rigidbody2D>().AddForce(vectoria* 1000 * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            goingUP = false;
        }
    }
}
