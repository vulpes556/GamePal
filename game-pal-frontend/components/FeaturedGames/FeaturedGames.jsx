import GameCard from "../GameCard/GameCard";

export default function FeaturedGames() {
    return (
        <div className="featured-games-main">
            <h3>Featured Games</h3>
            <div className="game-cards">
                <GameCard />
                <GameCard />
                <GameCard />
                <GameCard />
            </div>
        </div>
    )
}