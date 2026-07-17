import { gameState } from "./gameState";

const TILE_COLS = 18;
const TILE_ROWS = 32;

export function renderFrame(ctx: CanvasRenderingContext2D) {
  const canvasWidth = ctx.canvas.width;
  const canvasHeight = ctx.canvas.height;

  const tileWidth = canvasWidth / TILE_COLS;
  const tileHeight = canvasHeight / TILE_ROWS;

  ctx.clearRect(0, 0, canvasWidth, canvasHeight);

  // Draw the tile map grid
  ctx.strokeStyle = "#3a3a3a";
  ctx.lineWidth = 1;
  for (let col = 0; col <= TILE_COLS; col++) {
    const x = col * tileWidth;
    ctx.beginPath();
    ctx.moveTo(x, 0);
    ctx.lineTo(x, canvasHeight);
    ctx.stroke();
  }
  for (let row = 0; row <= TILE_ROWS; row++) {
    const y = row * tileHeight;
    ctx.beginPath();
    ctx.moveTo(0, y);
    ctx.lineTo(canvasWidth, y);
    ctx.stroke();
  }

  // Tick counter
  ctx.fillStyle = "#f0e2c0";
  ctx.font = "16px monospace";
  ctx.fillText(`Tick: ${gameState.tickId}`, 10, 20);

  // Draw entities as squares on their tile
  for (const entity of gameState.entities.values()) {
    const x = entity.position.x * tileWidth;
    const y = entity.position.y * tileHeight;

    ctx.fillStyle = "#c88a2b";
    ctx.fillRect(x, y, tileWidth, tileHeight);
  }
}
