using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NovelController : MonoBehaviour
{
	public static NovelController instance;
	public TextMeshProUGUI turnStatus;
	public GameObject EndScreen, ScoreScreen, chatCanvas, disconnectedDataPanel, DisconnectedPanel, backHomePanel, jijoInfo, kyojoInfo, kojoInfo, warningPanel, chatNotif, jijoEngIcon, kyojoEngIcon, kojoEngIcon, jijoIndIcon, kyojoIndIcon, kojoIndIcon;
	[HideInInspector]
	public string activeGameFileName = "";
	//public CanvasGroup nextButton;//, nextField;
	private bool myTurn = false;

	//The lines of data loaded directly from a chapter file.
	List<string> data = new List<string>();

	void Awake()
	{
		instance = this;
	}

	GAMEFILE activeGameFile
	{
		get { return GAMEFILE.activeFile; }
		set { GAMEFILE.activeFile = value; }
	}

	string activeChapterFile = "";
	public static string idA, idB;
	public static int idNetwork;
	public Button nextButton, chatButton, retryButton, backHomeButton, closeButton, homeButton, backNovelMenuButton, closeJijo, closeKyojo, closeKojo;

	// Use this for initialization
	void Start()
	{
		Debug.Log("Start Novel Controller..");
		PlayerPrefs.SetString("play_idA", "0");
		PlayerPrefs.SetString("play_idB", "0");
		PlayerPrefs.SetString("multiplayIdB", "0");
		PlayerPrefs.SetInt("SceneNumber", 6);
		int scene = PlayerPrefs.GetInt("SceneNumber");
		Debug.Log("Scene Number in Novel: " + scene);

		print("Play ID from ScoresInfo in NovelController: " + ScoresInfo.play_id);
		Debug.Log("Photon player ID in SyncID: " + PhotonNetwork.player.ID);
		//Send play_id to all players in the room
		if (PhotonNetwork.player.ID == 1)
		{
			idNetwork = 1;
			idA = ScoresInfo.play_id;
			PlayerNetwork.instance.Send_idA(idA);
		}
		else if (PhotonNetwork.player.ID == 2)
		{
			idNetwork = 2;
			idB = ScoresInfo.play_id;
			PlayerNetwork.instance.Send_idB(idB);
		}

		if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
		{
			Main.Instance.Multiplay.Disconnected();
		}

		chatCanvas.gameObject.SetActive(false);
		chatButton.onClick.AddListener(Load_Chat);
		backHomeButton.onClick.AddListener(BackHomePanel);
		backNovelMenuButton.onClick.AddListener(BackNovelMenu);

		//LoadGameFile
		//LoadGameFile(FileManager.LoadFile(FileManager.savPath + "savData/file.txt")[0]);
		//LoadChapterFile("chap0");
		if(PhotonNetwork.player.ID == 1)
			StartCoroutine(SyncStory());
		else
			LoadChapterFile("chap0");

		if(UserInfo.language == 1)
        {
			jijoEngIcon.SetActive(true);
			kyojoEngIcon.SetActive(true);
			kojoEngIcon.SetActive(true);
		}
		else if (UserInfo.language == 2)
		{
			jijoIndIcon.SetActive(true);
			kyojoIndIcon.SetActive(true);
			kojoIndIcon.SetActive(true);
		}

		Main.Instance.Multiplay.SetMultiplay("0");
	}

	private void Update()
	{
		//Shows the Disconnected Panel if the player disconnected from the photon server
		if (!PhotonNetwork.connected && !PhotonNetwork.connecting && backNovelMenu == false)
        {
            this.DisconnectedPanel.gameObject.SetActive(true);
        }
	}

	IEnumerator SyncStory()
    {
		print("Start delay for SyncStory: " + Time.time);
		yield return new WaitForSeconds(3.0f);
		LoadChapterFile("chap0");
		//nextButton.interactable = true;
		print("End delay for SyncStory: " + Time.time);
	}

	//Called when any player in the room disconnected from the photon server
	private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        if(backNovelMenu == false)
			this.DisconnectedPanel.gameObject.SetActive(true);
	}

	//bool encryptGameFile = true;
	/*
	public void LoadGameFile(string gameFileName)
	{
		activeGameFileName = gameFileName;

		string filePath = FileManager.savPath + "savData/gameFiles/" + activeGameFileName + ".txt";

		if (!System.IO.File.Exists(filePath))
		{
			activeGameFile = new GAMEFILE();//don't save because we want a new one to start whenever we hit new game. any save is manual.
		}
		else
		{
			if (encryptGameFile)
				activeGameFile = FileManager.LoadEncryptedJSON<GAMEFILE>(filePath, keys);
			else
				activeGameFile = FileManager.LoadJSON<GAMEFILE>(filePath);
		}

		//Load the file. Anything in the Resources folder should be loaded by Resources only.
		TextAsset chapterAsset = Resources.Load<TextAsset>("Story/" + activeGameFile.chapterName);
		data = FileManager.ReadTextAsset(chapterAsset);
		activeChapterFile = activeGameFile.chapterName;
		cachedLastSpeaker = activeGameFile.cachedLastSpeaker;

		DialogueSystem.instance.Open(activeGameFile.currentTextSystemSpeakerNameText, activeGameFile.currentTextSystemDisplayText);

		//Load all characters back into the scene
		for (int i = 0; i < activeGameFile.charactersInScene.Count; i++)
		{
			GAMEFILE.CHARACTERDATA data = activeGameFile.charactersInScene[i];
			Character character = CharacterManager.instance.CreateCharacter(data.characterName, data.enabled);
			character.displayName = data.displayName;
			character.canvasGroup.alpha = 1;//any saved character should start with an alpha of 1 since they already entered at one point in time and any character that should have an alpha of zero would already have been removed.							
			character.SetExpression(data.facialExpression);
			/*if (data.facingLeft)
				character.FaceLeft();
			else
				character.FaceRight();
			character.SetPosition(data.position);
		}

		//Load the layer images back into the scene
		if (activeGameFile.background != null)
			BCFC.instance.background.SetTexture(activeGameFile.background);

		//start the music back up
		if (activeGameFile.music != null)
			AudioManager.instance.PlaySong(activeGameFile.music);

		//start the ambiance back up
		if (activeGameFile.ambiance.Count > 0)
		{
			foreach (AudioClip clip in activeGameFile.ambiance)
			{
				AudioManager.instance.PlayAmbiance(clip);
			}
		}

		//load the temporary variables back
		CACHE.tempVals = activeGameFile.tempVals;

		if (handlingChapterFile != null)
			StopCoroutine(handlingChapterFile);
		handlingChapterFile = StartCoroutine(HandlingChapterFile());

		chapterProgress = activeGameFile.chapterProgress;
	}

	public void SaveGameFile()
	{
		string filePath = FileManager.savPath + "savData/gameFiles/" + activeGameFileName + ".txt";

		activeGameFile.chapterName = activeChapterFile;
		activeGameFile.chapterProgress = chapterProgress;
		activeGameFile.cachedLastSpeaker = cachedLastSpeaker;

		activeGameFile.currentTextSystemSpeakerNameText = DialogueSystem.instance.speakerNameText.text;
		activeGameFile.currentTextSystemDisplayText = DialogueSystem.instance.speechText.text;

		//get all the characters and save their stats.
		activeGameFile.charactersInScene.Clear();
		for (int i = 0; i < CharacterManager.instance.characters.Count; i++)
		{
			Character character = CharacterManager.instance.characters[i];
			GAMEFILE.CHARACTERDATA data = new GAMEFILE.CHARACTERDATA(character);
			activeGameFile.charactersInScene.Add(data);
		}

		//save the layers to disk
		BCFC b = BCFC.instance;
		activeGameFile.background = b.background.activeImage != null ? b.background.activeImage.texture : null;

		//save the music to disk
		activeGameFile.music = AudioManager.activeSong != null ? AudioManager.activeSong.clip : null;

		//save the ambiance to disk if there is any playing.
		activeGameFile.ambiance = AudioManager.activeAmbianceClips;

		//save the tempvals to disk. for easy variable storage.
		activeGameFile.tempVals = CACHE.tempVals;

		//save a preview image (screenshot) to be viewed from the save load screen
		string screenShotPath = FileManager.savPath + "savData/gameFiles/" + activeGameFileName + ".png";
		if (FileManager.TryCreateDirectoryFromPath(screenShotPath + ".png"))
		{
			GAMEFILE.activeFile.previewImage = ScreenCapture.CaptureScreenshotAsTexture();
			byte[] textureData = activeGameFile.previewImage.EncodeToPNG();
			FileManager.SaveComposingBytes(screenShotPath, textureData);
		}

		//save the data and time this file was created or modified.
		//activeGameFile.modificationDate = System.DateTime.Now.ToString();

		if (encryptGameFile)
			FileManager.SaveEncryptedJSON(filePath, activeGameFile, keys);
		else
			FileManager.SaveJSON(filePath, activeGameFile);
	}

	byte[] keys
	{
		get { return FileManager.keys; }
	}*/

	public void LoadChapterFile(string fileName)
	{
		activeChapterFile = fileName;

		if (UserInfo.language == 1)
        {
			data = FileManager.ReadTextAsset(Resources.Load<TextAsset>($"Story/Eng/{fileName}"));
		}
		else if(UserInfo.language == 2)
		{
			data = FileManager.ReadTextAsset(Resources.Load<TextAsset>($"Story/Ind/{fileName}"));
		}
		else if (UserInfo.language == 3)
		{
			data = FileManager.ReadTextAsset(Resources.Load<TextAsset>($"Story/Jpn/{fileName}"));
		}
		cachedLastSpeaker = "";

		if (handlingChapterFile != null)
			StopCoroutine(handlingChapterFile);
		handlingChapterFile = StartCoroutine(HandlingChapterFile());

		//auto start the chapter.
		Next();
	}

	/// Trigger that advances the progress through a chapter file.
	bool _next = false;

	/// Procede to the next line of a chapter or finish the line right now.
	public void Next()
	{
		_next = true;
		nextButton.interactable = false;
		//nextField.interactable = false;
	}

	public bool isHandlingChapterFile { get { return handlingChapterFile != null; } }
	Coroutine handlingChapterFile = null;
	[HideInInspector] public int chapterProgress = 0;
	IEnumerator HandlingChapterFile()
	{
		//the progress through the lines in this chapter.
		chapterProgress = 0;

		while (chapterProgress < data.Count)
		{
			//we need a way of knowing when the player wants to advance. We need a "next" trigger. Not just a keypress. But something that can be triggerd
			//by a click or a keypress
			if (_next)
			{
				string line = data[chapterProgress];//this is the line loaded in its pure format. No injections have taken place yet.
													//make sure the line has the proper data injected in it where it needs it.
				TagManager.Inject(ref line);//inject data into the line where it may be needed.
											//now our line will be properly formatted and ready to use.

				//this is a choice
				if (line.StartsWith("choice"))
				{
					yield return HandlingChoiceLine(line);
					chapterProgress++;
				}
				else
				{
					HandleLine(line);
					print("line" + line);
					chapterProgress++;
					while (isHandlingLine)
					{
						yield return new WaitForEndOfFrame();
					}
				}
			}
			yield return new WaitForEndOfFrame();
		}

		handlingChapterFile = null;
	}

	IEnumerator HandlingChoiceLine(string line)
	{
		string title = line.Split('"')[1];
		List<string> choices = new List<string>();
		List<string> actions = new List<string>();

		bool gatheringChoices = true;
		while (gatheringChoices)
		{
			chapterProgress++;
			line = data[chapterProgress];

			if (line == "{")
				continue;

			line = line.Replace("    ", "");//remove the tabs that have become quad spaces.

			if (line != "}")
			{
				choices.Add(line.Split('"')[1]);
				actions.Add(data[chapterProgress + 1].Replace("    ", ""));
				chapterProgress++;
			}
			else
			{
				gatheringChoices = false;
			}
		}

		//display choices
		if (choices.Count > 0 && myTurn)
		{
			ChoiceScreen.Show(title, choices.ToArray()); yield return new WaitForEndOfFrame();
			while (ChoiceScreen.isWaitingForChoiceToBeMade)
				yield return new WaitForEndOfFrame();

			//choice is made. execute the paired action.
			string action = actions[ChoiceScreen.lastChoiceMade.index];
			/*if (UserInfo.language == 1)
				turnStatus.text = "Discuss your answer with your friends!";
			else if (UserInfo.language == 2)
				turnStatus.text = "Diskusikan jawabanmu dengan temanmu!";
			else if (UserInfo.language == 3)
				turnStatus.text = "Discuss your answer with your friends!";*/
			PlayerNetwork.instance.Sync_ChoiceAction(action);
			while (isHandlingLine)
				yield return new WaitForEndOfFrame();
        }
	}

	public void HandleLine(string rawLine)
	{
		CLM.LINE line = CLM.Interpret(rawLine);

		//now we need to handle the line. This requires a loop full of waiting for input since the line consists of multiple segments that have to be
		//handled individually.
		StopHandlingLine();
		handlingLine = StartCoroutine(HandlingLine(line));
	}

	void StopHandlingLine()
	{
		if (isHandlingLine)
			StopCoroutine(handlingLine);
		handlingLine = null;
	}

	[HideInInspector]
	//Used as a fallback when no speaker is given.
	public string cachedLastSpeaker = "";

	public bool isHandlingLine { get { return handlingLine != null; } }
	Coroutine handlingLine = null;
	IEnumerator HandlingLine(CLM.LINE line)
	{
		//since the "next" trigger controls the flow of a chapter by moving through lines and yet also controls the progression through a line by
		//its segments, it must be reset.
		_next = false;
		int lineProgress = 0;//progress through the segments of a line.

		while (lineProgress < line.segments.Count)
		{
			_next = false;//reset at the start of each loop.
			CLM.LINE.SEGMENT segment = line.segments[lineProgress];
			print("progress" + lineProgress);
			//always run the first segment automatically. But wait for the trigger on all proceding segments.
			if (lineProgress > 0)
			{
				if (segment.trigger == CLM.LINE.SEGMENT.TRIGGER.autoDelay)
				{
					for (float timer = segment.autoDelay; timer >= 0; timer -= Time.deltaTime)
					{
						yield return new WaitForEndOfFrame();
						if (_next)
							break;//allow the termination of a delay when "next" is triggered. Prevents unskippable wait timers.
					}
				}
				else
				{
					while (!_next)
						yield return new WaitForEndOfFrame();//wait until the player says move to the next segment.
				}
			}
			_next = false;//next could have been triggered during an event above.

			//the segment now needs to build and run.
			segment.Run();

			while (segment.isRunning)
			{
				yield return new WaitForEndOfFrame();
				//allow for auto completion of the current segment for skipping purposes.
				if (_next)
				{
					//rapidly complete the text on first advance, force it to finish on the second.
					if (!segment.architect.skip)
						segment.architect.skip = true;
					else
						segment.ForceFinish();
					_next = false;
				}
			}

			lineProgress++;

			yield return new WaitForEndOfFrame();
		}

		//Line is finished. Handle all the actions set at the end of the line.
		for (int i = 0; i < line.actions.Count; i++)
		{
			HandleAction(line.actions[i]);
			print("lineaction" + line.actions[i]);
		}

		handlingLine = null;
	}

	public static string playidA, playidB;
	string multiplayIdB = "0";
	//ACTIONS
	//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
	public void HandleAction(string action)
	{
		//print("execute command - " + action);
		string[] data = action.Split('(', ')');
		//Run action based on the key command
		switch (data[0])
		{
			case "enter":
				Command_Enter(data[1]);
				Debug.Log("enter" + data[1]);
				break;

			case "exit":
				Command_Exit(data[1]);
				Debug.Log("exit" + data[1]);
				break;

			/*case "setBackground":
				Command_SetLayerImage(data[1], BCFC.instance.background);
				break;

			case "playSound":
				Command_PlaySound(data[1]);
				break;*/

			case "playMusic":
				Command_PlayMusic(data[1]);
				break;

			/*case "playAmbiance":
				Command_PlayAmbiance(data[1]);
				break;

			case "stopAmbiance":
				if (data[1] != "")
					Command_StopAmbiance(data[1]);
				else
					Command_StopAllAmbiance();
				break;*/

			case "move":
				Command_MoveCharacter(data[1]);
				break;

			case "setPosition":
				Command_SetPosition(data[1]);
				Debug.Log("setPos" + data[1]);
				break;

			case "setFace":
				Command_SetFace(data[1]);
				Debug.Log("setFace" + data[1]);
				break;

			case "flip":
				Command_Flip(data[1]);
				Debug.Log("flip" + data[1]);
				break;

			case "transBackground":
				Command_TransLayer(BCFC.instance.background, data[1]);
				Debug.Log("transBG" + data[1]);
				break;

			case "showScene":
				Command_ShowScene(data[1]);
				Debug.Log("showSc" + data[1]);
				break;

			case "Load":
				Command_Load(data[1]);
				Debug.Log("load" + data[1]);
				break;

			case "next":
				Next();
				Debug.Log("next");
				break;

			case "score":
				Count_Score(data[1]);
				break;

			case "1":
				if (PhotonNetwork.player.ID != 1)
				{
					myTurn = false;
				}
				else
				{
					myTurn = true;
				}
				//player1Turn();
				Invoke("player1Turn", 1.0f);
				print(myTurn);
				break;

			case "2":
				if (PhotonNetwork.player.ID == 1)
				{
					myTurn = false;
				}
				else
				{
					myTurn = true;
				}
				//player2Turn();
				Invoke("player2Turn", 1.0f);
				print(myTurn);
				break;

			case "1c":
				if (PhotonNetwork.player.ID == 1)
					myTurn = true;
                else
					myTurn = false;
				Next();
				if (PhotonNetwork.player.ID == 1)
                {
					if (UserInfo.language == 1)
						turnStatus.text = "Confused? Discuss it with your friends!";
					else if (UserInfo.language == 2)
						turnStatus.text = "Bingung? Diskusikan dengan temanmu!";
					else if (UserInfo.language == 3)
						turnStatus.text = "Confused? Discuss it with your friends!";
				}
                else
                {
					if (UserInfo.language == 1)
						turnStatus.text = "Player 1 is answering question";
					else if (UserInfo.language == 2)
						turnStatus.text = "Pemain 1 menjawab pertanyaan";
					else if (UserInfo.language == 3)
						turnStatus.text = "Player 1 is answering question";
				}
				print(myTurn);
				break;

			case "2c":
				if (PhotonNetwork.player.ID != 1)
					myTurn = true;
				else
					myTurn = false;
				Next();
				if (PhotonNetwork.player.ID != 1)
				{
					if (UserInfo.language == 1)
						turnStatus.text = "Confused? Discuss it with your friends!";
					else if (UserInfo.language == 2)
						turnStatus.text = "Bingung? Diskusikan dengan temanmu!";
					else if (UserInfo.language == 3)
						turnStatus.text = "Confused? Discuss it with your friends!";
				}
				else
                {
					if (UserInfo.language == 1)
						turnStatus.text = "Player 2 is answering question";
					else if (UserInfo.language == 2)
						turnStatus.text = "Pemain 2 menjawab pertanyaan";
					else if (UserInfo.language == 3)
						turnStatus.text = "Player 2 is answering question";
				}
				print(myTurn);
				break;

			case "end":
				Command_End();
				break;

			case "id":
				playidA = PlayerPrefs.GetString("play_idA");
				playidB = PlayerPrefs.GetString("play_idB");
				Debug.Log("Novel Controller - PlayIdA: " + playidA + " and PlayIdB: " + playidB);

                if (UserInfo.language == 1)
                {
					turnStatus.text = "Hello, " + playidA + " " + playidB;
				} else if (UserInfo.language == 2)
                {
					turnStatus.text = "Halo, " + playidA + " " + playidB;
				} else if (UserInfo. language ==3)
                {
					turnStatus.text = "Hello, " + playidA + " " + playidB;
				}
                    
				Debug.Log("PhotonNetwork playerID in Multiplay: " + PhotonNetwork.player.ID);
				if (PhotonNetwork.player.ID == 1)
				{
					StartCoroutine(Main.Instance.Web.AddMultiplay(playidA, playidB));
					Debug.Log("Multiplay ID for PlayID (" + playidA + ") has successfully added.");
					Debug.Log("Multiplay ID in HandleAction 'id': " + Multiplay.multiplay_id);
					StartCoroutine(TransferMultiplayID(3.5f));
					//TransferMultiplayID();
				} else if (PhotonNetwork.player.ID == 2)
				{
					Debug.Log("Multiplay ID for PlayID (" + playidB + ") has not yet added.");
				}
                break;

			case "multiplay":
				Debug.Log("PhotonNetwork playerID in Multiplay: " + PhotonNetwork.player.ID);
				Debug.Log("Multiplay ID before multiplay: " + Multiplay.multiplay_id);
				Debug.Log("MultiplayIdB before multiplay: " + multiplayIdB);
                if (PhotonNetwork.player.ID == 2)
				{
					if ((multiplayIdB == "0") && (Multiplay.multiplay_id == "0"))
                    {
						Debug.Log("Getting Multiplay Id B...");
						//multiplayIdB = PlayerPrefs.GetString("multiplayIdB");
						StartCoroutine(MultiplayIdB(4.0f));
					} else if ((multiplayIdB != "0") && (Multiplay.multiplay_id == "0"))
                    {
						Main.Instance.Multiplay.SetMultiplay(multiplayIdB);
					} else // multiplay id B != 0, multiplay_id != 0
                    {
						Debug.Log("Multiplay ID for Player2: " + Multiplay.multiplay_id);
                    }
				} else if (PhotonNetwork.player.ID == 1)
                {
                    if ((Multiplay.multiplay_id == "0") && (multiplayIdB == "0"))
                    {
						Debug.Log("Multiplay ID & Multiplay ID B = 0");
                        StartCoroutine(Main.Instance.Web.AddMultiplay(playidA, playidB));
					} else if ((Multiplay.multiplay_id != "0") && (multiplayIdB == "0"))
                    {
						Debug.Log("Only MultiplayIdB = 0");
                        StartCoroutine(TransferMultiplayID(3.5f));
					} else // multiplay id B != 0, multiplay_id != 0
					{
						Debug.Log("Multiplay ID for Player1: " + Multiplay.multiplay_id);
					}
                }
				break;

			case "chat":
				chatButton.interactable = true;
				break;
		}
	}

    public IEnumerator MultiplayIdB (float delayInSecs)
    {
		yield return new WaitForSeconds(delayInSecs);
		multiplayIdB = PlayerPrefs.GetString("multiplayIdB");
		Debug.Log("Novel Controller - multiplayIdB: " + multiplayIdB);
		Main.Instance.Multiplay.SetMultiplay(multiplayIdB);
	}

    public IEnumerator TransferMultiplayID (float delayInSecs)
    {
        print("Start delay for TransferMultiplayID: " + Time.time);
		yield return new WaitForSeconds(delayInSecs);
		PlayerNetwork.instance.Send_MultiplayId(Multiplay.multiplay_id);
		print("End delay for TransferMultiplayID: " + Time.time);
	}

	/*public void TransferMultiplayID()
	{
		//print("Start delay for TransferMultiplayID: " + Time.time);
		//yield return new WaitForSeconds(delayInSecs);
		PlayerNetwork.instance.Send_MultiplayId(Multiplay.multiplay_id);
		//print("End delay for TransferMultiplayID: " + Time.time);
	}*/

	//Run when the player has finished the visual novel game
	void Command_End()
	{
		EndScreen.SetActive(true);
		ScoreScreen.SetActive(false);
		if (idNetwork == 1)
		{
			Debug.Log(idNetwork + " is exporting Novel Scores in NovelController...");
            StartCoroutine(Main.Instance.Web.AddNovelScores(NovelController.jijoA, NovelController.kyojoA, NovelController.kojoA, NovelController.scoreA, ScoresInfo.play_id));
			/*StartCoroutine(Main.Instance.Web.AddJijo(ScoresInfo.play_id, NovelController.jijoA));
            StartCoroutine(Main.Instance.Web.AddKojo(ScoresInfo.play_id, NovelController.kojoA));
            StartCoroutine(Main.Instance.Web.AddKyojo(ScoresInfo.play_id, NovelController.kyojoA));
            StartCoroutine(Main.Instance.Web.AddGame(ScoresInfo.play_id, NovelController.scoreA));*/
		}
		else if (idNetwork == 2)
		{
			Debug.Log(idNetwork + " is exporting Novel Scores in NovelController...");
			StartCoroutine(Main.Instance.Web.AddNovelScores(NovelController.jijoB, NovelController.kyojoB, NovelController.kojoB, NovelController.scoreB, ScoresInfo.play_id));
			/*StartCoroutine(Main.Instance.Web.AddJijo(ScoresInfo.play_id, NovelController.jijoB));
            StartCoroutine(Main.Instance.Web.AddKojo(ScoresInfo.play_id, NovelController.kojoB));
            StartCoroutine(Main.Instance.Web.AddKyojo(ScoresInfo.play_id, NovelController.kyojoB));
            StartCoroutine(Main.Instance.Web.AddGame(ScoresInfo.play_id, NovelController.scoreB));*/
		}
	}

	/*void Command_PlayAmbiance(string ambianceTrackName)
	{
		//load the track
		AudioClip clip = Resources.Load<AudioClip>("Audio/Ambiance/" + ambianceTrackName);
		if (clip != null)
		{
			AudioManager.instance.PlayAmbiance(clip);
		}
	}

	void Command_StopAmbiance(string ambianceTrackName)
	{
		AudioManager.instance.StopAmbiance(ambianceTrackName);
	}

	void Command_StopAllAmbiance()
	{
		AudioManager.instance.StopAmbiance();
	}*/

	void Command_Load(string chapterName)
	{
		NovelController.instance.LoadChapterFile(chapterName);
	}

	/*void Command_SetLayerImage(string data, BCFC.LAYER layer)
	{
		string texName = data.Contains(",") ? data.Split(',')[0] : data;
		Texture2D tex = texName == "null" ? null : Resources.Load("Images/UI/Backdrops/" + texName) as Texture2D;
		float spd = 2f;
		bool smooth = false;

		if (data.Contains(","))
		{
			string[] parameters = data.Split(',');
			foreach (string p in parameters)
			{
				float fVal = 0;
				bool bVal = false;
				if (float.TryParse(p, out fVal))
				{
					spd = fVal; continue;
				}
				if (bool.TryParse(p, out bVal))
				{
					smooth = bVal; continue;
				}
			}
		}

		layer.TransitionToTexture(tex, spd, smooth);
	}

	void Command_PlaySound(string data)
	{
		AudioClip clip = Resources.Load("Audio/SFX/" + data) as AudioClip;

		if (clip != null)
			AudioManager.instance.PlaySFX(clip);
		else
			Debug.LogError("Clip does not exist - " + data);
	}*/

	void Command_PlayMusic(string data)
	{
		if (data.ToLower() == "null")
		{
			AudioManager.instance.PlaySong(null, VolumeSettings.musicFloat, VolumeSettings.musicFloat);
			Debug.Log("Play Null");
		}
		else
		{
			AudioClip clip = Resources.Load("Audio/Music/" + data) as AudioClip;
			Debug.Log("musicFloat in Command PlayMusic: " + VolumeSettings.musicFloat);
			AudioManager.instance.PlaySong(clip, VolumeSettings.musicFloat, VolumeSettings.musicFloat);
		}
		Debug.Log("Music: " + data);
	}

	void Command_MoveCharacter(string data)
	{
		string[] parameters = data.Split(',');
		string character = parameters[0];
		float locationX = float.Parse(parameters[1]);
		float locationY = parameters.Length >= 3 ? float.Parse(parameters[2]) : 0;
		float speed = parameters.Length >= 4 ? float.Parse(parameters[3]) : 7f;
		bool smooth = parameters.Length == 5 ? bool.Parse(parameters[4]) : true;

		Character c = CharacterManager.instance.GetCharacter(character);
		c.MoveTo(new Vector2(locationX, locationY), speed, smooth);
	}

	void Command_SetPosition(string data)
	{
		string[] parameters = data.Split(',');
		string character = parameters[0];
		float locationX = float.Parse(parameters[1]);
		float locationY = parameters.Length == 3 ? float.Parse(parameters[2]) : 0;

		Character c = CharacterManager.instance.GetCharacter(character);
		c.SetPosition(new Vector2(locationX, locationY));
	}

	void Command_SetFace(string data)
	{
		string[] parameters = data.Split(',');
		string character = parameters[0];
		string expression = parameters[1];
		float speed = parameters.Length == 3 ? float.Parse(parameters[2]) : 3f;

		Character c = CharacterManager.instance.GetCharacter(character);
		Sprite sprite = c.GetSprite(expression);

		c.TransitionExpression(sprite, speed, false);
	}

	void Command_Flip(string data)
	{
		string[] characters = data.Split(';');

		foreach (string s in characters)
		{
			Character c = CharacterManager.instance.GetCharacter(s);
			c.Flip();
		}
	}

	void Command_Exit(string data)
	{
		string[] parameters = data.Split(',');
		string[] characters = parameters[0].Split(';');
		float speed = 3;
		bool smooth = false;
		for (int i = 1; i < parameters.Length; i++)
		{
			float fVal = 0; bool bVal = false;
			if (float.TryParse(parameters[i], out fVal))
			{ speed = fVal; continue; }
			if (bool.TryParse(parameters[i], out bVal))
			{ smooth = bVal; continue; }
		}

		foreach (string s in characters)
		{
			Character c = CharacterManager.instance.GetCharacter(s);
			c.FadeOut(speed, smooth);
		}
	}

	void Command_Enter(string data)
	{
		string[] parameters = data.Split(',');
		string[] characters = parameters[0].Split(';');
		float speed = 3;
		bool smooth = false;
		for (int i = 1; i < parameters.Length; i++)
		{
			float fVal = 0; bool bVal = false;
			if (float.TryParse(parameters[i], out fVal))
			{ speed = fVal; continue; }
			if (bool.TryParse(parameters[i], out bVal))
			{ smooth = bVal; continue; }
		}

		foreach (string s in characters)
		{
			Character c = CharacterManager.instance.GetCharacter(s, true, false);
			c.enabled = true;
			c.FadeIn(speed, smooth);
		}
	}

	void Command_TransLayer(BCFC.LAYER layer, string data)
	{
		string[] parameters = data.Split(',');

		string texName = parameters[0];
		string transTexName = parameters[1];
		Texture2D tex = texName == "null" ? null : Resources.Load("Images/UI/Backdrops/" + texName) as Texture2D;
		Texture2D transTex = Resources.Load("Images/TransitionEffects/" + transTexName) as Texture2D;

		float spd = 2f;
		bool smooth = false;

		for (int i = 2; i < parameters.Length; i++)
		{
			string p = parameters[i];
			float fVal = 0;
			bool bVal = false;
			if (float.TryParse(p, out fVal))
			{ spd = fVal; continue; }
			if (bool.TryParse(p, out bVal))
			{ smooth = bVal; continue; }
		}

		TransitionMaster.TransitionLayer(layer, tex, transTex, spd, smooth);
	}

	void Command_ShowScene(string data)
	{
		string[] parameters = data.Split(',');
		bool show = bool.Parse(parameters[0]);
		string texName = parameters[1];
		Texture2D transTex = Resources.Load("Images/TransitionEffects/" + texName) as Texture2D;
		float spd = 2f;
		bool smooth = false;

		for (int i = 2; i < parameters.Length; i++)
		{
			string p = parameters[i];
			float fVal = 0;
			bool bVal = false;
			if (float.TryParse(p, out fVal))
			{ spd = fVal; continue; }
			if (bool.TryParse(p, out bVal))
			{ smooth = bVal; continue; }
		}

		TransitionMaster.ShowScene(show, spd, smooth, transTex);
	}

	public static int scoreA = 0;
	public static int jijoA = 0;
	public static int kyojoA = 0;
	public static int kojoA = 0;

	public static int scoreB = 0;
	public static int jijoB = 0;
	public static int kyojoB = 0;
	public static int kojoB = 0;

	void Count_Score(string type)
	{
		//string idA = PlayerPrefs.GetString("play_idA");
		//string idB = PlayerPrefs.GetString("play_idB");
		switch (type)
		{
			case "scoreA":
				scoreA = PlayerPrefs.GetInt("ScoreA") + 100;
				PlayerPrefs.SetInt("ScoreA", scoreA);
				Debug.Log("ScoreA = " + scoreA);

				//StartCoroutine(Main.Instance.Web.AddGame(idA, scoreA));
				break;

			case "jijoA":

				jijoA = PlayerPrefs.GetInt("JijoA") + 1;
				PlayerPrefs.SetInt("JijoA", jijoA);
				Debug.Log("JijoA = " + jijoA);

				//StartCoroutine(Main.Instance.Web.AddJijo(idA, jijoA));
				break;

			case "kyojoA":
				kyojoA = PlayerPrefs.GetInt("KyojoA") + 1;
				PlayerPrefs.SetInt("KyojoA", kyojoA);
				Debug.Log("KyojoA = " + kyojoA);;

				//StartCoroutine(Main.Instance.Web.AddKyojo(idA, kyojoA));
				break;

			case "kojoA":
                kojoA = PlayerPrefs.GetInt("KojoA") + 1;
				PlayerPrefs.SetInt("KojoA", kojoA);
				Debug.Log("KojoA = " + kojoA);

				//StartCoroutine(Main.Instance.Web.AddKojo(idA, kojoA));
				break;

			case "scoreB":
                scoreB = PlayerPrefs.GetInt("ScoreB") + 100;
				PlayerPrefs.SetInt("ScoreB", scoreB);
				Debug.Log("ScoreB = " + scoreB);
				Debug.Log("ScoreB after added in SQL: " + ScoresInfo.game_score);

				//StartCoroutine(Main.Instance.Web.AddGame(idB, scoreB));
				break;

			case "jijoB":
                jijoB = PlayerPrefs.GetInt("JijoB") + 1;
				PlayerPrefs.SetInt("JijoB", jijoB);
				Debug.Log("JijoB = " + jijoB);

				//StartCoroutine(Main.Instance.Web.AddJijo(idB, jijoB));
				break;

			case "kyojoB":
                kyojoB = PlayerPrefs.GetInt("KyojoB") + 1;
				PlayerPrefs.SetInt("KyojoB", kyojoB);
				Debug.Log("KyojoB = " + kyojoB);

				//StartCoroutine(Main.Instance.Web.AddKyojo(idB, kyojoB));
				break;

			case "kojoB":
				kojoB = PlayerPrefs.GetInt("KojoB") + 1;
				PlayerPrefs.SetInt("KojoB", kojoB);
				Debug.Log("KojoB = " + kyojoB);

				//StartCoroutine(Main.Instance.Web.AddKojo(idB, kojoB));
				break;
		}
	}

	public void SendID_A(string idA)
	{
		PlayerPrefs.SetString("play_idA", idA);
	}

	public void SendID_B(string idB)
	{
		PlayerPrefs.SetString("play_idB", idB);
	}

    public void SendMultiplayID (string id)
    {
		PlayerPrefs.SetString("multiplayIdB", id);
	}

	void player1Turn()
	{
		if (PhotonNetwork.player.ID != 1)
		{
			nextButton.interactable = false;
			//nextField.interactable = false;
		}
		else
		{
			nextButton.interactable = true;
			//nextField.interactable = true;
		}

        if (UserInfo.language == 1)
        {
			turnStatus.text = "Player 1";
		} else if (UserInfo.language == 2)
        {
			turnStatus.text = "Pemain 1";
		} else if (UserInfo.language == 3)
        {
			turnStatus.text = "Player 1";
		}
            
	}

	void player2Turn()
	{
		if (PhotonNetwork.player.ID == 1)
		{
			nextButton.interactable = false;
			//nextField.interactable = false;
		}
		else
		{
			nextButton.interactable = true;
			//nextField.interactable = true;
		}

		if (UserInfo.language == 1)
		{
			turnStatus.text = "Player 2";
		}
		else if (UserInfo.language == 2)
		{
			turnStatus.text = "Pemain 2";
		}
		else if (UserInfo.language == 3)
		{
			turnStatus.text = "Player 2";
		}
	}

    public void Load_Chat()
	{
		chatNotif.SetActive(false);
		chatCanvas.gameObject.SetActive(true);
		Main.Instance.Chat.Begin();
	}

	public void ActivateChatNotif()
    {
		chatNotif.SetActive(true);
    }

    public void Reimport(int value)
    {
		// bool imported = false;
        if (value == 1)
		{
            StartCoroutine(Main.Instance.Web.GetJijo(ScoresInfo.play_id));
			Debug.Log("Reimport Jijo.");
		} else if (value == 2)
		{
			StartCoroutine(Main.Instance.Web.GetKojo(ScoresInfo.play_id));
			Debug.Log("Reimport Kojo.");
		} else if (value == 3)
        {
			StartCoroutine(Main.Instance.Web.GetKyojo(ScoresInfo.play_id));
			Debug.Log("Reimport Kyojo.");
		} else if (value == 4)
		{
            StartCoroutine(Main.Instance.Web.GetGame(ScoresInfo.play_id));
			Debug.Log("Reimport Score Game.");
		}
	}

    public void BackHomePanel()
    {
		backHomePanel.SetActive(true);
		//Time.timeScale = 0;
		closeButton.onClick.AddListener(CloseBackHomePanel);
		homeButton.onClick.AddListener(BackNovelMenu);
	}

    public void DisconnectedDataNovel()
    {
		disconnectedDataPanel.SetActive(true);
		// Time.timeScale = 0;
		retryButton.onClick.AddListener(ExportNovelData);
		// Command_PlayMusic("null");
		backNovelMenuButton.onClick.AddListener(BackNovelMenu);
	}

    public void CloseBackHomePanel()
    {
		//Time.timeScale = 1;
		backHomePanel.SetActive(false);
	}

	public void ExportNovelData()
	{
		//Time.timeScale = 1;
		disconnectedDataPanel.SetActive(false);
		if (idNetwork == 1)
		{
			StartCoroutine(Main.Instance.Web.AddNovelScores(NovelController.jijoA, NovelController.kyojoA, NovelController.kojoA, NovelController.scoreA, ScoresInfo.play_id));
			/*StartCoroutine(Main.Instance.Web.AddJijo(ScoresInfo.play_id, NovelController.jijoA));
			StartCoroutine(Main.Instance.Web.AddKojo(ScoresInfo.play_id, NovelController.kojoA));
			StartCoroutine(Main.Instance.Web.AddKyojo(ScoresInfo.play_id, NovelController.kyojoA));
			StartCoroutine(Main.Instance.Web.AddGame(ScoresInfo.play_id, NovelController.scoreA));*/
		} else if (idNetwork == 2)
        {
			StartCoroutine(Main.Instance.Web.AddNovelScores(NovelController.jijoB, NovelController.kyojoB, NovelController.kojoB, NovelController.scoreB, ScoresInfo.play_id));
			/*StartCoroutine(Main.Instance.Web.AddJijo(ScoresInfo.play_id, NovelController.jijoB));
			StartCoroutine(Main.Instance.Web.AddKojo(ScoresInfo.play_id, NovelController.kojoB));
			StartCoroutine(Main.Instance.Web.AddKyojo(ScoresInfo.play_id, NovelController.kyojoB));
			StartCoroutine(Main.Instance.Web.AddGame(ScoresInfo.play_id, NovelController.scoreB));*/
		}
	}

	public void OnClickJijo()
    {
		jijoInfo.SetActive(true);
		closeJijo.onClick.AddListener(CloseJijo);
    }

	public void CloseJijo()
	{
		jijoInfo.SetActive(false);
	}

	public void OnClickKyojo()
	{
		kyojoInfo.SetActive(true);
		closeKyojo.onClick.AddListener(CloseKyojo);
	}

	public void CloseKyojo()
	{
		kyojoInfo.SetActive(false);
	}

	public void OnClickKojo()
	{
		kojoInfo.SetActive(true);
		closeKojo.onClick.AddListener(CloseKojo);
	}

	public void CloseKojo()
	{
		kojoInfo.SetActive(false);
	}

	public void CloseWarning()
	{
		warningPanel.SetActive(false);
		jijoEngIcon.SetActive(false);
		kyojoEngIcon.SetActive(false);
		kojoEngIcon.SetActive(false);
		jijoIndIcon.SetActive(false);
		kyojoIndIcon.SetActive(false);
		kojoIndIcon.SetActive(false);
	}

	public void BackHome()
    {
		Debug.Log("Quitting...");
		Command_PlayMusic("null");
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
		/*if (PhotonNetwork.connected)
        {
			PhotonNetwork.Disconnect();
			Debug.Log("Connecting to disconnecting...");
		}*/
		PlayerPrefs.SetInt("SceneNumber", 3);
		int scene = PlayerPrefs.GetInt("SceneNumber");
		Debug.Log("Scene Number in BackHome: " + scene);
	}

	bool backNovelMenu = false;
	public void BackNovelMenu()
	{
		Debug.Log("Quitting...");
		Command_PlayMusic("null");
		UnityEngine.SceneManagement.SceneManager.LoadScene("Novel Menu");
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
			Debug.Log("Connecting to disconnecting...");
		}
		backNovelMenu = true;
		PlayerPrefs.SetInt("SceneNumber", 5);
		int scene = PlayerPrefs.GetInt("SceneNumber");
		Debug.Log("Scene Number in BackNovelMenu: " + scene);
	}
}
