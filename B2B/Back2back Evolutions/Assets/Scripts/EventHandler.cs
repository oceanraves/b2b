using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    private List<int> stepList;
    private int _totalSteps;

    int steps;

    int currentStep;

    [SerializeField]
    bool stepCompleted = false;

    public GameObject building;
    public GameObject building_0;

    void Awake()
    {
        building.GetComponent<Animator>().SetTrigger("BuildingFall");
        building_0.GetComponent<Animator>().SetTrigger("BuildingFall");
    }

    void Start()
    {
        _totalSteps = 10;
        currentStep = 1;
        GetStepNumbers();       
        
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        int buildIndex = 0;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
    private void GetStepNumbers()
    {
        for(steps = 0; steps <= _totalSteps; steps++)
        {
            //Debug.Log(steps);
        }
    }

    private void NextStep()
    {
        if (stepCompleted)
        {
            if (currentStep <= _totalSteps)
            {
                currentStep++;
            }
        }
    }

    //STEP 1


}
