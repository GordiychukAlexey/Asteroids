using System;

namespace Core.Input {
	public interface IShootingInput {
		public Action Fire{ get; set; }
	}
}