using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private BallGenerator spawner;
    [SerializeField] private LayerMask mask;
    [SerializeField] private List<Gem> gemsToRemove = new List<Gem>();
    [SerializeField] private int currentGemID;
    [SerializeField] private int spawncount;
    [SerializeField] private int score;

    bool firstGemToTouch; //enables gem matching via dragging.  
    private Touch touch;
    Vector2 startPos;
    Vector2 draggingPos;
    Vector2 currentGemPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawner.GenerateGems(spawncount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnBeginTouch();
                    break;

                case TouchPhase.Moved:
                    OnDragging();
                    break;

                case TouchPhase.Ended:
                    OnEndTouch();
                    break;
            }

        }
    }

    void OnBeginTouch()
    {
        //cast ray from current touch positon if it hits any of the gem colliders the get gem component from the collider and get a ID and add it to the list of removable gems.
        startPos = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.zero, mask);
        
        if (hit)
        {
            if(hit.collider.gameObject.TryGetComponent<BombLogic>(out BombLogic bomb))
            {
                bomb.Explode();
                return;
            }

            Gem gem = hit.collider.GetComponent<Gem>();
            //defines first gems id as identification number to find same type of gems for till touchphase ends  
            currentGemID = gem.GetID();
            AddGem(gem);
            firstGemToTouch = true;
        }
    }

    void OnDragging()
    {
        draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(draggingPos, Vector2.zero, mask);
        if (hit)
        {
            //remember to comment and  make it more readable thisd monstrosity
            if(gemsToRemove.Count >= 2)
            {
                if (gemsToRemove[gemsToRemove.Count-2] == hit.collider.gameObject.GetComponent<Gem>())
                {
                    gemsToRemove[gemsToRemove.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
                    gemsToRemove.Remove(gemsToRemove[gemsToRemove.Count - 1]);
                    currentGemPosition = hit.collider.gameObject.transform.position;
                    return;
                }
            }


            //Does the same thing as onBeginDrag, this just enables to start clearing the line without tapping.
            Gem gem = hit.collider.GetComponent<Gem>();
            if(firstGemToTouch == false)
            {
                currentGemID = gem.GetID();
                AddGem(gem);
                firstGemToTouch = true;
                return;
            }

            //if gem is within distance of last added gem to clear and has same id as the first gem then it will add gem to the list
            if (gem.GetID() == currentGemID)
            {
                float distance = Vector3.Distance(gem.transform.position, currentGemPosition);
               // Debug.Log(distance);
                if (distance < 1.3f)
                {
                    AddGem(gem);
                }
            }
        }
    }

    void OnEndTouch()
    {
        //if over 3 matches clear the gems add score and spawn new gems instead.
        if(gemsToRemove.Count >= 3)
        {
            spawncount = gemsToRemove.Count;
            for (int i = 0; i < gemsToRemove.Count; i++)
            {
                Destroy(gemsToRemove[i].gameObject);
            }

            if(gemsToRemove.Count >= 7)
            {
                Instantiate(bombPrefab, currentGemPosition, Quaternion.identity);
            }

            spawner.GenerateGems(spawncount);
        } 
        else
        {
            foreach (var gemToDeselect in gemsToRemove)
            {
                gemToDeselect.transform.GetChild(0).gameObject.SetActive(false);
            }  
        }

        gemsToRemove.Clear();
        firstGemToTouch = false;
    }

    //checks if addable gem is already in the list
    void AddGem(Gem gemToAdd)
    {
        if (gemsToRemove.Contains(gemToAdd) == false)
        {
            currentGemPosition = gemToAdd.transform.position;
            gemToAdd.transform.GetChild(0).gameObject.SetActive(true);
            gemsToRemove.Add(gemToAdd);
        }
    }
}