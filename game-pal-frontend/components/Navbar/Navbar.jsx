import ThemeToggleBtn from "../ThemeToggler/ThemeTogglerBtn";
import { SiYoutubegaming } from "react-icons/si";

export default function Navbar() {
    return (
        <div className="navbar">
            <div className="logo-name">
            <h1> <SiYoutubegaming /> GAMEPAL</h1>
            </div>
            <div className="theme-toggler">
            <ThemeToggleBtn/>
            </div>
        </div>
    )
}