import { gameState } from "./gameState";

export function renderFrame(ctx: CanvasRenderingContext2D) {
  ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);

  ctx.fillStyle = "#f0e2c0";
  ctx.font = "16px monospace";
  ctx.fillText(`Tick: ${gameState.tickId}`, 10, 20);

  let i = 0;
  for (const entity of gameState.entities.values()) {
    ctx.fillStyle = "#c88a2b";
    ctx.beginPath();
    ctx.arc(40 + i * 30, 60, 8, 0, Math.PI * 2);
    ctx.fill();
    i++;
  }
}
