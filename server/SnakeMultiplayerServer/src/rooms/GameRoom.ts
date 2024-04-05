import { Room, Client } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { Vector2Schema } from "./schema/Vector2Schema";
import { StaticData } from "../services/staticData";

export class GameRoom extends Room<GameRoomState> {
  readonly startApplesCount: number = 100;

  onCreate (options: any) {
    console.log("Game Room created!")
    const staticData = new StaticData();
    staticData.initialize();

    this.setState(new GameRoomState(staticData));
    
    this.onMessage("move", (client, data) => {
      const position = new Vector2Schema(data.position.x, data.position.y);
      this.state.movePlayer(client.sessionId, position);
    });

    this.onMessage("collectApple", (client, data) => {
      this.state.collectApple(client.sessionId, data.appleId);
    })

    this.onMessage("snakeDeath", (client, data) => {
      this.state.processSnakeDeath(data.snakeId, data.positions);
    })

    for (var i = 0; i < this.startApplesCount; i++) {
      this.state.createAppleAtRandomPosition();
    }
  }

  onAuth(client: Client, options: any, req: any) {
    return true;
  }

  onJoin (client: Client, options: any) {
    console.log(client.sessionId, "joined!", options.username);
    this.state.createPlayer(client.sessionId, options.username);
  }

  onLeave (client: Client, consented: boolean) {
    console.log(client.sessionId, "left!");
    this.state.removePlayer(client.sessionId);
  }

  onDispose() {
    console.log("room", this.roomId, "disposing...");
  }
}
