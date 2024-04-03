import { Schema, type } from "@colyseus/schema";
import { Vector2Schema } from "./Vector2Schema";

export class PlayerSchema extends Schema {
  @type(Vector2Schema) position: Vector2Schema;
  @type("uint8") skinId: number;
  @type("uint8") size: number;

  constructor(position: Vector2Schema, skinId: number, size: number) {
    super();
    this.position = position;
    this.skinId = skinId;
    this.size = size;
  }
}