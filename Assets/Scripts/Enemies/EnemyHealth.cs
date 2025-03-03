using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	[SerializeField] private int _startingHealth = 25;
	[SerializeField] private GameObject _deathVFXPrefab;
	[SerializeField] private float _knockBackThrust = 15f;

	private int _currentHealth;
	private Knockback _knockback;
	private Flash _flash;

	private void Awake() {
		_flash = GetComponent<Flash>();
		_knockback = GetComponent<Knockback>();
	}

	private void Start() {
		_currentHealth = _startingHealth;
	}

	public void TakeDamage(int damage) {
		_currentHealth -= damage;
		_knockback.GetKnockedBack(PlayerController.Instance.transform, _knockBackThrust);
		StartCoroutine(_flash.FlashRoutine());
		StartCoroutine(CheckDetectDeathRoutine());
	}

	public IEnumerator CheckDetectDeathRoutine() {
		yield return new WaitForSeconds(_flash.GetRestoreMatTime());
		DetectDeath();
	}

	public void DetectDeath() {
		if (_currentHealth <= 0) {
			Instantiate(_deathVFXPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
