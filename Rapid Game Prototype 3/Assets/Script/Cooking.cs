using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooking : MonoBehaviour
{
    public GameObject nextStage;
    public Slider slider;
    public bool beingCooked;
    public float duration;

    float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = duration;
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (beingCooked)
        {
            slider.gameObject.SetActive(true);

            timer += Time.deltaTime;
            slider.value = timer;

            if (timer > duration)
            {
                GameObject next =  Instantiate(nextStage, transform.parent);
                next.transform.localPosition = Vector3.zero;
                next.transform.localRotation = Quaternion.identity;

                if (next.name == "Paddy Burnt" || next.name == "Burnt Fries")
                {
                    GameManager.score -=2;
                }

                Cooking cooking = next.GetComponent<Cooking>();
                if (cooking != null)
                {
                    cooking.beingCooked = true;
                }

                Destroy(this.gameObject);
            }
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }
}
