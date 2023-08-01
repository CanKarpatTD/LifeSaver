using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleController : MonoBehaviour
{
    public enum ObstacleType
    {
        None,
        MovingObstacle,
        Spike
    }

    public ObstacleType obstacleType;

    public enum MovingObstacleType
    {
        None,
        Hand,
        GPU
    }

    public MovingObstacleType movingObstacleType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.hardwareList.Count == 0)
            {
                GameManager.Instance.GameLose();
            }
            else
            {
                gameObject.transform.DOScaleY(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    gameObject.transform.DOMoveY(-1, 0.1f);
                });
            }
        }

        if (other.gameObject.CompareTag("Collected"))
        {
            if (obstacleType == ObstacleType.MovingObstacle)
            {
                if (movingObstacleType == MovingObstacleType.Hand)
                {
                    //TODO: Temas ettiği objeler gg olucak. Eğer listenin son objesi ise destroy değilse saçılma.

                    if (other.gameObject ==
                        GameManager.Instance.hardwareList[GameManager.Instance.hardwareList.Count - 1])
                    {
                        //Particle
                        Instantiate(GameManager.Instance.smokeExplosion, other.transform.position, Quaternion.identity);

                        GameManager.Instance.hardwareList.Remove(other.gameObject);
                        Destroy(other.gameObject);
                    }
                    else
                    {
                        Crash(other.gameObject, false);
                    }
                }

                if (movingObstacleType == MovingObstacleType.GPU)
                {
                    //TODO: Eşyalara temas ettiği kısımdan sonrası etrafa saçılıcak. Eğer listenin son objesi ise destroy değilse saçılma.

                    if (other.gameObject ==
                        GameManager.Instance.hardwareList[GameManager.Instance.hardwareList.Count - 1])
                    {
                        //Particle
                        Instantiate(GameManager.Instance.smokeExplosion, other.transform.position, Quaternion.identity);

                        GameManager.Instance.hardwareList.Remove(other.gameObject);
                        Destroy(other.gameObject);
                    }
                    else
                    {
                        Crash(other.gameObject, true);
                    }
                }
            }

            if (obstacleType == ObstacleType.Spike)
            {
                //TODO: Eşyalar etrafa saçılıcak. Eğer listenin son objesi ise destroy değilse saçılma.

                if (other.gameObject == GameManager.Instance.hardwareList[GameManager.Instance.hardwareList.Count - 1])
                {
                    //Particle
                    Instantiate(GameManager.Instance.smokeExplosion, other.transform.position, Quaternion.identity);

                    GameManager.Instance.hardwareList.Remove(other.gameObject);
                    Destroy(other.gameObject);
                }
                else
                {
                    Crash(other.gameObject, true);
                }
            }

            other.gameObject.GetComponent<HardwareController>().BlopEffect();
        }
    }

    private void Crash(GameObject other, bool jump)
    {
        foreach (var item in GameManager.Instance.hardwareList)
        {
            if (item == other.gameObject)
            {
                int index = GameManager.Instance.hardwareList.IndexOf(item);

                if (index >= 6)
                {
                    index = 6;
                }

                for (int i = index; i < GameManager.Instance.hardwareList.Count; i++)
                {
                    GameManager.Instance.CheckEmptyObj(i);

                    //Particle
                    Instantiate(GameManager.Instance.smokeExplosion, other.transform.position, Quaternion.identity);

                    if (jump)
                    {
                        Destroy(gameObject);

                        Vector3 spawnPos = new Vector3(other.transform.position.x, other.transform.position.y,
                            other.transform.position.z);

                        if (GameManager.Instance.hardwareList[i].GetComponent<HardwareController>().hardwareType ==
                            HardwareController.HardwareType.PatientSitting)
                        {
                            GameObject ga = Instantiate(GameManager.Instance.collectablePatientSitting, spawnPos,
                                Quaternion.identity);


                            Vector3 jumpPos = new Vector3(other.transform.position.x + Random.Range(-2.3f, 1),
                                other.transform.position.y, other.transform.position.z + Random.Range(5, 8));

                            ga.transform.DOJump(jumpPos, 3.6f, 1, 0.6f).SetEase(Ease.Linear);
                        }

                        if (GameManager.Instance.hardwareList[i].GetComponent<HardwareController>().hardwareType ==
                            HardwareController.HardwareType.PatientSick)
                        {
                            GameObject ga = Instantiate(GameManager.Instance.collectablePatientSick, spawnPos,
                                Quaternion.identity);

                            Vector3 jumpPos = new Vector3(other.transform.position.x + Random.Range(-2.3f, 1),
                                other.transform.position.y, other.transform.position.z + Random.Range(5, 8));

                            ga.transform.DOJump(jumpPos, 3.6f, 1, 0.6f).SetEase(Ease.Linear);
                        }

                        if (GameManager.Instance.hardwareList[i].GetComponent<HardwareController>().hardwareType ==
                            HardwareController.HardwareType.PatientHealty)
                        {
                            GameObject ga = Instantiate(GameManager.Instance.collectablePatientHealty, spawnPos,
                                Quaternion.identity);

                            Vector3 jumpPos = new Vector3(other.transform.position.x + Random.Range(-2.3f, 1),
                                other.transform.position.y, other.transform.position.z + Random.Range(5, 8));

                            ga.transform.DOJump(jumpPos, 3.6f, 1, 0.6f).SetEase(Ease.Linear);
                        }
                    }
                }
            }
        }
    }
}