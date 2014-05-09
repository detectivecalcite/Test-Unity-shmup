using UnityEngine;

public class MenuScript : MonoBehaviour
{
	void OnGUI()
	{
		const int buttonWidth = 84;
		const int buttonHeight = 60;

		if (
			GUI.Button (
				new Rect(
					Screen.width / 2 - (buttonWidth / 2),
					(2 * Screen.height / 3) - (buttonHeight / 2),
					buttonHeight,
					buttonWidth
				),
				"Start!"
			)
		)
		{
			// on lick, load first level
			// "scene1"
			Application.LoadLevel ("scene1");
		}
	}
}