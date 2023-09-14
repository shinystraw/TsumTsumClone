using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Gem : MonoBehaviour
{

    [SerializeField] private int id;

    [Header("Debug")]
    [SerializeField] private int listcount;
    [SerializeField] private float radius;

    List<GameObject> gemsList = new List<GameObject>();
    List<GameObject> gemsToRemove = new List<GameObject>();

    public void SetID(int idNum)
    {
        id = idNum;
    }

    public int GetID()
    {
        return id;
    }

    //public bool isConnectable() 
    //{
    //    return connectable; 
    //}

    //private void Update()
    //{
    //    listcount = gemsList.Count;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if collision gameobject has GemID component with same ID add it to the list and change connectable to true  
    //    if (collision.gameObject.TryGetComponent<Gem>(out Gem gemID)) 
    //    {
    //        if (gemID.GetID() == id)
    //        {
    //            gemsList.Add(collision.gameObject);
    //            connectable = true;
    //        } 
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //   // Debug.Log(gemsList.Count);
    //    //if the exiting gameobject is a gem in the list add it to removable list, this is due to a runtime error if the gem is removed from list directly during the first loop
    //    foreach (GameObject gem in gemsList) 
    //    {
    //        if (gem == collision.gameObject)
    //        {
    //            gemsToRemove.Add(gem);
    //        }
    //    }

    //    //removes gem that matches with gem object inside the removable list, it will then clear the list to prevent removing gems that are currently in contact again with this gameobject
    //    foreach (GameObject gem in gemsToRemove)
    //    {
    //        gemsList.Remove(gem);
    //    }    

    //    gemsToRemove.Clear();

    //    //if there are no gameobjets inside it is not connectable
    //    if(gemsList.Count == 0)
    //    {
    //        connectable = false;
    //    }

    //}

    //private void OnDrawGizmos()
    //{
    //    //Gizmos.DrawWireSphere(transform.position, radius);
    //}

}
