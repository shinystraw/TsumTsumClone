using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score : MonoBehaviour
{
    public float score;
    public Score( float score)
    {
        this.score = score;
    }
}
