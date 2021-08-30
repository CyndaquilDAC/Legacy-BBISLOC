using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MrSpikeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public SpriteRenderer MrSpikeRenderer;
    public Sprite MrSpikeInGround;
    public Sprite MrSpikeOutOfGround;
    public Collider MrSpikeCollider;
    public AudioQueueScript MrSpikeAudioQueue;
    public AudioClip MrSpikeClipOnPlayerCollided;
    public AudioClip MrSpikeSinkingClip;
    public PlayerScript ps;
    public GameControllerScript gc;
    public Collider PlayerCollider;
    public NavMeshAgent MrSpikeAgent;
    public void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            ps.runSpeed = 7;
            ps.walkSpeed = 5;
            MrSpikeAudioQueue.QueueAudio(MrSpikeClipOnPlayerCollided);
            MrSpikeRenderer.sprite = MrSpikeOutOfGround;
            MrSpikeCollider.enabled = false;
            StartCoroutine(PostTriggerEnterCoroutine());
        }
    }

    public IEnumerator PostTriggerEnterCoroutine()
    {
        MrSpikeAgent.speed = 15f;
        MrSpikeAgent.angularSpeed = 15.75f;
        yield return new WaitForSeconds(15);
        ps.runSpeed = 16;
        ps.walkSpeed = 10;
        MrSpikeAudioQueue.QueueAudio(MrSpikeSinkingClip);
        MrSpikeRenderer.sprite = MrSpikeInGround;
        MrSpikeCollider.enabled = true;
        MrSpikeAgent.speed = 0.35f;
        MrSpikeAgent.angularSpeed = 0.5f;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
