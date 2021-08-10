using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frier : MonoBehaviour
{
    public GameObject[] traySlots = new GameObject[4];
    public GameObject particles;

    GameObject particleClone;
    GameObject[] particleList = new GameObject[4];
    Transform[] trayTransforms = new Transform[4];
    bool isPlaying;

    AudioSource audioController;

    public bool isFull ()
    {

        for (int i = 0; i < 4; i++)
        {
            if (traySlots[i].transform.childCount == 0)
            {
                return false;
            }
        }
        return true;
    }
    public bool isEmpty()
    {
        for (int i = 0; i < 4; i++)
        {
            if (traySlots[i].transform.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Start()
    {
        isPlaying = false;
        audioController = GetComponent<AudioSource>();
        //because its easier to do it automatically
        for (int i = 0; i < 4; i++)
        {
            particleClone = Instantiate(particles, traySlots[i].transform.position, Quaternion.identity);//instantiate the 4 clones
            particleClone.transform.Rotate(new Vector3(-90, 0));
            particleClone.transform.SetParent(this.transform);
            particleList[i] = particleClone;//assign them to their proper slots
            particleList[i].SetActive(false);//disable them for now
        }
    }
    private void Update()
    {
        bool isEmpty = true;
        for (int i = 0; i < 4; i++)
        {
            if (traySlots[i].transform.childCount > 0)
            {
                particleList[i].SetActive(true);
                isEmpty = false;
            }
            else
            {
                particleList[i].SetActive(false);
            }
        }
        if (!isEmpty && !isPlaying)
        {
            audioController.Play();
            isPlaying = true;
        }
        else if (isEmpty)
        {
            audioController.Stop();
            isPlaying = false;
        }
        //if (isFull())
        //    particles.SetActive(true);
        //else
        //    particles.SetActive(false);
    }
}
