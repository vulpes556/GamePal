"use client";
import { CgProfile } from "react-icons/cg";
import { FaSearch } from "react-icons/fa";
import { PiGameControllerBold } from "react-icons/pi";

export default function HowItWorks() {
    return (
        <div className="how-it-works-main">
            <h3>How It Works</h3>
            <div className="how-it-works-sub">
                <div className="how-it-works-card" >
                    <div className="icon-container">
                        <CgProfile />
                    </div>
                    <h4>1. Create a profile</h4>
                    <p>Set up your gaming profile to get started.</p>
                </div>
                <div className="how-it-works-card">
                    <div className="icon-container">
                        <FaSearch />
                    </div>
                    <h4>2. Find players</h4>
                    <p>Search for other gamers by game or a variety of other filters.</p>
                </div>
                <div className="how-it-works-card">
                    <div className="icon-container">
                        <PiGameControllerBold />
                    </div>
                    <h4>3. Start playing</h4>
                    <p>Connect with your new buddies, and jump into a game.</p>
                </div>
            </div>
        </div>
    )
}