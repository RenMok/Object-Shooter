using System.Collections;
using UnityEngine;

public class LaserGunEnemy : Enemy
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private float aimTime;
    private bool aiming = false;

    public override void Move()
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Move toward player if outside of attack distance
        if (Vector2.Distance(target.transform.position, transform.position) > attackDistance)
        {
            base.Move(direction, angle, speed);
        }
        else
        {
            // Stop immediately
            rigidBody.velocity = Vector2.zero;
            // No movement, rotation only
            base.Move(Vector2.zero, angle, 0);
            if (!aiming)
            {
                aiming = true;
                StartCoroutine(AimLaser());
            }
        }
        // flip sprite to face the right direction
        if (direction.x <= 0)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }

    }
        private IEnumerator AimLaser()
    {
        float attackDistanceHolder = attackDistance;
        float time = aimTime;
        attackDistance = Mathf.Infinity;

        while (time > 0)
        {
            laser.positionCount = 2;
            laser.SetPositions(GetPositions());
            time -= Time.deltaTime; 
            yield return null;
        }
        laser.positionCount = 0;
        Attack();
        yield return new WaitForSecondsRealtime(fireRate);
        attackDistance = attackDistanceHolder;
        aiming = false;
    }
    private Vector3[] GetPositions()
    {
        Vector3[] temp = new Vector3[2];
        temp[0] = transform.position;
        temp[1] = target.transform.position;
        return temp;
    }
}
