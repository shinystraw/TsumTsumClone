using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private Sprite[] gemSprites;
    [SerializeField] private Sprite[] selectSprites;

    [Header("Attributes")]

    [SerializeField] private float delay = 0.04f;

    public void GenerateGems(int spawnCount)
    {
        StartCoroutine(Generate(spawnCount));
    }

    private IEnumerator Generate(int spawnCount)
    {
        for(int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-1f, 1f), 8) ;
            GameObject gem = Instantiate(gemPrefab, spawnPos, Quaternion.identity);
            int id = Random.Range(0, gemSprites.Length);

            GameObject onSelectGem = gem.transform.GetChild(0).gameObject;
            onSelectGem.GetComponent<SpriteRenderer>().sprite = selectSprites[id];
            onSelectGem.SetActive(false);

            gem.GetComponent<Gem>().SetID(id);
            gem.GetComponent<SpriteRenderer>().sprite = gemSprites[id];
            yield return new WaitForSeconds(delay);
        }
    }
}
