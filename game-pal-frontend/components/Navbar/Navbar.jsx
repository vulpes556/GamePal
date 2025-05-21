import ThemeToggleBtn from "../ThemeToggler/ThemeTogglerBtn";
import { SiYoutubegaming } from "react-icons/si";

export default function Navbar() {
    return (
        <div className="navbar">
            <div className="logo-name">
                <h1> <SiYoutubegaming /> GamePal</h1>
            </div>
            <div className="navbar-right">
                <div className="login-sign-up">
                    <button className="primary-button">
                        Login
                    </button>
                    <button className="primary-button">
                        Sign Up
                    </button>
                </div>
                <ThemeToggleBtn />
            </div>
        </div>
    )
}