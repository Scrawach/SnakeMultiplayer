import { Schema, MapSchema, type } from "@colyseus/schema";
import { PlayerSchema } from "./PlayerSchema";
import { Vector2Schema } from "./Vector2Schema";
import { StaticData } from "../../services/staticData";
import { AppleSchema } from "./AppleSchema";

export class GameRoomState extends Schema {
    readonly mapSize: number = 50;

    @type({ map: PlayerSchema }) players = new MapSchema<PlayerSchema>();
    @type({ map: AppleSchema}) apples = new MapSchema<AppleSchema>();

    staticData: StaticData;
    lastAppleId: number;

    constructor(staticData: StaticData) {
        super();
        this.staticData = staticData;
    }

    createApple() : AppleSchema {
        const data = new AppleSchema(this.getSpawnPoint(this.mapSize));
        this.apples.set(String(this.lastAppleId), data);
        this.lastAppleId++;
        return data;
    }

    createPlayer(sessionId: string): PlayerSchema {
        const player = new PlayerSchema(this.getSpawnPoint(this.mapSize), this.getRandomSkinId(), 1);
        this.players.set(sessionId, player);
        console.log(player.skinId);
        return player;
    }

    removePlayer(sessionId: string) {
        this.players.delete(sessionId);
    }

    getRandomSkinId(): number {
        return Math.floor(Math.random() * this.staticData.getAvailableSkinCount());
    }

    getSpawnPoint(size: number): Vector2Schema {
        const x = Math.floor(Math.random() * size) - size / 2;
        const y = Math.floor(Math.random() * size) - size / 2;
        return new Vector2Schema(x, y);
    }

    movePlayer(sessionId: string, targetPosition: Vector2Schema) {
        const player = this.players.get(sessionId);
        player.position = targetPosition;
    }
}