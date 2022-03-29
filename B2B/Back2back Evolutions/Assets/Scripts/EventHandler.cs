using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public GameObject building;
    public GameObject building_0;

    [SerializeField]
    private GameObject uiHandler;

    [SerializeField]
    GameObject dollyTracks;

    [SerializeField]
    GameObject dollyCameras;

    private DollyManager dollyManager;

    [SerializeField]
    GameObject player;
     
    PlayerHealth pHealth;

    [SerializeField]
    GameObject enemies;

    [SerializeField]
    List<GameObject> impactObjects;

    Vector3 explosionPos;

    [SerializeField]
    GameObject playerImpact;

    public bool playIntro;

    void Awake()
    {
        dollyManager = dollyTracks.GetComponent<DollyManager>();
    }

    void Start()
    {        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        pHealth = player.GetComponent<PlayerHealth>();

        if (playIntro)
        {
            //GAMEOBJECTS TO DISABLE DURING INTRO SCENE. SHOULD OBVIOUSLY BE A BETTER WAY TO DO THIS.
            DisablePlayerComponents();
            //
            dollyCameras.SetActive(false);
            enemies.SetActive(false);
            uiHandler.SetActive(false);

            Intro();
        }
        else
        {
            dollyManager.IntroIsFinished();
            pHealth.InitiateUI();
        }
    }

    private void DisablePlayerComponents()
    {
        player.GetComponent<PlayerHealth>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerCollision>().enabled = false;
        player.GetComponent<EatEnemy>().enabled = false;
        player.GetComponent<PlayerAttacks>().enabled = false;
        player.GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Intro()
    {
        player.GetComponent<Animator>().SetTrigger("IntroAnimation");
    }

    public void Impact()
    {
        playerImpact.GetComponent<PlayerImpact>().CameraShake();
        explosionPos = new Vector3(-109.5f, 5.44000006f, -663.200012f);
        GameObject explosionClone = Instantiate(Resources.Load("Explosion_0_Slow") as GameObject, explosionPos, Quaternion.identity);

        ParticleSystem ps = explosionClone.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.simulationSpeed = 0.1f;

        explosionClone.transform.localScale = new Vector3(30f, 30f, 30f);
        Destroy(explosionClone, 3f);
    }

    public void ImpactOnObjects()
    {
        building.GetComponent<Animator>().SetTrigger("BuildingFall");
        building_0.GetComponent<Animator>().SetTrigger("BuildingFall");

        foreach (GameObject thing in impactObjects)
        {
            thing.GetComponent<Rigidbody>().useGravity = true;
            thing.GetComponent<Rigidbody>().isKinematic = false;

            int directionX = Random.Range(-3, 3);
            int directionY = Random.Range(-3, 3);

            thing.GetComponent<Rigidbody>().AddForce(new Vector3((directionX * 100), 700, (directionY * 100)), ForceMode.Impulse);
            thing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 20, 200f), ForceMode.Impulse);
        }
    }

    public void PlayerStanding()
    {
        Invoke("ActivateCameras", 2f);
    }

    private void ActivateCameras()
    {
        dollyCameras.SetActive(true);
        dollyManager.IntroIsFinished();
        EnablePlayer();
    }


    private void EnablePlayer()
    {
        player.GetComponent<PlayerHealth>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerCollision>().enabled = true;
        player.GetComponent<EatEnemy>().enabled = true;
        player.GetComponent<PlayerAttacks>().enabled = true;
        player.GetComponent<BoxCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;

        uiHandler.SetActive(true);
        pHealth.InitiateUI();
    }



    private void Update()
    {
        int buildIndex = 0;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}
