import React from "react";
import Card from "./card";
import { getDeck } from "../services/deckService";

interface DeckContainerProps {
  onCardPointerDown: (cardId: number, e: React.PointerEvent) => void;
}

const DeckContainer = ({ onCardPointerDown }: DeckContainerProps) => {
  return (
    <div className="deck-container">
      {Object.entries(getDeck()).map(([slotId, entityId]) => (
        <Card
          entityId={entityId}
          key={slotId}
          onPointerDown={(e) => onCardPointerDown(Number(slotId), e)}
        />
      ))}
    </div>
  );
};

export default DeckContainer;
