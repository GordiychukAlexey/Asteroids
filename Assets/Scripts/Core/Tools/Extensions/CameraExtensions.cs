using UnityEngine;

namespace Core.Tools.Extensions {
	public static class CameraExtensions {
		public static Bounds OrthographicBounds(this Camera camera){
			float cameraHeight = camera.orthographicSize;
			float screenAspect = (float) Screen.width / Screen.height;

			float width = cameraHeight * screenAspect;
			float hight = cameraHeight;

			var position = camera.transform.position;

//			Debug.DrawLine(position - new Vector3(width , hight , 0),
//						   position + new Vector3(width , hight , 0));

			return new Bounds(position, new Vector3(width * 2, hight * 2, 0));
		}
	}
}