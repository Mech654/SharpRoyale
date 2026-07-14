import { useEffect, useState } from "react";
import GameWindow from "./gameWindow";

function Page() {
  const [registerResult, setRegisterResult] = useState<string>("");
  const [matchId, setMatchId] = useState<number | null>(null);

  useEffect(() => {
    (async () => {
      await RegisterUser(setRegisterResult);
    })();
  }, []);

  return (
    <>
      <header>{registerResult}</header>
      <div className="page">
        <GameWindow matchId={matchId} setMatchId={setMatchId} />
      </div>
    </>
  );
}

async function RegisterUser(
  setRegisterResult: React.Dispatch<React.SetStateAction<string>>,
) {
  const generateUsername = () => {
    const options = [
      "player",
      "ranger",
      "rocket",
      "shadow",
      "vortex",
      "blaze",
      "pixel",
      "comet",
      "nova",
      "storm",
      "ember",
      "cinder",
      "atlas",
      "onyx",
    ];
    return options[Math.floor(Math.random() * options.length)];
  };

  const username = generateUsername();
  const password = `${username}123`;

  try {
    const response = await fetch("http://localhost:5182/api/auth/register", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password }),
    });

    if (response.ok) {
      setRegisterResult(username);
    } else if (!response.ok) {
      const body = await response.text();
      const message = body ? body : response.statusText;
      setRegisterResult(`Error ${response.status}: ${message}`);
    }
  } catch (error) {
    setRegisterResult(`Network error: ${error}`);
  }
}

export default Page;
