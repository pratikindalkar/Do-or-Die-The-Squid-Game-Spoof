using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool runnning = false;
    bool game_Started = false;
    bool game_over = false;

    public GameObject player;
    public Animator player_animator;

    public GameObject toy;
    public Animator toy_animator;

    public GameObject laser;

    public GameObject camera;

    public ParticleSystem blood_splash;
    public GameObject blood;

    AudioSource source;
    public AudioClip step;
    public AudioClip shooting;
    public AudioClip hit;
    public AudioClip fall;

    public GameObject ui_start;
    public GameObject ui_gameover;
    public GameObject ui_win;
    public Text ui_guide;

    float speed = 1;


    KeyCode key1=0, key2=0, key3=0;

    float steps_counter;
    void Start()
    {
        source = GetComponent<AudioSource>();
        ui_start.SetActive(true);
    }

    void Update()
    {
        if (runnning)
        {
            player.transform.position -= new Vector3(0, 0, 0.5f * Time.deltaTime);
            camera.transform.position -= new Vector3(0, 0, 0.5f * Time.deltaTime);

            steps_counter += Time.deltaTime;
            if (steps_counter > .25f)
            {
                steps_counter = 0;
                source.PlayOneShot(step);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !game_Started)
        {
            runnning = true;
            game_Started = true;
            ui_start.SetActive(false);
            player_animator.SetTrigger("run");
            StartCoroutine(Sing());
        }
        if (Input.GetKeyDown(KeyCode.Space) && game_over)
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKey(key1) && Input.GetKey(key2) && Input.GetKey(key3) && !game_over)
        {
            runnning = false;
            player_animator.speed = 0;
        }
        else if ((Input.GetKeyUp(key1) || Input.GetKeyUp(key2) || Input.GetKeyUp(key3) && !game_over))
        {
            runnning = true;
            player_animator.speed = 1;
        }
    }
    IEnumerator Sing()
    {
        toy.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.5f / speed);

        key1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), (System.Char.ConvertFromUtf32('A' + Random.Range(0, 25)).ToString()));
        key2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), (System.Char.ConvertFromUtf32('A' + Random.Range(0, 25)).ToString()));
        key3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), (System.Char.ConvertFromUtf32('A' + Random.Range(0, 25)).ToString()));

        ui_guide.text = "PRESS " + key1 + " + " + key2 + " + " + key3 + " TO STOP";

        toy_animator.SetTrigger("look");
        yield return new WaitForSeconds(2 / speed);
        //Checks if the player is still moving
        if (runnning)
        {
            Debug.Log("Shot the Player");
            GameObject new_laser = Instantiate(laser);
            new_laser.transform.position = toy.transform.GetChild(0).transform.position;
            game_over = true;
            source.PlayOneShot(shooting);
        }
        ui_guide.text = "";

        yield return new WaitForSeconds(2 / speed);
        toy_animator.SetTrigger("idle");
        yield return new WaitForSeconds(1 / speed);
        toy.GetComponent<AudioSource>().Stop();

        speed = speed * 1.05f;
        toy.GetComponent<AudioSource>().pitch = speed;

        if (!game_over) StartCoroutine(Sing());
    }
    public void HitPlayer()
    {
        runnning = false;
        player_animator.SetTrigger("idle");
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 2, 2);
        player.GetComponent<Rigidbody>().angularVelocity = new Vector3(3, 0, 0);
        camera.GetComponent<Animator>().Play("camera_lose");
        blood_splash.Play();
        StartCoroutine(ShowBlood());
        source.PlayOneShot(hit);
    }

    IEnumerator ShowBlood()
    {
        runnning = false;
        yield return new WaitForSeconds(.3f);
        ui_gameover.SetActive(true);
        source.PlayOneShot(fall);
        blood.SetActive(true);
        blood.transform.position = new Vector3(player.transform.position.x, 0.001f, player.transform.position.z + 0.15f);
    }
    public IEnumerator Playerwin()
    {
        game_over = true;
        yield return new WaitForSeconds(1f);

        runnning = false;
        player_animator.SetTrigger("idle"); 

        ui_win.SetActive(true);
    }
}

