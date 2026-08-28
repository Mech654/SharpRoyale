import React, { useMemo } from "react";
import { ENTITY_DATA } from "../game/EntityData";

interface CardProps {
  entityId: number;
  onPointerDown: (e: React.PointerEvent) => void;
}

const PX_PER_UNIT = 52;

const Card = ({ entityId, onPointerDown }: CardProps) => {
  console.log("Rendering card for entityId:", entityId); // Debugging line
  const entity = ENTITY_DATA[entityId];

  if (entity.isConstruction) {
    const [w, h] = entity.size;
    return (
      <div className="card" onPointerDown={onPointerDown}>
        <div
          className="entity-shape entity-shape--rect"
          style={{
            width: `${w * PX_PER_UNIT}px`,
            height: `${h * PX_PER_UNIT}px`,
            backgroundColor: entity.color,
          }}
        />
      </div>
    );
  }

  const diameter = entity.radius * 2 * PX_PER_UNIT;
  return (
    <div className="card" onPointerDown={onPointerDown}>
      <div
        className="entity-shape entity-shape--circle"
        style={{
          width: `${diameter}px`,
          height: `${diameter}px`,
          backgroundColor: entity.color,
        }}
      />
    </div>
  );
};

export default Card;
