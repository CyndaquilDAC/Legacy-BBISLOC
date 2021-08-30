using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class BaldiFacesScript : MonoBehaviour
{
    // Token: 0x060001A7 RID: 423 RVA: 0x0000C3C6 File Offset: 0x0000A7C6
    public BaldiFacesScript()
    {
    }

    // Token: 0x060001A8 RID: 424 RVA: 0x0000C3CE File Offset: 0x0000A7CE
    private void Start()
    {
    }

    // Token: 0x060001A9 RID: 425 RVA: 0x0000C3D0 File Offset: 0x0000A7D0
    private void Update()
    {
    }

    // Token: 0x060001AA RID: 426 RVA: 0x0000C3D2 File Offset: 0x0000A7D2
    public void Hear(float priority, bool antiHearing, float current)
    {
        if (!antiHearing)
        {
            if (priority >= current)
            {
                this.BaldiIcon.SetTrigger("Hear");
            }
            else
            {
                this.BaldiIcon.SetTrigger("Think");
            }
        }
    }

    // Token: 0x060001AB RID: 427 RVA: 0x0000C406 File Offset: 0x0000A806
    public void CoolDown()
    {
        this.CoolDownFace = 1.5f;
    }

    // Token: 0x060001AC RID: 428 RVA: 0x0000C413 File Offset: 0x0000A813
    public void EndAnimation()
    {
        this.BaldiIcon.SetBool("Hear", false);
    }

    // Token: 0x0400027F RID: 639
    public Animator BaldiIcon;

    // Token: 0x04000280 RID: 640
    public float CoolDownFace;
}
