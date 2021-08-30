using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.AI;

// Token: 0x0200001F RID: 31
public class GameControllerScript : MonoBehaviour
{
	// Token: 0x06000080 RID: 128 RVA: 0x0000438C File Offset: 0x0000278C
	public GameControllerScript()
	{
		int[] array = new int[3];
		array[0] = -80;
		array[1] = -40;
		this.itemSelectOffset = array;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00004448 File Offset: 0x00002848
	private void Start()
	{
        this.teleportSelector = UnityEngine.Object.Instantiate<GameObject>(this.baldiScrpt.wanderer.gameObject).GetComponent<AILocationSelectorScript>();
        this.teleportSelector.GetNewTarget();

        this.audioDevice = base.GetComponent<AudioSource>(); //Get the Audio Source
		this.mode = PlayerPrefs.GetString("CurrentMode"); //Get the current mode
		if (this.mode == "Endless") //If it is endless mode
		{
			this.baldiScrpt.endless = true; //Set Baldi use his slightly changed endless anger system
		}
		if (this.mode == "Speedy") //If it is endless mode
		{
			this.JoesWettyMops.SetActive(false);
			this.player.walkSpeed = 50f;
			this.player.runSpeed = 75f;
			this.baldiScrpt.speed = 175f;
			this.baldiScrpt.baldiWait = 0.05f;
		}
		this.schoolMusic.Stop(); //Play the school music
		this.LockMouse(); //Prevent the mouse from moving
		this.UpdateNotebookCount(); //Update the notebook count
		this.itemSelected = 0; //Set selection to item slot 0(the first item slot)
		this.gameOverDelay = 0.5f;
        this.baldihear.SetActive(false);
        this.baldithink.SetActive(false);

	}

	// Token: 0x06000082 RID: 130 RVA: 0x000044BC File Offset: 0x000028BC
	public Material LoreBitVid1;
	public MeshRenderer StaticTV;
	public Animator StaticAnimator;
	private void Update()
	{
        this.MouseTexture();

		if (Input.GetKeyDown(KeyCode.Comma) & Input.GetKeyDown(KeyCode.Backspace))
		{
			StaticTV.material = LoreBitVid1;
			StaticAnimator.enabled = false;
		}

        if (!this.learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!this.gamePaused)
				{
					this.PauseGame(); 
				}
				else
				{
					this.UnpauseGame();
				}
			}
			if (Input.GetKeyDown(KeyCode.Y) & this.gamePaused)
			{
				this.ExitCourseSource.PlayOneShot(ExitCourse);
				SceneManager.LoadScene("MainMenu");
			}
			else if (Input.GetKeyDown(KeyCode.N) & this.gamePaused)
			{
				this.UnpauseGame();
			}
			if (!this.gamePaused & Time.timeScale != 1f)
			{
				Time.timeScale = 1f;
			}
			if (Input.GetMouseButtonDown(1))
			{
				this.UseItem();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				this.DecreaseItemSelection();
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				this.IncreaseItemSelection();
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				this.itemSelected = 0;
				this.UpdateItemSelection();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.itemSelected = 1;
				this.UpdateItemSelection();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.itemSelected = 2;
				this.UpdateItemSelection();
			}
		}
		else if (Time.timeScale != 0f)
		{
			Time.timeScale = 0f;
		}
		if (this.player.stamina < 0f & !this.warning.activeSelf)
		{
			this.warning.SetActive(true); //Set the warning text to be visible
		}
		else if (this.player.stamina > 0f & this.warning.activeSelf)
		{
			this.warning.SetActive(false); //Set the warning text to be invisible
		}
		if (this.player.gameOver)
		{
			Time.timeScale = 0f; //Pause the game
			this.gameOverDelay -= Time.unscaledDeltaTime;
			this.audioDevice.PlayOneShot(this.aud_buzz); //Play the jumpscare sound
			if (this.gameOverDelay <= 0f)
			{
				if (this.mode == "Endless") //If it is in endless
				{
					if (this.notebooks > PlayerPrefs.GetInt("HighBooks")) //If the player achieved a new score
					{
						PlayerPrefs.SetInt("HighBooks", this.notebooks); //Update the high score
						PlayerPrefs.SetInt("HighTime", Mathf.FloorToInt(this.time)); //(Unused) Update the time
						this.highScoreText.SetActive(true); // "WOW KAZOW! THATS A NEW HIGH SCORE!"
					}
					else if (this.notebooks == PlayerPrefs.GetInt("HighBooks") & Mathf.FloorToInt(this.time) > PlayerPrefs.GetInt("HighTime")) //(Unused) If the player has a brand new record for time
					{
						PlayerPrefs.SetInt("HighTime", Mathf.FloorToInt(this.time)); //Update the high time
						this.highScoreText.SetActive(true); // "WOW KAZOW! THATS A NEW HIGH SCORE!"
                    }
					PlayerPrefs.SetInt("CurrentBooks", this.notebooks); //Update the high score
                    PlayerPrefs.SetInt("CurrentTime", Mathf.FloorToInt(this.time)); //(Unused) Update the time
                }
				Time.timeScale = 1f; // Unpause the game
				SceneManager.LoadScene("GameOver"); // Go to the game over screen
			}
		}
		if (this.finaleMode && !this.audioDevice.isPlaying && this.exitsReached == 3) //Play the weird sound after getting some exits
        {
			this.audioDevice.clip = this.aud_MachineLoop;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		this.time += Time.deltaTime;
	}
	public AudioClip ExitCourse;
	public AudioSource ExitCourseSource;
	// Token: 0x06000083 RID: 131 RVA: 0x00004828 File Offset: 0x00002C28
	private void UpdateNotebookCount()
	{
		if (this.mode == "Story")
		{
			this.notebookCount.text = this.notebooks.ToString() + "/7 Notebooks";
		}
		else if (this.mode == "Free-Run")
		{
			this.notebookCount.text = this.notebooks.ToString() + "/7 Notebooks";
		}
		else if (this.mode == "Speedy")
		{
			this.notebookCount.text = this.notebooks.ToString() + "/7 Notebooks";
		}
		else
		{
			this.notebookCount.text = this.notebooks.ToString() + " Notebooks";
		}
		if (this.notebooks == 7 & this.mode == "Story")
		{
			this.ActivateFinaleMode();
		}
		if (this.notebooks == 7 & this.mode == "Speedy")
		{
			this.ActivateFinaleMode();
		}
		if (this.notebooks == 7 & this.mode == "Free-Run")
		{
			this.ActivateFinaleMode();
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000048C0 File Offset: 0x00002CC0
	public void CollectNotebook()
	{
		this.notebooks++;
		this.UpdateNotebookCount();
		this.time = 0f;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000048E1 File Offset: 0x00002CE1
	public void LockMouse()
	{
		if (!this.learningActive)
		{
			this.cursorController.LockCursor(); //Prevent the cursor from moving
			this.mouseLocked = true;
			this.reticle.SetActive(true);
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000490C File Offset: 0x00002D0C
	public void UnlockMouse()
	{
		this.cursorController.UnlockCursor(); //Allow the cursor to move
		this.mouseLocked = false;
		this.reticle.SetActive(false);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000492C File Offset: 0x00002D2C
	public void PauseGame()
	{
		Time.timeScale = 0f;
		this.gamePaused = true;
		this.pauseText.SetActive(true);
		this.baldiNod.SetActive(true);
		this.baldiShake.SetActive(true);
	}

	public void PauseGameLight()
	{
		Time.timeScale = 0f;
		this.gamePaused = true;
	}
	public void UnpauseGameLight()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
	}
	// Token: 0x06000088 RID: 136 RVA: 0x00004963 File Offset: 0x00002D63
	public void UnpauseGame()
	{
		Time.timeScale = 1f;
		this.gamePaused = false;
		this.pauseText.SetActive(false);
		this.baldiNod.SetActive(false);
		this.baldiShake.SetActive(false);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000499C File Offset: 0x00002D9C
	public void ActivateSpoopMode()
	{
		this.spoopMode = true; //Tells the game its time for spooky
		this.entrance_0.Lower(); //Lowers all the exits
		this.entrance_1.Lower();
		this.entrance_2.Lower();
		this.entrance_3.Lower();
		this.baldiTutor.SetActive(false); //Turns off Baldi(The one that you see at the start of the game)
		this.TheTest.SetActive(true);
		if (this.mode == "Free-Run")
		{
			this.MrSpike.SetActive(true);
			// does nothing lol
		}
		if (this.mode == "Story")
		{
			this.baldi.SetActive(true); //Turns on Baldi
			this.MrSpike.SetActive(true);
		}
		if (this.mode == "Speedy")
		{
			this.baldi.SetActive(true); //Turns on Baldi
		}
		if (this.mode == "Endless")
		{

			this.MrSpike.SetActive(true);
			this.baldi.SetActive(true); //Turns on Baldi
		}

		this.AnApple.SetActive(false); //Turns off Baldi Apple
        this.ABanana.SetActive(false); //Turns off Baldi Banana
        this.principal.SetActive(true); //Turns on Principal
        this.crafters.SetActive(true); //Turns on Crafters
        this.playtime.SetActive(true); //Turns on Playtime
        this.gottaSweep.SetActive(true); //Turns on Gotta Sweep
        this.zerothprize.SetActive(true); //Turns on 0th Prize
        this.mrcuts.SetActive(true); //Turns on Mr. Cuts
        this.bully.SetActive(true); //Turns on Bully
        this.firstPrize.SetActive(true); //Turns on First-Prize
        this.audioDevice.PlayOneShot(this.aud_Hang); //Plays the hang sound
		this.learnMusic.Stop(); //Stop all the music
		this.schoolMusic.Stop();
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004A63 File Offset: 0x00002E63
	public void ActivateFinaleMode()
	{
		this.finaleMode = true;
		this.entrance_0.Raise(); //Raise all the enterances(make them appear)
		this.entrance_1.Raise();
		this.entrance_2.Raise();
		this.entrance_3.Raise();
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004A98 File Offset: 0x00002E98
	public void GetAngry(float value) //Make Baldi get angry
	{
		if (!this.spoopMode)
		{
			this.ActivateSpoopMode();
		}
		this.baldiScrpt.GetAngry(value);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00004AB7 File Offset: 0x00002EB7
	public void ActivateLearningGame()
	{
		this.learningActive = true; 
		this.UnlockMouse(); //Unlock the mouse
		this.tutorBaldi.Stop(); //Make tutor Baldi stop talking
		if (!this.spoopMode) //If the player hasn't gotten a question wrong
		{
			this.schoolMusic.Stop(); //Start playing the learn music
			this.learnMusic.Play();
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004AF4 File Offset: 0x00002EF4
	public void DeactivateLearningGame(GameObject subject)
	{
		this.learningActive = false; 
		UnityEngine.Object.Destroy(subject);
		this.LockMouse(); //Prevent the mouse from moving
		if (this.player.stamina < 100f) //Reset Stamina
		{
			this.player.stamina = 100f;
		}
		if (!this.spoopMode) //If it isn't spoop mode, play the school music
		{
			this.schoolMusic.Play();
			this.learnMusic.Stop();
		}
		if (this.notebooks == 1 & !this.spoopMode) // If this is the players first notebook and they didn't get any questions wrong, reward them with a quarter
		{
			this.quarter.SetActive(true);
			this.tutorBaldi.PlayOneShot(this.aud_Prize);
		}
		else if (this.notebooks == 7 & this.mode == "Story") // Plays the all 7 notebook sound
		{
			this.audioDevice.PlayOneShot(this.aud_AllNotebooks, 0.8f);
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00004BCC File Offset: 0x00002FCC
	public void IncreaseItemSelection()
	{
		this.itemSelected++;
		if (this.itemSelected > 2)
		{
			this.itemSelected = 0;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); //Moves the item selector background(the red rectangle)
		this.UpdateItemName();
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00004C30 File Offset: 0x00003030
	public void DecreaseItemSelection()
	{
		this.itemSelected--;
		if (this.itemSelected < 0)
		{
			this.itemSelected = 2;
		}
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); //Moves the item selector background(the red rectangle)
        this.UpdateItemName();
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00004C91 File Offset: 0x00003091
	public void UpdateItemSelection()
	{
		this.itemSelect.anchoredPosition = new Vector3((float)this.itemSelectOffset[this.itemSelected], 0f, 0f); //Moves the item selector background(the red rectangle)
        this.UpdateItemName();
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00004CC8 File Offset: 0x000030C8
	public void CollectItem(int item_ID)
	{
		if (this.item[0] == 0)
		{
			this.item[0] = item_ID; //Set the item slot to the Item_ID provided
			this.itemSlot[0].texture = this.itemTextures[item_ID]; //Set the item slot's texture to a texture in a list of textures based on the Item_ID
		}
		else if (this.item[1] == 0)
		{
			this.item[1] = item_ID; //Set the item slot to the Item_ID provided
            this.itemSlot[1].texture = this.itemTextures[item_ID]; //Set the item slot's texture to a texture in a list of textures based on the Item_ID
        }
		else if (this.item[2] == 0)
		{
			this.item[2] = item_ID; //Set the item slot to the Item_ID provided
            this.itemSlot[2].texture = this.itemTextures[item_ID]; //Set the item slot's texture to a texture in a list of textures based on the Item_ID
        }
		else //This one overwrites the currently selected slot when your inventory is full
		{
			this.item[this.itemSelected] = item_ID;
			this.itemSlot[this.itemSelected].texture = this.itemTextures[item_ID];
		}
		this.UpdateItemName();
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00004D94 File Offset: 0x00003194
	public void UseItem()
	{
		if (this.item[this.itemSelected] != 0) //If the item slot isn't empty
		{
			if (this.item[this.itemSelected] == 1)  //Zesty Bar Code
			{
				this.player.stamina = this.player.maxStamina * 2f;
				this.ResetItem(); //Remove the item
			}
			else if (this.item[this.itemSelected] == 2) //Yellow Door Lock Code
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "SwingingDoor" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f))
				{
					raycastHit.collider.gameObject.GetComponent<SwingingDoorScript>().LockDoor(15f); //Lock the door for 15 seconds
					this.ResetItem(); //Remove the item
				}
			}
			else if (this.item[this.itemSelected] == 3) //Principal's Keys Code
			{
				Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit2;
				if (Physics.Raycast(ray2, out raycastHit2) && (raycastHit2.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit2.transform.position) <= 10f))
				{
					raycastHit2.collider.gameObject.GetComponent<DoorScript>().UnlockDoor(); //Unlock the door
					raycastHit2.collider.gameObject.GetComponent<DoorScript>().OpenDoor(); //Open the door
					this.ResetItem(); //Remove the item
				}
			}
			else if (this.item[this.itemSelected] == 4) //Bsoda Code
			{
				UnityEngine.Object.Instantiate<GameObject>(this.bsodaSpray, this.playerTransform.position, this.cameraTransform.rotation); //Clone the BSODA Spray object
				this.ResetItem(); //Remove the item
				this.player.ResetGuilt("drink", 1f); // Makes the player guilty for drinking
				this.audioDevice.PlayOneShot(this.aud_Soda); // Play the spray sound
			}
			else if (this.item[this.itemSelected] == 5) //Quarter Code
			{
				Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit3;
				if (Physics.Raycast(ray3, out raycastHit3))
				{
					if (raycastHit3.collider.name == "BSODAMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						this.ResetItem(); //Remove the item 
						this.CollectItem(4); //Give BSODA
					}
					else if (raycastHit3.collider.name == "ZestyMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						this.ResetItem(); //Remove the item
						this.CollectItem(1); //Give Zesty Bar
					}
					else if (raycastHit3.collider.name == "AppleMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						this.ResetItem(); //Remove the item
						this.CollectItem(11); //Give Apple
					}
					else if (raycastHit3.collider.name == "PayPhone" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
						raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play(); //Tell the phone to start making the noise
						this.ResetItem(); //Remove the item
					}
				}
			}
			else if (this.item[this.itemSelected] == 6) // Baldi Anti-hearing Code
			{
				Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit4;
				if (Physics.Raycast(ray4, out raycastHit4) && (raycastHit4.collider.name == "TapePlayer" & Vector3.Distance(this.playerTransform.position, raycastHit4.transform.position) <= 10f))
				{
					raycastHit4.collider.gameObject.GetComponent<TapePlayerScript>().Play(); //Tell the tape player to start making the noise
					this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 7) // Alarm Clock Code
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.alarmClock, this.playerTransform.position, this.cameraTransform.rotation); //Create a clone of the Alarm Clock
				gameObject.GetComponent<AlarmClockScript>().baldi = this.baldiScrpt; //Set the Alarm Clock's Baldi to the BaldiScript
				this.ResetItem(); //Remove the item
			}
			else if (this.item[this.itemSelected] == 8) // WD No Squee Code
			{
				Ray ray5 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit5;
				if (Physics.Raycast(ray5, out raycastHit5) && (raycastHit5.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit5.transform.position) <= 10f))
				{
					raycastHit5.collider.gameObject.GetComponent<DoorScript>().SilenceDoor(); // Silences the door
					this.ResetItem(); //Remove the item
					this.audioDevice.PlayOneShot(this.aud_Spray); //Plays the spray sound
				}
			}
			else if (this.item[this.itemSelected] == 9) // Safety Scissors Code
			{
				Ray ray6 = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit6;
				if (this.player.jumpRope)
				{
					this.player.DeactivateJumpRope();
					this.playtimeScript.Disappoint();
					this.player.ResetGuilt("bullying", 1f);
					this.ResetItem();
				}
				else if (Physics.Raycast(ray6, out raycastHit6) && raycastHit6.collider.name == "1st Prize")
				{
					this.firstPrizeScript.GoCrazy();
					this.player.ResetGuilt("sabotage", 1f);
					this.ResetItem();
				}
			}
			else if (this.item[this.itemSelected] == 10) // Stab Pencil Code
			{
				RaycastHit raycastHit3;
				if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f)), out raycastHit3))
				{
					if (raycastHit3.collider.tag == "NPC" || raycastHit3.collider.name == "Its a Bully")
					{
						Transform npc = raycastHit3.transform;
						//Set position of the NPC to the position of the teleporter
						npc.position = new Vector3(this.teleportSelector.transform.position.x, npc.position.y, this.teleportSelector.transform.position.z);
						if (npc.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>())
						{
							//Characters with a NavMeshAgent need to be warped, else they break or just stay where they were before.
							npc.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(new Vector3(this.teleportSelector.transform.position.x, npc.position.y, this.teleportSelector.transform.position.z));
						}
						//Set new teleport position.
						this.audioDevice.PlayOneShot(this.aud_pencilstab);
						this.teleportSelector.GetNewTarget();
						this.ResetItem();
						return;
					}
				}
			}
			else if (this.item[this.itemSelected] == 11) // Baldi Apple Code
			{
				StartCoroutine(AppleDistractBaldi());
				this.ResetItem();
				return;
			}
			else if (this.item[this.itemSelected] == 12) // Baldi Banana Code
			{
				StartCoroutine(BananaDistractBaldi());
				this.ResetItem();
				return;
			}
			else if (this.item[this.itemSelected] == 13) // Slingshot Stunshot Code
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f)), out raycastHit) && raycastHit.collider.tag == "NPC" && raycastHit.collider.name != "Baldi" && Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) <= 10f)
				{
					StartCoroutine(Stunlock(raycastHit.transform.gameObject));
					this.ResetItem();
					//The guilt type (for example "drink") sets which audio clip gets played when the principal catches you. "stunning" does not exist by default, just set this to whatever you want him to say
					this.player.ResetGuilt("stunning", 1f);
					return;
				}
			}
			else if (this.item[this.itemSelected] == 14) // Broken Ruler Code (Similar to pencil code)
			{
				RaycastHit raycastHit3;
				if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f)), out raycastHit3))
				{
					if (raycastHit3.collider.tag == "NPC" || raycastHit3.collider.name == "Its a Bully")
					{
						Transform npc = raycastHit3.transform;
						//Set position of the NPC to the position of the teleporter
						npc.position = new Vector3(this.teleportSelector.transform.position.x, npc.position.y, this.teleportSelector.transform.position.z);
						if (npc.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>())
						{
							//Characters with a NavMeshAgent need to be warped, else they break or just stay where they were before.
							npc.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(new Vector3(this.teleportSelector.transform.position.x, npc.position.y, this.teleportSelector.transform.position.z));
						}
						//Set new teleport position.
						this.audioDevice.PlayOneShot(this.aud_rulerbreak);
						this.teleportSelector.GetNewTarget();
						this.ResetItem();
						return;
					}
				}
			}
			else if (this.item[this.itemSelected] == 15) // Joe's Wetty Mop Code
			{
				StartCoroutine(JoeWetMop()); //Starts Joe's Wetty Mop
				this.ResetItem(); //Removes The Item From The Player's Inventory
				return;
			}
			else if (this.item[this.itemSelected] == 16)
			{
				principal.GetComponent<NavMeshAgent>().SetDestination(this.player.position);
				this.WhistleSourcePlayer.PlayOneShot(PriWhistleSound);
				this.PriQueue.QueueAudio(this.Pri_Coming);
				this.ResetItem(); //Removes The Item From The Player's Inventory
				return;
			}
			else if (this.item[this.itemSelected] == 17)
			{
				baldi.GetComponent<NavMeshAgent>().SetDestination(this.player.position);
				this.baldihereinatorsound.PlayOneShot(aud_Teleport);
				this.ResetItem(); //Removes The Item From The Player's Inventory
				return;
			}
			else if (this.item[this.itemSelected] == 18)
			{
				StartCoroutine(Teleporter());
				this.ResetItem(); //Removes The Item From The Player's Inventory
				return;
			}
			else if (this.item[this.itemSelected] == 19)
			{
				StartCoroutine(FogItem());
				this.ResetItem(); //Removes The Item From The Player's Inventory
				return;
			}
			else if (this.item[this.itemSelected] == 20)
			{
				StartCoroutine(FacTag());
				this.ResetItem(); //Removes The Item From The Player's Inventory
				return;
			}
			else if (this.item[this.itemSelected] == 21)
			{
				StartCoroutine(SockMask());
				this.ResetItem(); //Removes The Item From The Player's Inventory
				return;
			}
		}
	}
	public AudioQueueScript PriQueue;
	public AudioClip Pri_Coming;
	public AudioSource WhistleSourcePlayer;
    // Token: Distract Baldi Code for An Apple For Baldi
    IEnumerator AppleDistractBaldi()
    {
        float timer = 15f;
        UnityEngine.AI.NavMeshAgent navMeshAgent = baldi.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.bal_collider.SetActive(false);
        this.AnApple.SetActive(true);
        this.BaldiSprite.SetActive(false);
        //Do Sprite Swap Here
        this.audioDevice.PlayOneShot(this.baldi_apple);
        while (timer > 0)
        {
            navMeshAgent.velocity = Vector3.zero;
            timer -= Time.deltaTime;
            baldiScrpt.timeToMove = timer + 1f;
            yield return new WaitForEndOfFrame();
        }
        this.bal_collider.SetActive(true);
        this.BaldiSprite.SetActive(true);
        this.AnApple.SetActive(false);
    }
	public GameObject EventStuff;
	public TextMeshProUGUI EventText;
    IEnumerator JoeWetMop()
    {
        float timer = 30f; //Make the event last for 30 seconds

        this.waterflood.SetActive(true); //Activates the water gameobject
        this.audioDevice.PlayOneShot(this.aud_flood); //Plays the water sound
        this.player.walkSpeed = 2.5f; //Make the player's walk speed slower (decreased by 4 times)
        this.player.runSpeed = 4f; //Make the player's run speed slower (decreased by 4 times)
        this.baldiScrpt.speed = 18.75f; //Make baldi's speed slower (decreased by 4 times)
		EventText.text = "Uh oh! Looks like the leak is leaking again! There's no whirlpools this time, but you and baldi are still slow!";
		EventStuff.SetActive(true);
        while (timer > 0) //When the event ends
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        this.waterflood.SetActive(false); //Deactivates the water gameobject
        this.player.walkSpeed = 10f; //Increase player's walk speed to original amount
        this.player.runSpeed = 16f; //Increase player's run speed to original amount
        this.baldiScrpt.speed = 75f; //Increase baldi's speed to original amount
		EventStuff.SetActive(false);
	}
	IEnumerator FacTag()
	{
		float timer = 15f; //Make the event last for 30 seconds

		HudFacultyNametag.SetActive(true);
		principal.SetActive(false);

		while (timer > 0) //When the event ends
		{
			timer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		HudFacultyNametag.SetActive(false);
		principal.SetActive(true);
	}
	public Color White;
	IEnumerator FogItem()
	{
		float timer = 30f; //Make the event last for 30 seconds

		RenderSettings.fogColor = White;
		RenderSettings.fog = true;
		TheTest.SetActive(false);
		EventText.text = "Looks like a broken fog machine got activated! Ack, ack, ack!";
		EventStuff.SetActive(true);
		while (timer > 0) //When the event ends
		{
			timer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		EventStuff.SetActive(false);
		TheTest.SetActive(true);
		RenderSettings.fog = false;
	}
	public GameObject SockMaskHud;
	IEnumerator SockMask()
	{
		float timer = 30f; //Make the event last for 30 seconds

		this.crafters.SetActive(false);
		SockMaskHud.SetActive(true);

		while (timer > 0) //When the event ends
		{
			timer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		this.crafters.SetActive(true);
		SockMaskHud.SetActive(false);
	}

	IEnumerator BananaDistractBaldi()
    {
        float timer = 5f;
        UnityEngine.AI.NavMeshAgent navMeshAgent = baldi.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.bal_collider.SetActive(false);
        this.ABanana.SetActive(true);
        this.BaldiSprite.SetActive(false);
        //Do Sprite Swap Here
        this.audioDevice.PlayOneShot(this.baldi_bananaeat);
        while (timer > 0)
        {
            navMeshAgent.velocity = Vector3.zero;
            timer -= Time.deltaTime;
            baldiScrpt.timeToMove = timer + 1f;
            yield return new WaitForEndOfFrame();
        }
        this.bal_collider.SetActive(true);
        this.BaldiSprite.SetActive(true);
        this.ABanana.SetActive(false);
    }

    IEnumerator Stunlock(GameObject character)
    {
        float timer = 10f;
        UnityEngine.AI.NavMeshAgent navMeshAgent = character.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.audioDevice.PlayOneShot(this.slingshothit);
        while (timer > 0)
        {
            navMeshAgent.velocity = Vector3.zero;
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

	private IEnumerator Teleporter()
	{
		this.playerCollider.enabled = false;
		int teleports = UnityEngine.Random.Range(7, 14);
		int teleportCount = 0;
		float baseTime = 0.3f;
		float currentTime = baseTime;
		float increaseFactor = 1.1f;
		while (teleportCount < teleports)
		{
			currentTime -= Time.deltaTime;
			if (currentTime < 0f)
			{
				this.Teleport();
				teleportCount++;
				baseTime *= increaseFactor;
				currentTime = baseTime;
			}
			yield return null;
		}
		this.playerCollider.enabled = true;
		yield break;
	}
	public GameObject MrSpike;
	public void Teleport()
	{
		this.AILocationSelector.GetNewTarget();
		this.player.transform.position = this.AILocationSelector.transform.position + Vector3.up * this.player.height;
		this.audioDevice.PlayOneShot(this.aud_Teleport);
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000052F4 File Offset: 0x000036F4
	public void ResetItem()
	{
		this.item[this.itemSelected] = 0; //Resets the current item slot
		this.itemSlot[this.itemSelected].texture = this.itemTextures[0]; //Resets the current item slot texture
		this.UpdateItemName();
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00005324 File Offset: 0x00003724
	public void LoseItem(int id)
	{
		this.item[id] = 0; //Resets the item slot
        this.itemSlot[id].texture = this.itemTextures[0]; //Resets the item slot texture
        this.UpdateItemName();
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000534A File Offset: 0x0000374A
	public void UpdateItemName()
	{
		this.itemText.text = this.itemNames[this.item[this.itemSelected]];
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000536C File Offset: 0x0000376C
	public void ExitReached()
	{
		this.exitsReached++;
		if (this.exitsReached == 1)
		{
			RenderSettings.ambientLight = Color.red; //Make everything red and start player the weird sound
			RenderSettings.fog = true;
			this.audioDevice.PlayOneShot(this.aud_Switch, 0.8f);
			this.audioDevice.clip = this.aud_MachineQuiet;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 2) //Play a sound
		{
			this.audioDevice.volume = 0.8f;
			this.audioDevice.clip = this.aud_MachineStart;
			this.audioDevice.loop = true;
			this.audioDevice.Play();
		}
		if (this.exitsReached == 3) //Play a even louder sound
		{
			this.audioDevice.clip = this.aud_MachineRev;
			this.audioDevice.loop = false;
			this.audioDevice.Play();
		}
	}
	// Token: 0x06000097 RID: 151 RVA: 0x00005459 File Offset: 0x00003859
	public void DespawnCrafters()
	{
		this.crafters.SetActive(false); //Make Arts And Crafters Inactive
	}

    private void MouseTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Door" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 15f & !this.gamePaused))

        {

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }



            {

				this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            this.reticle.SetActive(true);

            this.reticle2.SetActive(false);

            return;

        }
        if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "FacultyDoor" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 15f & !this.gamePaused))

        {

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

			this.reticle.SetActive(true);

            this.reticle2.SetActive(false);

            return;

        }

		if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "MathMachine" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 15f & !this.gamePaused))

		{

			{

				this.reticle.SetActive(false);

				this.reticle2.SetActive(true);

				return;

			}

			this.reticle.SetActive(true);

			this.reticle2.SetActive(false);

			return;

		}

		if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "NumberBallon" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 15f & !this.gamePaused))

		{

			{

				this.reticle.SetActive(false);

				this.reticle2.SetActive(true);

				return;

			}

			this.reticle.SetActive(true);

			this.reticle2.SetActive(false);

			return;

		}

		else

        {

            if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Notebook" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 10f & !this.gamePaused))

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            if (Physics.Raycast(ray, out raycastHit) && (raycastHit.collider.tag == "Item" & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 10f & !this.gamePaused))

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            if (raycastHit.collider.tag == "SwingingDoor" & this.item[this.itemSelected] == 2 & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 10f & !this.gamePaused)

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            if (!Physics.Raycast(ray, out raycastHit) || !(raycastHit.collider.tag == "Untagged"))

            {

                this.reticle.SetActive(true);

                this.reticle2.SetActive(false);

                return;

            }

            if (raycastHit.collider.name == "BSODAMachine" & this.item[this.itemSelected] == 5 & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 10f & !this.gamePaused)

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            if (raycastHit.collider.name == "ZestyMachine" & this.item[this.itemSelected] == 5 & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 10f & !this.gamePaused)

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            if (raycastHit.collider.name == "PayPhone" & this.item[this.itemSelected] == 5 & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 10f & !this.gamePaused)

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            if (raycastHit.collider.name == "TapePlayer" & this.item[this.itemSelected] == 6 & Vector3.Distance(this.playerTransform.position, raycastHit.transform.position) < 10f & !this.gamePaused)

            {

                this.reticle.SetActive(false);

                this.reticle2.SetActive(true);

                return;

            }

            this.reticle.SetActive(true);

            this.reticle2.SetActive(false);

            return;

        }
    }

	// Token: 0x060000B4 RID: 180 RVA: 0x00006694 File Offset: 0x00004A94
	public AudioClip PriWhistleSound;
    public void GetNewTarget()
    {
        this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 28f)); //Get a random number between 0 and 28
        base.transform.position = this.newLocation[this.id].position; //Set it's location to a position in a list of positions using the ID variable that just got set.
        this.ambience.PlayAudio(); //Play an ambience audio
    }

    // Token: 0x060000B5 RID: 181 RVA: 0x000066E4 File Offset: 0x00004AE4
    public void GetNewTargetHallway()
    {
        this.id = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 15f)); //Get a random number between 0 and 15
        base.transform.position = this.newLocation[this.id].position; //Set it's location to a position in a list of positions using the ID variable that just got set.
        this.ambience.PlayAudio(); //Play an ambience audio
    }

    private AILocationSelectorScript teleportSelector;

    // Token: 0x0400012B RID: 299
    public Transform[] newLocation = new Transform[29];

    // Token: 0x0400012D RID: 301
    private int id;

    // Token: 0x0400012C RID: 300
    public AmbienceScript ambience;

    public GameObject BaldiSprite;

    public GameObject AnApple;

    public GameObject ABanana;

    public GameObject bal_collider;

    public GameObject waterflood; //the water gameobject

    public AudioClip aud_flood; 

    // Token: 0x04000156 RID: 342
    public SpriteRenderer spriteImage;

    public Sprite baldiapple;

    // Token: 0x040000AB RID: 171
    public CursorControllerScript cursorController;

	// Token: 0x040000AC RID: 172
	public PlayerScript player;

	public GameObject JoesWettyMops;

	// Token: 0x040000AD RID: 173
	public Transform playerTransform;

	// Token: 0x040000AE RID: 174
	public Transform cameraTransform;

	// Token: 0x040000AF RID: 175
	public EntranceScript entrance_0;

	// Token: 0x040000B0 RID: 176
	public EntranceScript entrance_1;

	// Token: 0x040000B1 RID: 177
	public EntranceScript entrance_2;

	// Token: 0x040000B2 RID: 178
	public EntranceScript entrance_3;

	// Token: 0x040000B3 RID: 179
	public GameObject baldiTutor;

	// Token: 0x040000B4 RID: 180
	public GameObject baldi;

	// Token: 0x040000B5 RID: 181
	public BaldiScript baldiScrpt;

	// Token: 0x040000B6 RID: 182
	public AudioClip aud_Prize;

	// Token: 0x040000B7 RID: 183
	public AudioClip aud_AllNotebooks;

	// Token: 0x040000B8 RID: 184
	public GameObject principal;

	// Token: 0x040000B9 RID: 185
	public GameObject crafters;

	// Token: 0x040000BA RID: 186
	public GameObject playtime;

    public GameObject mrcuts;

	// Token: 0x040000BB RID: 187
	public PlaytimeScript playtimeScript;

	// Token: 0x040000BC RID: 188
	public GameObject gottaSweep;

    public GameObject zerothprize;

	// Token: 0x040000BD RID: 189
	public GameObject bully;

	// Token: 0x040000BE RID: 190
	public GameObject firstPrize;

	// Token: 0x040000BF RID: 191
	public FirstPrizeScript firstPrizeScript;

	// Token: 0x040000C0 RID: 192
	public GameObject quarter;

	// Token: 0x040000C1 RID: 193
	public AudioSource tutorBaldi;

	// Token: 0x040000C2 RID: 194
	public string mode;

	// Token: 0x040000C3 RID: 195
	public int notebooks;

	// Token: 0x040000C4 RID: 196
	public GameObject[] notebookPickups;

	// Token: 0x040000C5 RID: 197
	public int failedNotebooks;

	// Token: 0x040000C6 RID: 198
	public float time;

	// Token: 0x040000C7 RID: 199
	public bool spoopMode;

	// Token: 0x040000C8 RID: 200
	public bool finaleMode;

	// Token: 0x040000C9 RID: 201
	public bool debugMode;

	// Token: 0x040000CA RID: 202
	public bool mouseLocked;

	// Token: 0x040000CB RID: 203
	public int exitsReached;

	// Token: 0x040000CC RID: 204
	public int itemSelected;

	// Token: 0x040000CD RID: 205
	public int[] item = new int[3];

	// Token: 0x040000CE RID: 206
	public RawImage[] itemSlot = new RawImage[3];


	// Token: 0x040000CF RID: 207
	private string[] itemNames = new string[]
	{
		"NOTHING",
		"ENERGY FLAVORED ZESTY BAR",
		"SWINGING DOOR LOCK",
		"PRINCIPAL'S KEYS",
		"BSODA",
		"QUARTER",
		"BALDI'S LEAST FAVORITE TAPE",
		"ALARM CLOCK",
		"WD-NOSQUEE (DOOR TYPE)",
		"SUPER SAFE SCISSORS",
        "PENCIL O' STABBIN",
        "AN APPLE FOR BALDI",
        "BANANA.PNG",
        "SLINGY SLINGSHOT",
        "SNAPPED RULER",
        "JOE'S WETTY MOP",
		"PRINCIPAL WHISTLE",
		"BRING BALDI HERE - INATOR",
		"DANGEROUS TELEPORTER",
		"BROKEN FOG MACHINE",
		"FACULTY NAMETAG",
		"SOCK MASK"
	};

	// Token: 0x040000D0 RID: 208
	public TextMeshProUGUI itemText;

	// Token: 0x040000D1 RID: 209
	public UnityEngine.Object[] items = new UnityEngine.Object[10];

	// Token: 0x040000D2 RID: 210
	public Texture[] itemTextures = new Texture[10];

	// Token: 0x040000D3 RID: 211
	public GameObject bsodaSpray;

	// Token: 0x040000D4 RID: 212
	public GameObject alarmClock;

	// Token: 0x040000D5 RID: 213
	public TextMeshProUGUI notebookCount;

	// Token: 0x040000D6 RID: 214
	public GameObject pauseText;

	// Token: 0x040000D7 RID: 215
	public GameObject highScoreText;

	// Token: 0x040000D8 RID: 216
	public GameObject baldiNod;

	// Token: 0x040000D9 RID: 217
	public GameObject baldiShake;

	// Token: 0x040000DA RID: 218
	public GameObject warning;

	// Token: 0x040000DB RID: 219
	public GameObject reticle;

    public GameObject baldihear;

    public GameObject baldithink;

	public GameObject TheTest;

	// Token: 0x040000DC RID: 220
	public RectTransform itemSelect;

	// Token: 0x040000DD RID: 221
	public int[] itemSelectOffset;

	// Token: 0x040000DE RID: 222
	public bool gamePaused;

	// Token: 0x040000DF RID: 223
	public bool learningActive;

	// Token: 0x040000E0 RID: 224
	private float gameOverDelay;

	// Token: 0x040000E1 RID: 225
	private AudioSource audioDevice;

	// Token: 0x040000E2 RID: 226
	public AudioClip aud_Soda;

	// Token: 0x040000E3 RID: 227
	public AudioClip aud_Spray;

	// Token: 0x040000E4 RID: 228
	public AudioClip aud_buzz;

	// Token: 0x040000E5 RID: 229
	public AudioClip aud_Hang;

	// Token: 0x040000E6 RID: 230
	public AudioClip aud_MachineQuiet;

	public AudioSource baldihereinatorsound;

	// Token: 0x040000E7 RID: 231
	public AudioClip aud_MachineStart;

	// Token: 0x040000E8 RID: 232
	public AudioClip aud_MachineRev;

	// Token: 0x040000E9 RID: 233
	public AudioClip aud_MachineLoop;

	// Token: 0x040000EA RID: 234
	public AudioClip aud_Switch;

    // Token: 0x040000EA RID: 234
    public AudioClip aud_pencilstab;

    // Token: 0x040000EC RID: 236
    public AudioClip baldi_apple;

    // Token: 0x040000EC RID: 236
    public AudioClip baldi_bananaeat;

    // Token: 0x040000EC RID: 236
    public AudioClip slingshothit;

    public AudioClip aud_rulerbreak;

    // Token: 0x040000EB RID: 235
    public AudioSource schoolMusic;

	// Token: 0x040000EC RID: 236
	public AudioSource learnMusic;

    public GameObject reticle2;

	public AILocationSelectorScript AILocationSelector;

	public AudioClip aud_Teleport;

	public Collider playerCollider;

	public GameObject HudFacultyNametag;
}
