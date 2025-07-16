import { fetchGames } from "@/scripts/scripts";
import AddGameClient from "@/components/AddGame/AddGameClient.jsx";

export default async function AddGameServer({ searchParams }) {
  const page = parseInt(searchParams?.page || "1");
  const pageSize = 16;
  const name = searchParams?.name || null;
  const genre = searchParams?.genre || null;
  const platform = searchParams?.platform || null;


  let games = [];
  try {
    games = await fetchGames(page, pageSize, genre, name, platform);
    console.log("games", games)
  } catch (e) {
    console.error("Failed to fetch games:", e);
  }

  return <AddGameClient searchParams={searchParams} initialGames={games} currentPage={page} />;
}
