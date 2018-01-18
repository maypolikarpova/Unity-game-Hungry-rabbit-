using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnRabitHit(HeroRabbit rabit)
    {}

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabbit rabit = collider.gameObject.GetComponent<HeroRabbit>();
        if (rabit != null)
        {
            this.OnRabitHit(rabit);
        }
    }

    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}
