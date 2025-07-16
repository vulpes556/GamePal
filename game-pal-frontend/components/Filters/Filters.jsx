import React, { useState } from 'react';
import { FiSearch, FiChevronDown, FiChevronUp } from "react-icons/fi";

function Filters({
    platforms = ['PC', 'PS 4', 'PS 5', 'Xbox Series X'],
    genres = ["Survival", "FPS", "Sandbox", "Horror"],
    selectedPlatforms = [],
    selectedGenres = [],
    searchTerm = '',
    onSearchTermChange,
    onPlatformChange,
    onGenreChange,
}) {
    const [showPlatforms, setShowPlatforms] = useState(false);
    const [showGenres, setShowGenres] = useState(false);

    return (
        <div className="filters-container">
            <div className="filters">
                <div className="search-bar">
                    <input
                        type="text"
                        value={searchTerm}
                        onChange={(e) => onSearchTermChange(e.target.value)}
                        placeholder="Search games..."
                    />
                    <FiSearch className="search-icon" />
                </div>

                {/* Platforms */}
                <div className="filter-section">
                    <div className="filter-header" onClick={() => setShowPlatforms(!showPlatforms)}>
                        <h4>Platforms</h4>
                        {showPlatforms ? <FiChevronUp /> : <FiChevronDown />}
                    </div>
                    <div className={`filter-content ${showPlatforms ? 'open' : 'closed'}`}>
                        {platforms.map((platform) => (
                            <div className="filter-item" key={platform}>
                                <label htmlFor={`platform-${platform}`}>{platform}</label>
                                <input
                                    type="checkbox"
                                    id={`platform-${platform}`}
                                    checked={selectedPlatforms.includes(platform)}
                                    onChange={() => onPlatformChange(platform)}
                                />
                            </div>
                        ))}
                    </div>
                </div>

                {/* Genres */}
                <div className="filter-section">
                    <div className="filter-header" onClick={() => setShowGenres(!showGenres)}>
                        <h4>Category</h4>
                        {showGenres ? <FiChevronUp /> : <FiChevronDown />}
                    </div>
                    <div className={`filter-content ${showGenres ? 'open' : 'closed'}`}>
                        {genres.map((genre) => (
                            <div className="filter-item" key={genre}>
                                <label htmlFor={`genre-${genre}`}>{genre}</label>
                                <input
                                    type="checkbox"
                                    id={`genre-${genre}`}
                                    checked={selectedGenres.includes(genre)}
                                    onChange={() => onGenreChange(genre)}
                                />
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Filters;
