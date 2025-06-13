import Image from "next/image"

export default function GameCard({ game }) {
    return (
        <div className="game-card-main">
            <div className="img-container">
                <Image fill alt="Picture of the game" src={game?.pictureUrl || "/gameImage.png"} />
            </div>
            <h3>{game?.name || "Game Name"}</h3>
        </div>
    )
}