using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour {

	public bool GettingKnockedBack {  get; private set; }

	[SerializeField] private float _knockBackTime = 0.2f;

	private Rigidbody2D _rigidbody2D;

	private void Awake() {
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void GetKnockedBack(Transform damageSource, float knockBackThrust) {
		GettingKnockedBack = true;
		Vector2 diff = (transform.position - damageSource.position).normalized * knockBackThrust * _rigidbody2D.mass;
		_rigidbody2D.AddForce(diff, ForceMode2D.Impulse);
		StartCoroutine(KnockRoutine());
	}

	private IEnumerator KnockRoutine() {
		yield return new WaitForSeconds(_knockBackTime);
		_rigidbody2D.velocity = Vector2.zero;
		GettingKnockedBack = false;
	}
}
