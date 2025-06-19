import { fetchGames } from "@/scripts/scripts";
import AddGameClient from "@/components/AddGame/AddGameClient.jsx";

export default async function AddGameServer({ searchParams }) {
  const page = parseInt(searchParams?.page || "1");
  const pageSize = 8;

  let games = [];
  try {
    games = await fetchGames(page, pageSize);
  } catch (e) {
    console.error("Failed to fetch games:", e);
  }

  return <AddGameClient initialGames={games} currentPage={page} />;
}
