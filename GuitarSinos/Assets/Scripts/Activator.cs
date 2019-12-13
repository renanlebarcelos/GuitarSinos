using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public bool createMode;
    public GameObject n;
    public KeyCode key;

    bool active = false;
    Color old;
    GameObject note, gm;
    MeshRenderer mr;

    void Awake() {
        mr = GetComponent<MeshRenderer>();
    }

    void Start() {
        gm = GameObject.Find("GameManager");
        old = mr.material.color;
    }

    void Update() {
        if(createMode && Input.GetKeyDown(key)) {
            if(Input.GetKeyDown(key))
                Instantiate(n, transform.position, Quaternion.identity);
        } else {
            if(Input.GetKeyDown(key))
                StartCoroutine(Pressed());

            if (Input.GetKeyDown(key) && active) {
                Destroy(note);
                gm.GetComponent<GameManager>().AddStreak();
                AddScore();
                active = false;
            } else if(Input.GetKeyDown(key) && !active) {
                gm.GetComponent<GameManager>().ResetStreak();
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        active = true;
        if(col.gameObject.tag == "Note") {
            note = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col) {
        active = false;
        gm.GetComponent<GameManager>().ResetStreak();
    }

    void AddScore() {
        PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score") + gm.GetComponent<GameManager>().GetScore());
    }

    IEnumerator Pressed() {
        Color old = mr.material.color;

        mr.material.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.05f);

        mr.material.color = old;
    }
}
