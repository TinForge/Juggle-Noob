using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDatabase : MonoBehaviour
{
    [SerializeField] private Sprite[] skins;

    public static Sprite GetSkin()
    {
        BallDatabase b = FindObjectOfType<BallDatabase>();
        int num = (int)Mathf.PingPong(Time.timeSinceLevelLoad, b.skins.Length);
        return b.skins[num];

    }
}
