export interface MatchAction {
  option: number;
  entityId: number;
  ownerId: number;
  values: any;
  time: string;
}

export interface MatchEvent {
  matchId: number;
  tickId: number;
  actions: MatchAction[];
}
