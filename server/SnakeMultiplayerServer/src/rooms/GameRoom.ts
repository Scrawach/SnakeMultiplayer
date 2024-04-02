import { Room, Client } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { Vector2Data } from "./schema/Vector2Data";
import { StaticData } from "../services/staticData";

export class GameRoom extends Room<GameRoomState> {

  onCreate (options: any) {
    console.log("Game Room created!")
    const staticData = new StaticData();
    staticData.initialize();

    this.setState(new GameRoomState(staticData));
    
    this.onMessage("move", (client, data) => {
      const position = new Vector2Data();
      position.x = data.position.x;
      position.y = data.position.y;
      this.state.movePlayer(client.sessionId, position);
    });
  }

  onAuth(client: Client, options: any, req: any) {
    return true;
  }

  onJoin (client: Client, options: any) {
    console.log(client.sessionId, "joined!");
    this.state.createPlayer(client.sessionId);
  }

  onLeave (client: Client, consented: boolean) {
    console.log(client.sessionId, "left!");
    this.state.removePlayer(client.sessionId);
  }

  onDispose() {
    console.log("room", this.roomId, "disposing...");
  }
}
