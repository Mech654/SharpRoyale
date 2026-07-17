import { useState, useEffect, useRef } from "react";
import { connectToMatch } from "../services/WSconnection";
import { renderFrame } from "../game/renderer";
import { applyMatchEvent } from "../services/gameEvents";

interface GameWindowProps {
  matchId: number | null;
  setMatchId: React.Dispatch<React.SetStateAction<number | null>>;
}

let eventSource: EventSource | null = null;

const GameWindow = ({ matchId, setMatchId }: GameWindowProps) => {
  const [matchStatus, setMatchStatus] = useState("");
  const [isJoining, setIsJoining] = useState(false);
  const canvasRef = useRef<HTMLCanvasElement>(null);

  useEffect(() => {
    const canvas = canvasRef.current;
    const ctx = canvas?.getContext("2d");
    if (!ctx) return;

    let frameId: number;
    const loop = () => {
      renderFrame(ctx);
      frameId = requestAnimationFrame(loop);
    };
    frameId = requestAnimationFrame(loop);

    return () => cancelAnimationFrame(frameId);
  }, []);

  useEffect(() => {
    if (matchId == null) return;
    const cleanup = connectToMatch(matchId, applyMatchEvent);
    return cleanup;
  }, [matchId]);

  return (
    <div className="game-window">
      <canvas ref={canvasRef} />
      {matchId === null && <p>{matchStatus}</p>}
      {matchId === null && (
        <button
          disabled={isJoining}
          className="join-match-button"
          onClick={() =>
            handleJoinMatchClick(setMatchId, setMatchStatus, setIsJoining)
          }
        >
          Join Before Its Too Late!!!
        </button>
      )}
    </div>
  );
};

function handleJoinMatchClick(
  setMatchId: React.Dispatch<React.SetStateAction<number | null>>,
  setMatchStatus: React.Dispatch<React.SetStateAction<string>>,
  setIsJoining: React.Dispatch<React.SetStateAction<boolean>>,
) {
  setIsJoining(true);
  eventSource = new EventSource(`http://localhost:5182/api/lobby/stream/join`, {
    withCredentials: true,
  });
  eventSource.onmessage = (event) => {
    const data = JSON.parse(event.data);
    if (data.matchId) {
      setMatchId(data.matchId);
      setMatchStatus("Found Match!!! joining: " + data.matchId + "...");
      eventSource?.close();
    }
  };
}

export default GameWindow;
