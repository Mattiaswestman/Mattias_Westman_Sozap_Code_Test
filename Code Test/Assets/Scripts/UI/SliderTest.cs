using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour
{
    private int playerCount;
    private Slider mySlider = null;

    private void Awake()
    {
        mySlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCount = GameManager.instance.playerCount;

        mySlider.value = playerCount;
    }
}
