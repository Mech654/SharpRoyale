import React from "react";
import Card from "./card";

let dummyDeck = {
  0: 3,
  1: 3,
  2: 3,
};

interface DeckContainerProps {
  onCardPointerDown: (cardId: number, e: React.PointerEvent) => void;
}

const DeckContainer = ({ onCardPointerDown }: DeckContainerProps) => {
  return (
    <div className="deck-container">
      {Object.entries(dummyDeck).map(([slotId, entityId]) => (
        <Card
          entityId={entityId}
          key={slotId}
          onPointerDown={(e) => onCardPointerDown(entityId, e)}
        />
      ))}
    </div>
  );
};

export default DeckContainer;
