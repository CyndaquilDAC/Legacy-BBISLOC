using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000025 RID: 37
public class MrCutsScript : MonoBehaviour
{
    // Token: 0x060000AE RID: 174 RVA: 0x0000653A File Offset: 0x0000493A
    private void Start()
    {
        this.agent = base.GetComponent<NavMeshAgent>(); // Define the AI Agent
        this.Wander(); //Start wandering
        this.mrcutsnoscissors.SetActive(true);
        this.mrcutsscissors.SetActive(false);
        this.mrcutssnipsound.SetActive(false);
    }

    // Token: 0x060000AF RID: 175 RVA: 0x0000654E File Offset: 0x0000494E
    private void Update()
    {
        if (this.coolDown > 0f)
        {
            this.coolDown -= 1f * Time.deltaTime;
        }
    }

    // Token: 0x060000B0 RID: 176 RVA: 0x00006578 File Offset: 0x00004978
    private void FixedUpdate()
    {
        if (this.playscrpt.jumpRopeStarted == true) //Check if the player has started jumproping
        {
            this.jumping = true; //If the player has started playtime's jumprope minigame
            this.TargetPlayer(); //Head towards the player
        }
        else
        {
            this.jumping = false; //If the player is not jumproping with playtime
            if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
            {
                this.Wander(); //Just wander
            }
        }
    }

    // Token: 0x060000B1 RID: 177 RVA: 0x00006629 File Offset: 0x00004A29
    private void Wander()
    {
        this.wanderer.GetNewTarget(); //Get a new target on the map
        this.agent.SetDestination(this.wanderTarget.position); //Set its destination to position of the wanderTarget
        this.agent.speed = 13f; //Set the character's speed to 13
        this.coolDown = 1f;
        this.mrcutsnoscissors.SetActive(true);
        this.mrcutsscissors.SetActive(false);
        this.mrcutssnipsound.SetActive(false);
    }

    // Token: 0x060000B2 RID: 178 RVA: 0x00006658 File Offset: 0x00004A58
    private void TargetPlayer()
    {
        this.agent.SetDestination(this.player.position); //Target the player
        this.agent.speed = 20f; //Increase character's speed to 20
        this.coolDown = 1f;
        this.mrcutsnoscissors.SetActive(false);
        this.mrcutsscissors.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.playscrpt.jumpRopeStarted == true) //If the character touches the player while jumproping
        {
            this.psc.DeactivateJumpRope(); //Deactivate (Cut) Playtime's Jumprope
            this.playscrpt.Disappoint(); //Make playtime sad after cutting her rope
            this.mrcutsnoscissors.SetActive(true);
            this.mrcutsscissors.SetActive(false);
            this.mrcutssnipsound.SetActive(true);
        }
    }

    // Token: 0x04000125 RID: 293
    public bool db;

    // Token: 0x04000126 RID: 294
    public Transform player; //the player

    // Token: 0x04000127 RID: 295
    public Transform wanderTarget;

    // Token: 0x04000128 RID: 296
    public AILocationSelectorScript wanderer;

    // Token: 0x04000129 RID: 297
    public float coolDown;

    // Token: 0x0400012A RID: 298
    private NavMeshAgent agent;

    public PlaytimeScript playscrpt; //playtime's script

    public bool jumping; //test if the player is jumping

    public PlayerScript psc; //the player's script

    public GameObject mrcutsnoscissors;

    public GameObject mrcutsscissors;

    public GameObject mrcutssnipsound;
}
