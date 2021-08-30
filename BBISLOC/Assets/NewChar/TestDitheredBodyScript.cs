using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000016 RID: 22
public class TestDitheredBodyScript : MonoBehaviour
{
	// Token: 0x06000071 RID: 113
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
		this.audioQueue = base.GetComponent<AudioQueueScript>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.Wander();
		base.GetComponent<CapsuleCollider>().radius = 2.5f;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003679 File Offset: 0x00001879
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x06000073 RID: 115
	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			this.Ready = true;
			this.Stopped();
			this.audioQueue.QueueAudio(this.aud_spot);
			this.audioQueue.QueueAudio(this.aud_loop1);
			this.audioQueue.ClearAudioQueue();
			return;
		}
		this.Ready = false;
		if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
		{
			this.Wander();
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003774 File Offset: 0x00001974
	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & this.Ready)
		{
			base.StartCoroutine(this.Collided());
		}
	}

	// Token: 0x06000075 RID: 117
	private IEnumerator Collided()
	{
		base.GetComponent<Collider>().enabled = false;
		RenderSettings.ambientLight = Color.white;
		this.Ready = false;
		this.Body.SetActive(false);
		RenderSettings.fogColor = this.black;
		RenderSettings.fog = true;
		this.audioQueue.QueueAudio(this.aud_loop2);
		this.agent.isStopped = true;
		yield return new WaitForSeconds(70f);
		this.Body.SetActive(true);
		RenderSettings.fogColor = this.black;
		RenderSettings.fog = false;
		this.Wander();
		this.Ready = true;
		base.GetComponent<Collider>().enabled = true;
		yield break;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000037AB File Offset: 0x000019AB
	private void Stopped()
	{
		this.agent.isStopped = true;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000037B9 File Offset: 0x000019B9
	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
	}

	// Token: 0x0600055F RID: 1375
	private void OnMouseOver()
	{
		if ((Vector3.Distance(this.player.position, base.transform.position) <= 65f & this.Ready) && !this.Blinking)
		{
			base.StartCoroutine(this.Blink());
		}
	}

	// Token: 0x06000560 RID: 1376
	private void OnMouseExit()
	{
		RenderSettings.ambientLight = Color.white;
	}

	// Token: 0x06000662 RID: 1634
	private IEnumerator Blink()
	{
		this.Blinking = true;
		this.audioQueue.QueueAudio(this.aud_creepyCollided);
		RenderSettings.ambientLight = Color.grey;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.white;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.grey;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.white;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.grey;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.white;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.grey;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.white;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.grey;
		yield return new WaitForSeconds(0.3f);
		RenderSettings.ambientLight = Color.white;
		this.Blinking = false;
		yield break;
	}

	// Token: 0x0400005B RID: 91
	public AILocationSelectorScript wanderer;

	// Token: 0x0400005C RID: 92
	public Transform wanderTarget;

	// Token: 0x0400005D RID: 93
	public Color black;

	// Token: 0x0400005E RID: 94
	public Transform player;

	// Token: 0x0400005F RID: 95
	public GameObject Body;

	// Token: 0x04000060 RID: 96
	public GameObject playerTorch;

	// Token: 0x04000061 RID: 97
	public float coolDown;

	// Token: 0x04000062 RID: 98
	private NavMeshAgent agent;

	// Token: 0x04000063 RID: 99
	public AudioSource audioDevice;

	// Token: 0x04000064 RID: 100
	public bool Ready;

	// Token: 0x04000065 RID: 101
	public AudioClip aud_creepyCollided;

	// Token: 0x04000066 RID: 102
	public AudioClip aud_spot;

	// Token: 0x04000067 RID: 103
	public AudioClip aud_loop1;

	// Token: 0x04000068 RID: 104
	public AudioClip aud_loop2;

	// Token: 0x04000069 RID: 105
	public AudioQueueScript audioQueue;

	// Token: 0x0400092C RID: 2348
	public bool Blinking;
}
