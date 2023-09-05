using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tools.ServiceLocator {
	public static class ServiceLocator {
		private static readonly HashSet<ServiceLocatorItem> _registeredObjects;

		static ServiceLocator(){
			_registeredObjects = new HashSet<ServiceLocatorItem>();
		}

		public static ServiceLocatorRegisterCommand<TObject> Bind<TObject>(TObject obj) where TObject : class{
			return new ServiceLocatorRegisterCommand<TObject>(new ServiceLocatorItem(obj));
		}

		public static void Bind<TObject>(ServiceLocatorRegisterCommand<TObject> command){
			_registeredObjects.Add(command.Item);
		}

		public static T Resolve<T>(string tag = null) where T : class{
			var obj = _registeredObjects.SingleOrDefault(x => x.objectInstance is T && x.tag == tag)?.objectInstance;
			if (obj != null) return obj as T;

			throw new ArgumentException($"Unregistered type {typeof(T)}{(tag == null ? "" : $" with tag {tag}")}");
		}

		public static void Unbind<T>(T obj) where T : class{
			var item = _registeredObjects.SingleOrDefault(x => x.objectInstance == obj);
			_registeredObjects.Remove(item);
		}
	}
}