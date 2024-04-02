import { Schema, MapSchema, type } from "@colyseus/schema";
import { Player } from "./Player";
import { Vector2Data } from "./Vector2Data";

export class GameRoomState extends Schema {
    readonly mapSize: number = 256;

    @type({ map: Player }) players = new MapSchema<Player>();

    createPlayer(sessionId: string): Player {
        const player = new Player();
        player.position = this.getSpawnPoint();
        this.players.set(sessionId, player);
        return player;
    }

    removePlayer(sessionId: string) {
        this.players.delete(sessionId);
    }

    getSpawnPoint(): Vector2Data {
        const position = new Vector2Data();
        position.x = Math.floor(Math.random() * this.mapSize) - this.mapSize / 2;
        position.y = Math.floor(Math.random() * this.mapSize) - this.mapSize / 2;
        return position;
    }

    movePlayer(sessionId: string, targetPosition: Vector2Data) {
        const player = this.players.get(sessionId);
        player.position = targetPosition;
    }
}