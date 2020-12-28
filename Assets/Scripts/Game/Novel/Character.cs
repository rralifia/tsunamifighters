﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character 
{
	public string characterName;

    /// <summary>
    /// the actual name that is displayed when this character speaks. By default it is set to the true character name.
    /// </summary>
    public string displayName = "";

	/// <summary>
	/// The root is the container for all images related tot he character in the scene. The root object.
	/// </summary>
	[HideInInspector]public RectTransform root;

	public bool enabled {get{ return root.gameObject.activeInHierarchy;} set{ root.gameObject.SetActive (value);}}

	/// <summary>
	/// The space between the anchors of this character. Defines how much space a character takes up on the canvas.
	/// </summary>
	/// <value>The anchor padding.</value>
	public Vector2 anchorPadding {get{ return root.anchorMax - root.anchorMin;}}

	DialogueSystem dialogue;

	/// <summary>
	/// Make this character say something.
	/// </summary>
	/// <param name="speech">Speech.</param>
	public void Say(string speech, bool add = false)
	{
		if (!enabled)
			enabled = true;

        if (!isInScene)
            FadeIn();

		//get the details for the dialogue system related to this character. details are saved on file.
		//CharacterDialogueDetails.CDD c = CharacterDialogueDetails.instance.GetDetailsForCharacter(characterName);
		//Debug.Log(characterName);

		dialogue.Say(speech, displayName, add);//, c);
	}

    public Vector2 _targetPosition
    {
        get { return targetPosition; }
    }
	Vector2 targetPosition;
	Coroutine moving;
	bool isMoving{get{ return moving != null;}}
	/// <summary>
	/// Move to a specific point relative to the canvas space. (1,1) = far top right, (0,0) = far bottom left, (0.5,0.5) = Middle.
	/// </summary>
	/// <param name="Target">Target.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="smooth">If set to <c>true</c> smooth.</param>
	public void MoveTo(Vector2 Target, float speed, bool smooth = true)
	{
        Debug.Log("move " + characterName + " to " + Target.ToString());
		//if we are moving, stop moving
		StopMoving ();
		//start moving coroutine.
		moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth)); 
	}

	/// <summary>
	/// Stops the character in its tracks, either setting it immediately at the target position or not.
	/// </summary>
	/// <param name="arriveAtTargetPositionImmediately">If set to <c>true</c> arrive at target position immediately.</param>
	public void StopMoving(bool arriveAtTargetPositionImmediately = false)
	{
		if (isMoving) 
		{
			CharacterManager.instance.StopCoroutine (moving);
			if (arriveAtTargetPositionImmediately)
				SetPosition (targetPosition);
		}
		moving = null;
	}

	/// <summary>
	/// Immediately set the position of this character to the intended target.
	/// </summary>
	/// <param name="target">Target.</param>
	public void SetPosition(Vector2 target)
	{
		targetPosition = target;

		Vector2 padding = anchorPadding;
		float maxX = 1f - padding.x;
		float maxY = 1f - padding.y;

		Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);

		root.anchorMin = minAnchorTarget;
		root.anchorMax = root.anchorMin + padding;
	}

	/// <summary>
	/// The coroutine that runs to gradually move the character towards a position.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="smooth">If set to <c>true</c> smooth.</param>
	IEnumerator Moving(Vector2 target, float speed, bool smooth)
	{
		targetPosition = target;

		//now we want to get the padding between the anchors of this character so we know what their min and max positions are.
		Vector2 padding = anchorPadding;

		//now get the limitations for 0 to 100% movement. The farthest a character can move to the right before reaching 100% should be the 1 value - the padding (thickness of the character) so that 100% to the right/up does not place them outside of the canvas.
		float maxX = 1f - padding.x;
		float maxY = 1f - padding.y;

		//now get the actual position target for the minimum anchors (left/bottom bounds) of the character. because maxX and maxY is just a percent reference.
		Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
		speed *= Time.deltaTime;

		//move until we reach the target position.
		while (root.anchorMin != minAnchorTarget) 
		{
			root.anchorMin = (!smooth) ? Vector2.MoveTowards (root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp (root.anchorMin, minAnchorTarget, speed);
			root.anchorMax = root.anchorMin + padding;
			yield return new WaitForEndOfFrame ();
		}

		StopMoving ();
	}

	//Begin Transitioning Images\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
	public Sprite GetSprite(int index = 0)
	{
		Sprite[] sprites = Resources.LoadAll<Sprite> ("Images/Characters/" + characterName);
		return sprites[index];
	}

	public Sprite GetSprite(string spriteName = "")
	{
		Sprite[] sprites = Resources.LoadAll<Sprite> ("Images/Characters/" + characterName);
		for(int i = 0; i < sprites.Length; i++)
		{
			if (sprites[i].name == spriteName)
				return sprites[i];
		}
		return sprites.Length > 0 ? sprites[0] : null;
	}

	public void SetExpression(int index)
	{
		renderers.expressionRenderer.sprite = GetSprite (index);
	}
	public void SetExpression(Sprite sprite)
	{
		renderers.expressionRenderer.sprite = sprite;
	}
	public void SetExpression(string spriteName)
	{
        if (spriteName == "AlphaOnly")
            SetExpression(Resources.Load<Sprite>("Images/AlphaOnly"));
        else
		    renderers.expressionRenderer.sprite = GetSprite (spriteName);
	}

	//Transition Expression
	bool isTransitioningExpression {get{ return transitioningExpression != null;}}
	Coroutine transitioningExpression = null;

	public void TransitionExpression(Sprite sprite, float speed, bool smooth)
	{
		StopTransitioningExpression ();
		transitioningExpression = CharacterManager.instance.StartCoroutine (TransitioningExpression (sprite, speed, smooth));
	}

	void StopTransitioningExpression()
	{
		if (isTransitioningExpression)
			CharacterManager.instance.StopCoroutine (transitioningExpression);
		transitioningExpression = null;
	}

	public IEnumerator TransitioningExpression(Sprite sprite, float speed, bool smooth)
	{
		for (int i = 0; i < renderers.allExpressionRenderers.Count; i++) 
		{
			Image image = renderers.allExpressionRenderers [i];
			if (image.sprite == sprite) 
			{
				renderers.expressionRenderer = image;
				break;
			}
		}

		if (renderers.expressionRenderer.sprite != sprite) 
		{
			Image image = GameObject.Instantiate (renderers.expressionRenderer.gameObject, renderers.expressionRenderer.transform.parent).GetComponent<Image> ();
			renderers.allExpressionRenderers.Add (image);
			renderers.expressionRenderer = image;
			image.color = GlobalF.SetAlpha (image.color, 0f);
			image.sprite = sprite;
		}

		while (GlobalF.TransitionImages (ref renderers.expressionRenderer, ref renderers.allExpressionRenderers, speed, smooth, true))
			yield return new WaitForEndOfFrame ();

		StopTransitioningExpression ();
	}
		
	//End Transition Images\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
	public void Flip()
	{
		root.localScale = new Vector3(root.localScale.x * -1, 1, 1);
	}

	/*public bool isFacingLeft {get{return root.localScale.x == 1;}}
	public void FaceLeft()
	{
		root.localScale = Vector3.one;
	}

	public bool isFacingRight {get{return root.localScale.y == -1;}}
	public void FaceRight()
	{
		root.localScale = new Vector3(-1, 1, 1);
	}*/

	public void FadeOut(float speed = 3, bool smooth = false)
	{
        if (isEnteringOrExitingScene)
            CharacterManager.instance.StopCoroutine(enteringExiting);

        enteringExiting = CharacterManager.instance.StartCoroutine(ExitingScene(speed, smooth));
	}

    public bool isInScene = false;
    Coroutine enteringExiting = null;
    public bool isEnteringOrExitingScene{get{return enteringExiting != null;}}

    IEnumerator EnteringScene(float speed = 3, bool smooth = false)
    {
        isInScene = true;

        while(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha = smooth ? Mathf.Lerp(canvasGroup.alpha, 1, speed * Time.deltaTime) : Mathf.MoveTowards(canvasGroup.alpha, 1, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        enteringExiting = null;
    }

    IEnumerator ExitingScene(float speed = 3, bool smooth = false)
    {
        isInScene = false;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha = smooth ? Mathf.Lerp(canvasGroup.alpha, 0, speed * Time.deltaTime) : Mathf.MoveTowards(canvasGroup.alpha, 0, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        enteringExiting = null;

        //character is completely faded out and exited the scene. Destroy it so it is no longer saved to file until recalled.
        CharacterManager.instance.DestroyCharacter(this);
    }

	Sprite lastBodySprite, lastFacialSprite = null;
	public void FadeIn(float speed = 3, bool smooth = false)
	{
        if (isEnteringOrExitingScene)
            CharacterManager.instance.StopCoroutine(enteringExiting);

        enteringExiting = CharacterManager.instance.StartCoroutine(EnteringScene(speed, smooth));
	}

    public CanvasGroup canvasGroup;

	/// <summary>
	/// Create a new character.
	/// </summary>
	/// <param name="_name">Name.</param>
	public Character (string _name, bool enableOnStart = true)
	{
		CharacterManager cm = CharacterManager.instance;
		//locate the character prefab.
		GameObject prefab = Resources.Load ("Characters/Character[" + _name + "]") as GameObject;
		//spawn an instance of the prefab directly on the character panel.
		GameObject ob = GameObject.Instantiate (prefab, cm.characterPanel);

		root = ob.GetComponent<RectTransform> ();
        canvasGroup = ob.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

		characterName = _name;
        displayName = characterName;

		//get the renderer(s)
		renderers.expressionRenderer = ob.transform.Find ("ExpressionLayer").GetComponentInChildren<Image> ();
		renderers.allExpressionRenderers.Add (renderers.expressionRenderer);

		dialogue = DialogueSystem.instance;

		enabled = enableOnStart;
	}

	[System.Serializable]
	public class Renderers
	{
		//sprites use images.
		/// <summary>
		/// The expression renderer for a multi layer character.
		/// </summary>
		public Image expressionRenderer;

		public List<Image> allExpressionRenderers = new List<Image> ();
	}

	public Renderers renderers = new Renderers();
}