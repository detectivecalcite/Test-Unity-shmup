using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
	public Vector2 speed = new Vector2(2, 2);
	public Vector2 direction = new Vector2(-1, 0);

	public bool isLinkedToCamera = false;
	public bool isLooping = false;

	private List<Transform> backgroundPart;

	void Start()
	{
		// for infinite background only
		if (isLooping)
		{
			// get all the children of the layer with a renderer
			backgroundPart = new List<Transform>();

			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);

				// only add visible children
				if (child.renderer != null)
				{
					backgroundPart.Add(child);
				}
			}

			backgroundPart = backgroundPart.OrderBy(
				t => t.position.x
			).ToList ();
		}
	}

	void Update()
	{
		Vector3 movement = new Vector3(
			speed.x * direction.x,
			speed.y * direction.y,
			0);

		movement *= Time.deltaTime;
		transform.Translate(movement);

		// move the camera
		if (isLinkedToCamera)
		{
			Camera.main.transform.Translate(movement);
		}

		// loop
		if (isLooping)
		{
			// get first object
			// ordered l to r by x position
			Transform firstChild = backgroundPart.FirstOrDefault();
		
			if (firstChild != null)
			{
				// check if the chlid is already (partly) before the camera
				if (firstChild.position.x < Camera.main.transform.position.x)
				{
					// if the child is already on the left of the camera
					if (firstChild.renderer.IsVisibleFrom(Camera.main) == false)
					{
						// get the last child position
						Transform lastChild = backgroundPart.LastOrDefault();
						Vector3 lastPosition = lastChild.transform.position;
						Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

						// set the last position of the recycled one to be after the last child
						// note: only works for horizontal scrolling currently
						firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

						// set the recycled child to the last position of the backgroundPart list
						backgroundPart.Remove(firstChild);
						backgroundPart.Add (firstChild);
					}
				}
			}
		}
	}
}