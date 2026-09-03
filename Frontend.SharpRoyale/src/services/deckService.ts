let dummyDeck: Record<number, number> = {
  0: 3,
  1: 3,
  2: 3,
};

export function getDeck() {
  return dummyDeck;
}

export function getCardEntityId(slotId: number): number | undefined {
  return dummyDeck[slotId];
}
