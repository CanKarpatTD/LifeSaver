using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFinder : MonoBehaviour
{
    [Header("Obstacles")] [Space(20)]public GameObject handFromLeft;
    public GameObject handFromRight;
    public GameObject spike;
    public GameObject sedye;

    [Header("Upgrade Doors")] [Space(20)]public GameObject stockDoor;
    public GameObject upgradeDoor;

    [Header("Collectable Item Prefabs")] [Space(20)]public GameObject sittingPatient;
    public GameObject sickPatient;
    public GameObject healthyPatient;

    [Header("Stacked Item Prefabs")] [Space(20)]public GameObject sittingPatientStacked;
    public GameObject sickPatientStacked;
    public GameObject healthyPatientStacked;

    [Header("Finish line")] [Space(20)]public GameObject finishLinePrefab;
    
    [Header("Ground")] [Space(20)]public GameObject ground;
}
