using UnityEngine;

public class SlashAnim : MonoBehaviour {

	private ParticleSystem _ps;

	private void Awake() {
		_ps = GetComponent<ParticleSystem>();
	}

	private void Update() {
		if (_ps && !_ps.IsAlive()) {
			DestroySelf();
		}
	}

	public void DestroySelf() {
		Destroy(gameObject);
	}
}
