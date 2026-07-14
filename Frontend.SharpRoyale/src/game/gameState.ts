import type { MatchEvent, MatchAction } from "../services/gameEvents";

interface EntityState {
  entityId: number;
  ownerId: number;
  lastAction: MatchAction | null;
}

export const gameState = {
  matchId: null as number | null,
  tickId: 0,
  entities: new Map<number, EntityState>(),
};

export function applyEvent(event: MatchEvent) {
  gameState.matchId = event.matchId;
  gameState.tickId = event.tickId;

  for (const action of event.actions) {
    console.log("applyEvent called", event.tickId);
    const existing = gameState.entities.get(action.entityId);
    gameState.entities.set(action.entityId, {
      entityId: action.entityId,
      ownerId: action.ownerId,
      lastAction: action,
    });
  }
}
