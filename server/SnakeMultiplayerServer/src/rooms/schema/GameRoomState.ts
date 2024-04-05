import { Schema, MapSchema, type } from "@colyseus/schema";
import { PlayerSchema } from "./PlayerSchema";
import { Vector2Schema } from "./Vector2Schema";
import { StaticData } from "../../services/staticData";
import { AppleSchema } from "./AppleSchema";

export class GameRoomState extends Schema {
    readonly mapSize: number = 140;
    readonly scorePerApple: number = 1;
    readonly maxApplesOnRoom: number = 150;

    @type({ map: PlayerSchema }) players = new MapSchema<PlayerSchema>();
    @type({ map: AppleSchema}) apples = new MapSchema<AppleSchema>();

    staticData: StaticData;
    lastAppleId: number = 0;
    processedDeaths: Set<string>;

    constructor(staticData: StaticData) {
        super();
        this.staticData = staticData;
        this.processedDeaths = new Set<string>();
    }

    createAppleAtRandomPosition() : AppleSchema {
        return this.createApple(this.getSpawnPoint(this.mapSize));
    }

    createApple(position: Vector2Schema) : AppleSchema {
        const data = new AppleSchema(position);
        this.apples.set(String(this.lastAppleId), data);
        this.lastAppleId++;
        return data;
    }

    collectApple(sessionId: string, appleId: string) {
        const player = this.players.get(sessionId);
        const apple = this.apples.get(appleId);
        player.addScore(this.scorePerApple);
        
        if (this.apples.size > this.maxApplesOnRoom) {
            this.apples.delete(appleId);
        } else {
            apple.position = this.getSpawnPoint(this.mapSize);
        }
    }

    createPlayer(sessionId: string, username: string): PlayerSchema {
        const player = new PlayerSchema(username, this.getSpawnPoint(this.mapSize), this.getRandomSkinId(), 1);
        this.players.set(sessionId, player);
        return player;
    }

    removePlayer(sessionId: string) {
        if (this.players.has(sessionId)){
            this.players.delete(sessionId);
        }
    }

    processSnakeDeath(snakeId: string, positions: any) {
        if (this.processedDeaths.has(snakeId))
            return;

        this.removePlayer(snakeId);
        this.processedDeaths.add(snakeId);
        this.removeIdFromProcessedDeathAfterDelay(snakeId, 10_000);

        for (var i = 0; i < positions.length; i++) {
            const worldPosition = new Vector2Schema(positions[i].x, positions[i].y);
            this.createApple(worldPosition);
        }
    }

    async removeIdFromProcessedDeathAfterDelay(snakeId: string, delayInMilliseconds: number) {
        await new Promise(resolve => setTimeout(resolve, delayInMilliseconds));
        this.processedDeaths.delete(snakeId);
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