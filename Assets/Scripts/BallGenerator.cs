using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private Sprite[] gemSprites;

    [Header("Attributes")]
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private float delay = 0.04f;


    void Start()
    {

        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Generate()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-5f, 5f), 8) ;
            GameObject gem = Instantiate(gemPrefab, spawnPos, Quaternion.identity);
            int id = Random.Range(0, gemSprites.Length);
            gem.GetComponent<GemID>().SetID(id);
            gem.GetComponent<SpriteRenderer>().sprite = gemSprites[id];
            yield return new WaitForSeconds(delay);
        }
    }
}
