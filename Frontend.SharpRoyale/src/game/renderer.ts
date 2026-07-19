import { gameState } from "./gameState";
import { ENTITY_DATA } from "./EntityData";

const TILE_COLS = 18;
const TILE_ROWS = 32;

export function renderFrame(ctx: CanvasRenderingContext2D) {
  const canvas = ctx.canvas;
  const canvasWidth = canvas.clientWidth; // logical/CSS pixels
  const canvasHeight = canvas.clientHeight;

  const tileWidth = canvasWidth / TILE_COLS;
  const tileHeight = canvasHeight / TILE_ROWS;

  ctx.clearRect(0, 0, canvasWidth, canvasHeight);

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

  ctx.fillStyle = "#f0e2c0";
  ctx.font = "16px monospace";
  ctx.fillText(`Tick: ${gameState.tickId}`, 10, 20);

  for (const entity of gameState.entities.values()) {
    const size = ENTITY_DATA[entity.entityId].size;
    const [sizeW, sizeH] = size;

    const centerX = entity.position.x * tileWidth;
    const centerY = entity.position.y * tileHeight;

    const x = centerX - (sizeW * tileWidth) / 2;
    const y = centerY - (sizeH * tileHeight) / 2;

    ctx.fillStyle = "#c88a2b";
    ctx.fillRect(x, y, tileWidth * size[0], tileHeight * size[1]);
  }
}

export function setupCanvasResolution(canvas: HTMLCanvasElement) {
  const dpr = window.devicePixelRatio || 1;
  const rect = canvas.getBoundingClientRect(); // CSS size, e.g. 360x640

  canvas.width = Math.round(rect.width * dpr);
  canvas.height = Math.round(rect.height * dpr);

  const ctx = canvas.getContext("2d")!;
  ctx.setTransform(1, 0, 0, 1, 0, 0); // reset before scaling again on resize
  ctx.scale(dpr, dpr);

  return ctx;
}
