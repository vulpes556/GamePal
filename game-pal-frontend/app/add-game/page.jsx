import { fetchGames } from "@/scripts/scripts";
import { auth } from "@/auth.js";
import GameCard from "@/components/GameCard/GameCard";

export default async function AddGame({ searchParams }) {
    const session = await auth();

    const page = parseInt(searchParams?.page || "1");
    const pageSize = 10;
    let games = [];

    try {
        games = await fetchGames(session, page, pageSize);
    } catch (e) {
        console.error("Failed to fetch games:", e);
    }

    return (
        <div className="add-game-main">
            <div className="game-cards-paginated">
                {games.length > 0 ? (
                    games.map((g) => { console.log(g); return <GameCard key={g.gameId} game={g} /> })
                ) : (
                    <div>No games found or failed to load games.</div>
                )}
            </div>

            <div className="pagination-controls">
                {page > 1 && (
                    <a href={`/add-game?page=${page - 1}`}>Previous</a>
                )}
                <a href={`/add-game?page=${page + 1}`}>Next</a>
            </div>
        </div>
    );
}
