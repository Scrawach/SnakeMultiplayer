import { Schema, type } from "@colyseus/schema";
import { Vector2Schema } from "./Vector2Schema";

export class AppleSchema extends Schema {
    @type(Vector2Schema) position: Vector2Schema;

    constructor(position: Vector2Schema) {
        super();
        this.position = position;
    }
}