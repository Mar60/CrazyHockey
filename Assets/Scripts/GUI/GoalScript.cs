using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

    
    private int cpt;
    private GameObject Ball;


    public AudioClip soundGoalClip;
    private AudioSource soundGoalSource;
    // Use this for initialization
    void Start () {
        cpt = 0;
        Ball = GameObject.Find("Ball");
        soundGoalSource = CreateSound(soundGoalClip);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private AudioSource CreateSound(AudioClip clip)
    {
        //Création de la souvelle source audio et configuration de ses propriétés
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = clip;
        source.volume = 10;
        source.loop = false;
        return source;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            cpt++;
            soundGoalSource.Play();
            Ball.GetComponent<Control>().resetTimerBall();
        }
    }

    public int score()
    {
        return cpt;
    }
}
