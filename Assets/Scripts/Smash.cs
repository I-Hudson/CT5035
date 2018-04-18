using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
	public float breakVelocity;
	public GameObject brokenPrefab;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > breakVelocity || GetComponent<Rigidbody>().velocity.magnitude > breakVelocity)
		{
			//Smash!
			Destroy(gameObject);
			GameObject smashedItem = Instantiate(brokenPrefab, transform.position, transform.rotation, transform.parent);
			
		}
	}

}
