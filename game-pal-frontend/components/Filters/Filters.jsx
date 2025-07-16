import { getPlatforms, getGameCategories } from '@/scripts/scripts';
import React, { useState, useEffect } from 'react';
import { FiSearch, FiChevronDown, FiChevronUp } from "react-icons/fi";

function Filters({

    // platforms = ['PC', 'PS 4', 'PS 5', 'Xbox Series X'],
    // genres = ["Survival", "FPS", "Sandbox", "Horror"],
    selectedPlatforms = [],
    selectedGenres = [],
    searchTerm = '',
    onSearchTermChange,
    onPlatformChange,
    onGenreChange,
}) {
    const [showPlatforms, setShowPlatforms] = useState(false);
    const [showGenres, setShowGenres] = useState(false);
    const [platforms, setPlatforms] = useState();
    const [genres, setGenres] = useState();

    useEffect(() => {
        async function fetchData() {
            try {
                const [cats, plats] = await Promise.all([
                    getGameCategories(),
                    getPlatforms(),
                ]);
                setPlatforms(plats);
                setGenres(cats);
            } catch (error) {
                console.error("Failed to fetch categories or platforms:", error);
            }
        }

        fetchData();
    }, []);



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
                        {platforms?.map((platform) => (
                            <div className="filter-item" key={platform.id}>
                                <label htmlFor={`platform-${platform.id}`}>{platform.name}</label>
                                <input
                                    type="checkbox"
                                    id={`platform-${platform.id}`}
                                    checked={selectedPlatforms.includes(platform.name)}
                                    onChange={() => onPlatformChange(platform.name)}
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
                        {genres?.map((genre) => (
                            <div className="filter-item" key={genre.id}>
                                <label htmlFor={`genre-${genre.id}`}>{genre.name}</label>
                                <input
                                    type="checkbox"
                                    id={`genre-${genre.id}`}
                                    checked={selectedGenres.includes(genre.name)}
                                    onChange={() => onGenreChange(genre.name)}
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
