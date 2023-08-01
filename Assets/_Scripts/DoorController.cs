using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public enum DoorType
    {
        None,
        Upgrade,
        Stock
    }

    public DoorType doorType;

    public GameObject otherDoor;

    // public TextMeshPro stockText;
    //public int stocked;



    // private void Update()
    // {
    //     if (doorType == DoorType.Stock)
    //         stockText.text = GameManager.Instance.stocked.ToString();
    // }

    public void CloseOther()
    {
        if (otherDoor != null)
        {
            Destroy(otherDoor);
        }
    }
}
