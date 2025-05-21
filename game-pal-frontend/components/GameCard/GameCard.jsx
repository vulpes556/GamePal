import Image from "next/image"

export default function GameCard() {
    return (
        <div className="game-card-main">
            <div className="img-container">
                <Image fill alt="Picture of the game" src={"/gameImage.png"} />
            </div>
            <h3>Game Name</h3>
        </div>
    )
}