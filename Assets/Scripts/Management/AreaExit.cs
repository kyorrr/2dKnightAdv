using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {

	[SerializeField] private string _sceneToLoad;
	[SerializeField] private string _sceneTransitionName;

	private float _waitToLoadTime = 1f;

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.GetComponent<PlayerController>()) {
			SceneManagement.Instance.SetTransitionName(_sceneTransitionName);
			UIFade.Instance.FadeToBlack();
			StartCoroutine(LoadSceneRoutine());
		}
	}

	private IEnumerator LoadSceneRoutine() {
		while (_waitToLoadTime >= 0) {
			_waitToLoadTime -= Time.deltaTime;
			yield return null;
		}
		SceneManager.LoadScene(_sceneToLoad);
	}
}
