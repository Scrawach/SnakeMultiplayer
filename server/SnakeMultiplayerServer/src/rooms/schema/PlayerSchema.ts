import { Schema, type } from "@colyseus/schema";
import { Vector2Schema } from "./Vector2Schema";

export class PlayerSchema extends Schema {
  @type("string") username: string;
  @type(Vector2Schema) position: Vector2Schema;
  @type("uint8") skinId: number;
  @type("uint8") size: number;
  @type("uint16") score: number;

  constructor(username: string, position: Vector2Schema, skinId: number, score: number, size: number = 2) {
    super();
    this.username = username;
    this.position = position;
    this.skinId = skinId;
    this.score = score;
    this.size = size;
  }

  addScore(count: number) {
    this.score += count;
    
    let nextSize = 2 + Math.floor(this.score / 3);

    if (nextSize > 255) {
      nextSize = 255;
    }

    this.size = nextSize;
  }
}