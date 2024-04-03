// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.29
// 

using Colyseus.Schema;
using Action = System.Action;

namespace Network.Schemas {
	public partial class AppleSchema : Schema {
		[Type(0, "ref", typeof(Vector2Schema))]
		public Vector2Schema position = new Vector2Schema();

		/*
		 * Support for individual property change callbacks below...
		 */

		protected event PropertyChangeHandler<Vector2Schema> __positionChange;
		public Action OnPositionChange(PropertyChangeHandler<Vector2Schema> __handler, bool __immediate = true) {
			if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
			__callbacks.AddPropertyCallback(nameof(this.position));
			__positionChange += __handler;
			if (__immediate && this.position != null) { __handler(this.position, null); }
			return () => {
				__callbacks.RemovePropertyCallback(nameof(position));
				__positionChange -= __handler;
			};
		}

		protected override void TriggerFieldChange(DataChange change) {
			switch (change.Field) {
				case nameof(position): __positionChange?.Invoke((Vector2Schema) change.Value, (Vector2Schema) change.PreviousValue); break;
				default: break;
			}
		}
	}
}
