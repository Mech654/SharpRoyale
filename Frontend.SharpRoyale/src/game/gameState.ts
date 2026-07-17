import type { MatchEvent, MatchAction } from "../services/gameEvents";

interface EntityState {
  id: number;
  entityId: number;
  ownerId: number;
  position: { x: number; y: number };
  lastAction: MatchAction | null;
}

export const gameState = {
  matchId: null as number | null,
  tickId: 0,
  entities: new Map<number, EntityState>(),
};
