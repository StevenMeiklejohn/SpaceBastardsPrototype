using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour {

    //StarGO prefab
    public GameObject StarGO;

    //Max number of stars
    public int MaxStars;

    //Array of colours
    Color[] starColors = {
        //blue
        new Color(0.5f, 0.5f, 1f),
        //green
        new Color(0, 1f, 1f),
        //yellow
        new Color(1f, 1f, 0),
        //red
        new Color(1f, 0, 0)
    };

    // Use this for initialization
    void Start()
    {
        //bottom left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //top right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Loop to create the stars.
        for (int i = 0; i < MaxStars; i++)
        {
            GameObject star = (GameObject)Instantiate(StarGO);

            //set the star color
            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            //set the position of the star (randsom x and random y)
            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            //set a random speed for the star.
            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

            //make the star a child of the StarGeneratorGO.
            star.transform.parent = transform;

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
