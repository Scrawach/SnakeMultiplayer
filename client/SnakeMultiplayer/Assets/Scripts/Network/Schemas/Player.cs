// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.29
// 

using Colyseus.Schema;
using Action = System.Action;

namespace Network.Schemas
{
	public partial class Player : Schema {
		[Type(0, "ref", typeof(Vector2Data))]
		public Vector2Data position = new Vector2Data();

		[Type(1, "uint8")]
		public byte size = default(byte);

		/*
	 * Support for individual property change callbacks below...
	 */

		protected event PropertyChangeHandler<Vector2Data> __positionChange;
		public Action OnPositionChange(PropertyChangeHandler<Vector2Data> __handler, bool __immediate = true) {
			if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
			__callbacks.AddPropertyCallback(nameof(this.position));
			__positionChange += __handler;
			if (__immediate && this.position != null) { __handler(this.position, null); }
			return () => {
				__callbacks.RemovePropertyCallback(nameof(position));
				__positionChange -= __handler;
			};
		}

		protected event PropertyChangeHandler<byte> __sizeChange;
		public Action OnSizeChange(PropertyChangeHandler<byte> __handler, bool __immediate = true) {
			if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
			__callbacks.AddPropertyCallback(nameof(this.size));
			__sizeChange += __handler;
			if (__immediate && this.size != default(byte)) { __handler(this.size, default(byte)); }
			return () => {
				__callbacks.RemovePropertyCallback(nameof(size));
				__sizeChange -= __handler;
			};
		}

		protected override void TriggerFieldChange(DataChange change) {
			switch (change.Field) {
				case nameof(position): __positionChange?.Invoke((Vector2Data) change.Value, (Vector2Data) change.PreviousValue); break;
				case nameof(size): __sizeChange?.Invoke((byte) change.Value, (byte) change.PreviousValue); break;
				default: break;
			}
		}
	}
}

