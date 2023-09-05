using UnityEngine;

namespace Core.Tools.ServiceLocator {
	public class ServiceLocatorRegisterCommand<TObject> {
		public ServiceLocatorItem Item{ get; }

		public ServiceLocatorRegisterCommand(ServiceLocatorItem item){
			Item = item;
		}


//		public ServiceLocatorRegisterCommand<TObject> FromNew(){
//			Item.objectInstance=new TObject();
//			return this;
//		}

//		public ServiceLocatorRegisterCommand<TObject> FromInstance(TObject instance){
//			Item.objectInstance=instance;
//			return this;
//		}

		public ServiceLocatorRegisterCommand<TObject> WithTag(string tag){
			Item.tag = tag;
			return this;
		}

//		public ServiceLocatorRegisterCommand<TObject> UnderTransform(Transform parentTransform){
//			Item.parentTransform = parentTransform;
//			return this;
//		}

		public void AsSingle(){
			ServiceLocator.Bind(this);
		}
	}
}