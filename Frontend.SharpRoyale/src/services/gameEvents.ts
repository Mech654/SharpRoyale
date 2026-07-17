import { gameState } from "../game/gameState";
export interface MatchAction {
  option: number;
  id: number;
  entityId: number;
  ownerId: number;
  values: SpawnValues | AttackValues;
  time: string;
}

export interface MatchEvent {
  matchId: number;
  tickId: number;
  actions: MatchAction[];
}

interface SpawnValues {
  position: { x: number; y: number };
  entityId: number;
}

interface AttackValues {
  dummy: number;
}

function isSpawnValues(val: unknown): val is SpawnValues {
  if (typeof val !== "object" || val === null) return false;
  const obj = val as Record<string, unknown>;

  if (typeof obj.entityId !== "number") return false;

  const pos = obj.position;
  if (typeof pos !== "object" || pos === null) return false;
  const posObj = pos as Record<string, unknown>;

  return typeof posObj.x === "number" && typeof posObj.y === "number";
}

export function applyMatchEvent(event: MatchEvent) {
  for (const action of event.actions) {
    switch (action.option) {
      case 0: // spawn
        console.log("Applying spawn action:", action);
        applySpawnAction(action);
        break;
    }
  }
  gameState.tickId = event.tickId;
}

function applySpawnAction(action: MatchAction) {
  if (!isSpawnValues(action.values)) {
    console.error("Invalid spawn values:", action.values);
    return;
  }
  // TODO: Implement spawn action logic
  gameState.entities.set(action.id, {
    id: action.id,
    entityId: action.entityId,
    ownerId: action.ownerId,
    lastAction: action,
    position: action.values.position,
  });
}
