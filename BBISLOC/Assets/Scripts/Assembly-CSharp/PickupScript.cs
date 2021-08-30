using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class PickupScript : MonoBehaviour
{
	// Token: 0x060000DE RID: 222 RVA: 0x0000771F File Offset: 0x00005B1F
	private void Start()
	{
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00007724 File Offset: 0x00005B24
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				if (raycastHit.transform.name == "Pickup_EnergyFlavoredZestyBar" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(1);
				}
				else if (raycastHit.transform.name == "Pickup_YellowDoorLock" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(2);
				}
				else if (raycastHit.transform.name == "Pickup_Key" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(3);
				}
				else if (raycastHit.transform.name == "Pickup_BSODA" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(4);
				}
				else if (raycastHit.transform.name == "Pickup_Quarter" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(5);
				}
				else if (raycastHit.transform.name == "Pickup_Tape" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(6);
				}
				else if (raycastHit.transform.name == "Pickup_AlarmClock" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(7);
				}
				else if (raycastHit.transform.name == "Pickup_WD-3D" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(8);
				}
				else if (raycastHit.transform.name == "Pickup_SafetyScissors" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(9);
				}
                else if (raycastHit.transform.name == "Pickup_StabPencil" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    this.gc.CollectItem(10);
                }
                else if (raycastHit.transform.name == "Pickup_BaldiApple" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    this.gc.CollectItem(11);
                }
                else if (raycastHit.transform.name == "Pickup_Baldnana" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    this.gc.CollectItem(12);
                }
                else if (raycastHit.transform.name == "Pickup_Stunshot" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    this.gc.CollectItem(13);
                }
                else if (raycastHit.transform.name == "Pickup_BrokenRuler" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    this.gc.CollectItem(14);
                }
                else if (raycastHit.transform.name == "Pickup_JoesWettyMop" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
                {
                    raycastHit.transform.gameObject.SetActive(false);
                    this.gc.CollectItem(15); //Put the item in the player's inventory
                }
				else if (raycastHit.transform.name == "Pickup_PriWhistle" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(16); //Put the item in the player's inventory
				}
				else if (raycastHit.transform.name == "Pickup_BBHinator" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(17); //Put the item in the player's inventory
				}
				else if (raycastHit.transform.name == "Pickup_Teleporter" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(18); //Put the item in the player's inventory
				}
				else if (raycastHit.transform.name == "Pickup_BrokenFogMachine" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(19); //Put the item in the player's inventory
				}
				else if (raycastHit.transform.name == "Pickup_FacultyTag" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(20); //Put the item in the player's inventory
				}
				else if (raycastHit.transform.name == "Pickup_SockMask" & Vector3.Distance(this.player.position, base.transform.position) < 10f)
				{
					raycastHit.transform.gameObject.SetActive(false);
					this.gc.CollectItem(21); //Put the item in the player's inventory
				}
			}
		}
	}

	// Token: 0x04000185 RID: 389
	public GameControllerScript gc;

	// Token: 0x04000186 RID: 390
	public Transform player;
}
