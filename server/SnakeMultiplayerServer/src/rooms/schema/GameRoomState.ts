import { Schema, MapSchema, type } from "@colyseus/schema";
import { Player } from "./Player";
import { Vector2Data } from "./Vector2Data";
import { StaticData } from "../../services/staticData";

export class GameRoomState extends Schema {
    readonly mapSize: number = 50;

    @type({ map: Player }) players = new MapSchema<Player>();

    staticData: StaticData

    constructor(staticData: StaticData) {
        super();
        this.staticData = staticData;
    }

    createPlayer(sessionId: string): Player {
        const player = new Player();
        player.position = this.getSpawnPoint();
        player.size = 1;
        player.skinId = this.getRandomSkinId();
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