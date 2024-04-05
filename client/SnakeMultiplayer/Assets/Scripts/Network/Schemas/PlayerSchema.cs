// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.29
// 

using Colyseus.Schema;
using Action = System.Action;

namespace Network.Schemas {
	public partial class PlayerSchema : Schema {
		[Type(0, "string")]
		public string username = default(string);

		[Type(1, "ref", typeof(Vector2Schema))]
		public Vector2Schema position = new Vector2Schema();

		[Type(2, "uint8")]
		public byte skinId = default(byte);

		[Type(3, "uint8")]
		public byte size = default(byte);

		[Type(4, "uint16")]
		public ushort score = default(ushort);

		/*
		 * Support for individual property change callbacks below...
		 */

		protected event PropertyChangeHandler<string> __usernameChange;
		public Action OnUsernameChange(PropertyChangeHandler<string> __handler, bool __immediate = true) {
			if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
			__callbacks.AddPropertyCallback(nameof(this.username));
			__usernameChange += __handler;
			if (__immediate && this.username != default(string)) { __handler(this.username, default(string)); }
			return () => {
				__callbacks.RemovePropertyCallback(nameof(username));
				__usernameChange -= __handler;
			};
		}

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

		protected event PropertyChangeHandler<byte> __skinIdChange;
		public Action OnSkinIdChange(PropertyChangeHandler<byte> __handler, bool __immediate = true) {
			if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
			__callbacks.AddPropertyCallback(nameof(this.skinId));
			__skinIdChange += __handler;
			if (__immediate && this.skinId != default(byte)) { __handler(this.skinId, default(byte)); }
			return () => {
				__callbacks.RemovePropertyCallback(nameof(skinId));
				__skinIdChange -= __handler;
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

		protected event PropertyChangeHandler<ushort> __scoreChange;
		public Action OnScoreChange(PropertyChangeHandler<ushort> __handler, bool __immediate = true) {
			if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
			__callbacks.AddPropertyCallback(nameof(this.score));
			__scoreChange += __handler;
			if (__immediate && this.score != default(ushort)) { __handler(this.score, default(ushort)); }
			return () => {
				__callbacks.RemovePropertyCallback(nameof(score));
				__scoreChange -= __handler;
			};
		}

		protected override void TriggerFieldChange(DataChange change) {
			switch (change.Field) {
				case nameof(username): __usernameChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
				case nameof(position): __positionChange?.Invoke((Vector2Schema) change.Value, (Vector2Schema) change.PreviousValue); break;
				case nameof(skinId): __skinIdChange?.Invoke((byte) change.Value, (byte) change.PreviousValue); break;
				case nameof(size): __sizeChange?.Invoke((byte) change.Value, (byte) change.PreviousValue); break;
				case nameof(score): __scoreChange?.Invoke((ushort) change.Value, (ushort) change.PreviousValue); break;
				default: break;
			}
		}
	}
}
