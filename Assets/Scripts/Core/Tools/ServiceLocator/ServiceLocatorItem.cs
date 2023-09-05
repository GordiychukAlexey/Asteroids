using UnityEngine;

namespace Core.Tools.ServiceLocator {
	public class ServiceLocatorItem {
		public object objectInstance{ get; }

		public string tag{ get; set; } = null;
//		public Transform parentTransform{ get; set; } = null;

		public ServiceLocatorItem(object objectInstance){
			this.objectInstance = objectInstance;
		}
	}
}