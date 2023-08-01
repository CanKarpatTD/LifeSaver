using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class HardwareController : MonoBehaviour
{
    public enum HardwareType
    {
        None,
        PatientSitting,
        PatientSick,
        PatientHealty
    }

    public HardwareType hardwareType;

    public GameObject patientSitting;
    public GameObject patientSick;
    public GameObject patientHealty;
    
    public bool canMove;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            if (other.gameObject.GetComponent<CollectableController>().collectableType == CollectableController.CollectableType.PatientSitting)
            {
                GameManager.Instance.GainProduct(1);
            }
            
            if (other.gameObject.GetComponent<CollectableController>().collectableType == CollectableController.CollectableType.PatientSick)
            {
                GameManager.Instance.GainProduct(2);
            }
            
            if (other.gameObject.GetComponent<CollectableController>().collectableType == CollectableController.CollectableType.PatientHealty)
            {
                GameManager.Instance.GainProduct(3);
            }
            
            PlexusExplosion();
            
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Door"))
        {
            var checker = other.gameObject.GetComponent<DoorController>().doorType;

            var myVoid = other.gameObject.GetComponent<DoorController>();

            
            
            myVoid.CloseOther();
            
            if (checker == DoorController.DoorType.Stock)
            {
                other.gameObject.GetComponent<DOTweenAnimation>().DORestart();
                
                // other.gameObject.transform.GetChild(1).transform.DOScale(0.22f, 0.1f).SetEase(Ease.OutBack).OnComplete(() =>
                // {
                //     other.gameObject.transform.GetChild(1).transform.DOScale(0.1685751f, 0.1f);
                // });

                GameManager.Instance.stocked ++;

                PlexusExplosion();
                
                GameManager.Instance.hardwareList.Remove(gameObject);
                GameManager.Instance.stockedHardwareList.Add(gameObject);
                Destroy(gameObject);
            }

            if (checker == DoorController.DoorType.Upgrade)
            {
                PlexusExplosion();
                
                if (hardwareType == HardwareType.PatientSitting)
                {
                    patientSitting.SetActive(false);
                    patientSick.SetActive(true);
                    patientHealty.SetActive(false);

                    TimeManager.Instance.transform.DOMoveX(0, 0.5f).OnComplete(() => { hardwareType = HardwareType.PatientSick; });
                }

                if (hardwareType == HardwareType.PatientSick)
                {
                    patientSitting.SetActive(false);
                    patientSick.SetActive(false);
                    patientHealty.SetActive(true);
                    
                    TimeManager.Instance.transform.DOMoveX(0, 0.5f).OnComplete(() => { hardwareType = HardwareType.PatientHealty; });
                }

                if (hardwareType == HardwareType.PatientHealty)
                {
                    patientSitting.SetActive(false);
                    patientSick.SetActive(false);
                    patientHealty.SetActive(true);
                }
                
                BlopEffect();
            }
        }
    }

    public void PlexusExplosion()
    {
        var pos = gameObject.transform.position.y + 1;
        Instantiate(GameManager.Instance.plexusEffect, new Vector3(gameObject.transform.position.x,pos,gameObject.transform.position.z), Quaternion.identity);
    }

    public void BlopEffect()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f);
        });
    }
}
