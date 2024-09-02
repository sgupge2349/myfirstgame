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
        }
    }

    private void AddRunners(int amount)
    {
        for(int i=0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnerParent);
        }
    }
}
