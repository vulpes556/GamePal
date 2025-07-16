import React, { useState } from 'react';
import { FiSearch } from "react-icons/fi";
import { FiChevronDown, FiChevronUp } from "react-icons/fi";

const Filters = () => {
    const [selectedPlatforms, setSelectedPlatforms] = useState([]);
    const [selectedGenres, setSelectedGenres] = useState([]);
    const [showPlatforms, setShowPlatforms] = useState(false);
    const [showGenres, setShowGenres] = useState(false);



    // fetch these from the backend
    const platforms = ['PC', 'PS 4', 'PS 5', 'Xbox Series X'];
    const genres = ["Survival", "FPS", "Sandbox", "Horror"]

    const handlePlatformCheckboxChange = (platform) => {
        setSelectedPlatforms((prev) =>
            prev.includes(platform)
                ? prev.filter((item) => item !== platform)
                : [...prev, platform]
        );
    };

    const handleGenreCheckboxChange = (genre) => {
        setSelectedGenres((prev) =>
            prev.includes(genre)
                ? prev.filter((item) => item !== genre)
                : [...prev, genre]
        );
    };

    return (
        <div className="filters-container">
            <div className="filters">
                <div className="search-bar">
                    <input type="text" />
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
                                    onChange={() => handlePlatformCheckboxChange(platform)}
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
                                    onChange={() => handleGenreCheckboxChange(genre)}
                                />
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );

};

export default Filters;
