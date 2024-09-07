using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private CrowdSystem crowdSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectDoors();
    }
    
    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

        for(int i=0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                Debug.Log("We hit some doors");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);


                doors.Disable();

                crowdSystem.ApplyBonus(bonusType, bonusAmount);

            }

            else if (detectedColliders[i].tag == "Finish")
            {
                Debug.Log("WE Finish");
                SceneManager.LoadScene(0);
            }
        }
    }
}
