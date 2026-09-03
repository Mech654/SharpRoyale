import * as conn from "./WSconnection.ts";

export function sendSpawnAction(
  action: string,
  values: conn.SpawnActionValues,
) {
  conn.sendSpawnAction(action, values);
}
