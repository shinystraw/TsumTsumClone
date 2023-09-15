using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;
    GameLogic gameLogic;
    BallGenerator spawner;
    bool exploded = false;

    private void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BallGenerator>();
    }

    [ContextMenu("EXPLOOOOSION!!!")]
    public void Explode()
    {

        if (!exploded)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius, layer);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].gameObject.layer == 7 && !hit[i].gameObject == this.gameObject)
                {
                    exploded = true;
                    hit[i].gameObject.GetComponent<BombLogic>().Explode();
                    continue;
                }

                Destroy(hit[i].gameObject);
            }
            Debug.Log(hit.Length);

            gameLogic.AddScore(hit.Length * 300);
            
            if (exploded && hit.Length > 1)
            {
                Debug.Log("HI?");
                spawner.GenerateGems(hit.Length - 2);
            }
            else if(hit.Length > 0)
            { 
                spawner.GenerateGems(hit.Length - 1);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
