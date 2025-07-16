"use client";

import { useEffect, useState } from "react";
import Link from "next/link";
import GameCard from "@/components/GameCard/GameCard";
import Modal from "@/components/Modal/Modal";
import Image from "next/image";
import { addGameToUserLibrary } from "@/scripts/scripts";
import Filters from "../Filters/Filters";

export default function AddGameClient({ initialGames, currentPage }) {
  const [games, setGames] = useState(initialGames.items);
  const [selectedGame, setSelectedGame] = useState(null);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);


  useEffect(() => {
    setGames(initialGames.items);
  }, [initialGames]);

  function openModal(game) {
    setSelectedGame(game);
  }

  console.log("selected game:", selectedGame)

  function closeModal() {
    setIsDropdownOpen(false);
    setSelectedGame(null);
  }

  async function handlePlatformSelect(gameId, platformId) {
    try {
      await addGameToUserLibrary({ gameId, platformId })
    } catch (e) {
      console.log(e);
    }
  }

  return (
    <div className="add-game-main">
      <Filters />
      <div className="cards-navigation">
        <div className="game-cards-paginated">
          {games?.length > 0 ? (
            games?.map((g) => (
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
            <Link className="primary-button" href={`/add-game?page=${currentPage - 1}`}>Previous</Link>
          )}
          {
            currentPage * 8 < initialGames.totalCount && (
              <Link className="primary-button" href={`/add-game?page=${currentPage + 1}`}>Next</Link>
            )
          }
        </div>
      </div>
      <Modal isOpen={!!selectedGame} onClose={closeModal}>
        {selectedGame && (
          <>
            <div className="img-container" >
              <Image fill alt="Picture of the game" src={selectedGame?.pictureUrl || "/gameImage.png"} />
            </div>
            <h2>{selectedGame.name}</h2>
            <p>Categories: [{selectedGame.categories.map(c => c.name).join(", ")}]</p>
            <div className="dropdown-wrapper">
              <button onClick={() => {
                setIsDropdownOpen(prev => !prev);
              }} className="primary-button">
                Add game
              </button>

              {isDropdownOpen && (
                <ul className="dropdown">
                  {selectedGame.platforms.map((platform) => (
                    <li
                      key={platform.id}
                      onClick={() => handlePlatformSelect(selectedGame.gameId, platform.id)}
                      className="dropdown-item"
                    >
                      {platform.name}
                    </li>
                  ))}
                </ul>
              )}
            </div>

          </>
        )}
      </Modal>
    </div>
  );
}
