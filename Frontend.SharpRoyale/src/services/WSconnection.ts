import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import type { MatchEvent } from "./gameEvents";

export function connectToMatch(
  matchId: number,
  onEvent: (data: MatchEvent) => void,
): () => void {
  console.log("Connecting to match with ID:", matchId);
  const connection: HubConnection = new HubConnectionBuilder()
    .withUrl(`http://localhost:5182/hubs/match/${matchId}`)
    .withAutomaticReconnect()
    .build();

  connection.on("TickResult", (data) => {
    console.log("Received MatchEvent:", data);
    onEvent(data);
  });

  connection
    .start()
    .catch((err) => console.error("SignalR connection failed:", err));

  return () => {
    connection.stop();
  };
}
