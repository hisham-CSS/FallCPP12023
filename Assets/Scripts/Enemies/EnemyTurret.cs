using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class EnemyTurret : Enemy
{
    Shoot shootScript;
    AudioSourceManager asm;

    public AudioClip shootSound;
    public float projectileFireRate;
    public float distThreshold;

    float timeSinceLastFire = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        shootScript = GetComponent<Shoot>();
        asm = GetComponent<AudioSourceManager>();

        if (!shootScript) Debug.Log("Shoot Script not added");
        if (!asm) Debug.Log("ASM not added!");

        if (projectileFireRate <= 0)
            projectileFireRate = 2;

        if (distThreshold <= 0)
            distThreshold = 3.0f;

        shootScript.OnProjectileSpawned += PlayShootSound;
    }

    void PlayShootSound()
    {
        asm.PlayOneShot(shootSound, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerInstance == null) return;
        if (GameManager.Instance.playerInstance.transform.position.x < transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        float distance = Vector3.Distance(GameManager.Instance.playerInstance.transform.position, transform.position);

        if (distance <= distThreshold)
        {
            sr.color = Color.red;
            if (curPlayingClips[0].clip.name != "Fire")
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                }
            }
        }
        else
        {
            sr.color = Color.white;
        }
    }
}
