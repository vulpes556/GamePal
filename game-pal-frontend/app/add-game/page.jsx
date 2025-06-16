import { fetchGames } from "@/scripts/scripts";
import { auth } from "@/auth.js";
import AddGameClient from "@/components/AddGame/AddGameClient.jsx";

export default async function AddGameServer({ searchParams }) {
  const session = await auth();
  const page = parseInt(searchParams?.page || "1");
  const pageSize = 10;

  let games = [];
  try {
    games = await fetchGames(session, page, pageSize);
  } catch (e) {
    console.error("Failed to fetch games:", e);
  }

  return <AddGameClient initialGames={games} currentPage={page} />;
}
