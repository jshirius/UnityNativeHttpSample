using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackHttpNative : MonoBehaviour {

	// ネイティブ側からの戻り値取得
	void CallbackHttpTask (string result) {
		Debug.Log("CallbackHttpNative CallbackHttpTask result:" + result);
	}
	
}
