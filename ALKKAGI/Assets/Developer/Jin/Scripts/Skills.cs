using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : Singleton<Skills>
{
    public GameObject player;
    private bool useSkill = false;

    public IEnumerator Skill_Pawn()
    {
        if (!useSkill)
        {
            useSkill = true;
            float spd = player.GetComponent<Player>().speed;

            // �̵��ӵ� ����
            player.GetComponent<Player>().speed = spd * 2;

            yield return new WaitForSeconds(5f);
            player.GetComponent<Player>().speed = spd;
            useSkill = false;
        }
    }
    public IEnumerator Skill_Elephant()
    {
        // ���� �̵��ӵ� ����
        if (!useSkill)
        {
            useSkill = true;

            yield return new WaitForSeconds(5f);
            useSkill = false;
        }
    }
    public IEnumerator Skill_Knight()
    {
        // ���� or �߰� ���
        if (!useSkill)
        {
            useSkill = true;

            yield return new WaitForSeconds(5f);
            useSkill = false;
        }
    }
    public IEnumerator Skill_Cannon(Vector3 teleportPos)
    {
        // �����̵�
        if (!useSkill)
        {
            useSkill = true;

            player.transform.position = teleportPos;
            yield return new WaitForSeconds(5f);
            useSkill = false;
        }
    }
    public IEnumerator Skill_Rook()
    {
        // ����
        if(!useSkill)
        {
            useSkill = true;
            Vector3 forwardDirection = Vector3.forward;
            player.GetComponent<Rigidbody>().AddForce(forwardDirection * 20f, ForceMode.Impulse);

            yield return new WaitForSeconds(5f);
            useSkill = false;
        }
    }
    public IEnumerator Skill_Guards()
    {
        // ����ź
        if (!useSkill)
        {
            useSkill = true;

            yield return new WaitForSeconds(5f);
            useSkill = false;
        }
    }
    public IEnumerator Skill_King()
    {
        // ?
        if (!useSkill)
        {
            useSkill = true;

            yield return new WaitForSeconds(5f);
            useSkill = false;
        }
    }
}
