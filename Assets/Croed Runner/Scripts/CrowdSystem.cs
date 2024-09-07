using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Transform runnerParent;
    [SerializeField] private GameObject runnerPrefab;

    [Header("Setting")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaceRunners();

    }

    private void PlaceRunners()
    {
        for(int i = 0; i < runnerParent.childCount; i++)
        {
            Vector3 childLocalPosition=GetRunnerLocalPosition(i);
            runnerParent.GetChild(i).localPosition = childLocalPosition;
        }

       
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x,0,z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnerParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;

            case BonusType.Product:
                int runnersToAdd = (runnerParent.childCount * bonusAmount) - runnerParent.childCount;
                AddRunners(runnersToAdd);
                break;

            case BonusType.Difference:
                RemoveRunner(bonusAmount);
                break;

            case BonusType.Division:
                int runnersToRemove = runnerParent.childCount - (runnerParent.childCount / bonusAmount);
                RemoveRunner(runnersToRemove);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for(int i=0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnerParent);
        }
    }

    private void RemoveRunner(int amount)
    {
        if(amount > runnerParent.childCount)
        {
            amount = runnerParent.childCount;
        }
        int runnersAmount = runnerParent.childCount;

        for(int i =runnersAmount - 1;i>=runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnerParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
