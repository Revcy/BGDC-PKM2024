using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherAnimations : MonoBehaviour
{
    private Animator anim;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimationUpdate();
    }

    // Update is called once per frame
    void UpdateAnimationUpdate()
    {
        if (playerMovement != null)
        {
            anim.SetBool("FoamSpraying", playerMovement.isShootingFoam);
            anim.SetBool("CO2Spraying", playerMovement.isShootingCO2);
            anim.SetBool("DryChemSpraying", playerMovement.isShootingDryChem);
            anim.SetBool("WetChemSpraying", playerMovement.isShootingWetChem);
            anim.SetBool("DryPowSpraying", playerMovement.isShootingDryPow);
        }
    }
}
