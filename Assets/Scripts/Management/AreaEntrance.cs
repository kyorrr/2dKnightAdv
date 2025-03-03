using UnityEngine;

public class AreaEntrance : MonoBehaviour {

	[SerializeField] private string _transitionName;

	private void Start() {
		if (_transitionName == SceneManagement.Instance.SceneTransitionName) {
			PlayerController.Instance.transform.position = this.transform.position;
			CameraController.Instance.SetPlayerCameraFollow();
			UIFade.Instance.FadeToClear();
		}
	}
}
