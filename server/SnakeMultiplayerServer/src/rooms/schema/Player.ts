import { Schema, type } from "@colyseus/schema";
import { Vector2Data } from "./Vector2Data";

export class Player extends Schema {
  @type(Vector2Data) position: Vector2Data;
  @type("uint8") skinId: number;
  @type("uint8") size: number;
}