"use client";

import { useEffect, useState, useRef } from "react";
import Link from "next/link";
import GameCard from "@/components/GameCard/GameCard";
import Modal from "@/components/Modal/Modal";
import Image from "next/image";
import { addGameToUserLibrary } from "@/scripts/scripts";
import Filters from "../Filters/Filters";
import { useRouter } from 'next/navigation';

export default function AddGameClient({ searchParams = {}, initialGames, currentPage }) {
  const [games, setGames] = useState(initialGames.items || []);
  const [selectedGame, setSelectedGame] = useState(null);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const router = useRouter();
  const [searchTerm, setSearchTerm] = useState('');


  const [filters, setFilters] = useState({
    selectedPlatforms: searchParams.platform?.split(",") || [],
    selectedGenres: searchParams.genre?.split(",") || [],
    searchTerm: searchParams.name || "",
  });


  const handlePlatformChange = (platform) => {
    setFilters((prev) => {
      const selectedPlatforms = prev.selectedPlatforms.includes(platform)
        ? prev.selectedPlatforms.filter(p => p !== platform)
        : [...prev.selectedPlatforms, platform];
      return { ...prev, selectedPlatforms };
    });
  };

  const handleGenreChange = (genre) => {
    setFilters((prev) => {
      const selectedGenres = prev.selectedGenres.includes(genre)
        ? prev.selectedGenres.filter(g => g !== genre)
        : [...prev.selectedGenres, genre];
      return { ...prev, selectedGenres };
    });
  };


  const handleSearchTermChange = (term) => {
    setSearchTerm(term);
    setFilters(prev => ({ ...prev, searchTerm: term }));
  };

  useEffect(() => {
    setGames(initialGames.items || []);
  }, [initialGames]);


  useEffect(() => {
    const debounceTimer = setTimeout(() => {
      const params = new URLSearchParams();
      params.set("page", "1");
      if (filters.searchTerm) params.set("name", filters.searchTerm);
      if (filters.selectedPlatforms.length > 0)
        params.set("platform", filters.selectedPlatforms.join(","));
      if (filters.selectedGenres.length > 0)
        params.set("genre", filters.selectedGenres.join(","));

      router.push(`/add-game?${params.toString()}`);
    }, 500); // 500ms delay

    return () => clearTimeout(debounceTimer); // cancel on new change
  }, [filters]);


  function openModal(game) {
    setSelectedGame(game);
  }

  function closeModal() {
    setIsDropdownOpen(false);
    setSelectedGame(null);
  }

  async function handlePlatformSelect(gameId, platformId) {
    try {
      await addGameToUserLibrary({ gameId, platformId });
      // maybe show confirmation here (later)
    } catch (e) {
      console.error(e);
    }
  }

  return (
    <div className="add-game-main">
      <Filters
        selectedPlatforms={filters.selectedPlatforms}
        selectedGenres={filters.selectedGenres}
        searchTerm={filters.searchTerm}
        onSearchTermChange={handleSearchTermChange}
        onPlatformChange={handlePlatformChange}
        onGenreChange={handleGenreChange}
      />

      <div className="cards-navigation">
        <div className="game-cards-paginated">
          {games.length > 0 ? (
            games.map((g) => (
              <div key={g.gameId} onClick={() => openModal(g)}>
                <GameCard game={g} />
              </div>
            ))
          ) : (
            <h1>No games found or failed to load games.</h1>
          )}
        </div>

        <div className="pagination-controls">
          {currentPage === 1 ? (
            <span className="primary-button disabled">Previous</span>
          ) : (
            <Link className="primary-button" href={`/add-game?page=${currentPage - 1}`}>
              Previous
            </Link>
          )}
          {currentPage * 32 >= initialGames.totalCount ? (
            <span className="primary-button disabled">Next</span>
          ) : (
            <Link className="primary-button" href={`/add-game?page=${currentPage + 1}`}>
              Next
            </Link>
          )}

        </div>
      </div>

      <Modal isOpen={!!selectedGame} onClose={closeModal}>
        {selectedGame && (
          <>
            <div className="img-container">
              <Image
                fill
                alt="Picture of the game"
                src={selectedGame?.pictureUrl || "/gameImage.png"}
              />
            </div>
            <h2>{selectedGame.name}</h2>
            <p>Categories: [{selectedGame.categories.map(c => c.name).join(", ")}]</p>
            <div className="dropdown-wrapper">
              <button
                onClick={() => setIsDropdownOpen((prev) => !prev)}
                className="primary-button"
              >
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
