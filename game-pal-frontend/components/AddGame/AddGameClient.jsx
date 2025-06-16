"use client";

import { useState } from "react";
import Link from "next/link";
import GameCard from "@/components/GameCard/GameCard";
import Modal from "@/components/Modal/Modal";
import Image from "next/image";

export default function AddGameClient({ initialGames, currentPage }) {
  const [games] = useState(initialGames);
  const [selectedGame, setSelectedGame] = useState(null);

  function openModal(game) {
    setSelectedGame(game);
  }

  console.log("selected game:", selectedGame)

  function closeModal() {
    setSelectedGame(null);
  }

  return (
    <div className="add-game-main">
      <div className="game-cards-paginated">
        {games.length > 0 ? (
          games.map((g) => (
            <div key={g.gameId} onClick={() => openModal(g)}>
              <GameCard game={g} />
            </div>
          ))
        ) : (
          <div>No games found or failed to load games.</div>
        )}
      </div>

      <div className="pagination-controls">
        {currentPage > 1 && (
          <Link href={`/add-game?page=${currentPage - 1}`}>Previous</Link>
        )}
        <Link href={`/add-game?page=${currentPage + 1}`}>Next</Link>
      </div>

      <Modal isOpen={!!selectedGame} onClose={closeModal}>
        {selectedGame && (
          <>
            <div className="img-container" >
              <Image fill alt="Picture of the game" src={selectedGame?.pictureUrl || "/gameImage.png"} />
            </div>
            <h2>{selectedGame.name}</h2>
            <p>{selectedGame.categories.join(", ")}</p>
          </>
        )}
      </Modal>
    </div>
  );
}
