import { fetchGames } from "@/scripts/scripts"
import { auth } from "@/auth.js"

export default async function AddGame() {
    const session = await auth()
    const games = await fetchGames(session);

    return (
        <div className="add-game-main">
            {
                games.map(g => <div key={g.gameId} >{g.name}</div>)
            }
        </div>
    )
}