using UnityEngine;

namespace Core.Tools.Extensions {
	public static class Vector2Extensions {
		public static float PosNegAngle(this Vector2 vector){
			float angle = Vector2.Angle(vector, Vector2.right);
			float sign = Mathf.Sign(Vector2.Dot(vector, Vector2.up));
			return angle * sign;
		}
	}
}