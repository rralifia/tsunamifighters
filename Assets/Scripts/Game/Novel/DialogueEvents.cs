using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents : MonoBehaviour {

	//Handle an event on a line segment.
	public static void HandleEvent(string _event, CLM.LINE.SEGMENT segment)
	{
		if (_event.Contains("("))
		{
			//get all actions delimitted by a comma
			string[] actions = _event.Split(' ');
			for(int i = 0; i < actions.Length; i++)
			{
				NovelController.instance.HandleAction(actions[i]);
			}
			return;
		}

		string[] eventData = _event.Split(' ');

		switch(eventData[0])
		{
		case "txtSpd":
			EVENT_TxtSpd(eventData[1], segment);
			break;
		case "/txtSpd":
			segment.architect.speed = 1;
			segment.architect.charactersPerFrame = 1;
			break;
		}
	}

	//Change the text speed of the segment's textArchitect.
	static void EVENT_TxtSpd(string data, CLM.LINE.SEGMENT seg)
	{
		string[] parts = data.Split(',');
		float delay = float.Parse(parts[0]);
		int charactersPerFrame = int.Parse(parts[1]);

		seg.architect.speed = delay;
		seg.architect.charactersPerFrame = charactersPerFrame;
	}
}
