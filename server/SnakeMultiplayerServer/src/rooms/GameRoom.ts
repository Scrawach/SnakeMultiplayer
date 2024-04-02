import { Room, Client } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";

export class GameRoom extends Room<GameRoomState> {

  onCreate (options: any) {
    console.log("Game Room created!")
    this.setState(new GameRoomState());
    this.onMessage("move", (client, data) => {
      this.state.movePlayer(client.sessionId, data);
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
