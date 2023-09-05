namespace Core.Tools.ServiceLocator {
	public class ServiceLocatorRegisterCommand<TObject> {
		public ServiceLocatorItem Item{ get; }

		public ServiceLocatorRegisterCommand(ServiceLocatorItem item){
			Item = item;
		}

		public ServiceLocatorRegisterCommand<TObject> WithTag(string tag){
			Item.tag = tag;
			return this;
		}

		public void AsSingle(){
			ServiceLocator.Bind(this);
		}
	}
}