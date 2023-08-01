using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public enum CollectableType
    {
        None,
        PatientSitting,
        PatientSick,
        PatientHealty
    }

    public CollectableType collectableType;


    public Animator animator;
}
