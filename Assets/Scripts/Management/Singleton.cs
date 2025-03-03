using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {

	private static T _instance;
	public static T Instance { get { return _instance; } }

	protected virtual void Awake() {
		if (_instance != null && this.gameObject != null) {
			Destroy(this.gameObject);
		}
		else {
			_instance = (T)this;
		}
		if (!gameObject.transform.parent) {
			DontDestroyOnLoad(gameObject);
		}
	}
}
