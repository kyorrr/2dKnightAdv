using UnityEngine;

public class DamageSource : MonoBehaviour {

	[SerializeField] private int _damageAmount = 5;

	private void OnTriggerEnter2D(Collider2D other) {
		EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
		enemyHealth?.TakeDamage(_damageAmount);
	}
}
