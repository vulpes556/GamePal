import ThemeToggleBtn from "../ThemeToggler/ThemeTogglerBtn";
import { SiYoutubegaming } from "react-icons/si";
import Link from "next/link";

export default function Navbar() {
    return (
        <div className="navbar">
            <div className="logo-name">
                <h1> <SiYoutubegaming /> GamePal</h1>
            </div>
            <div className="navbar-right">
                <div className="login-sign-up">
                    <Link href={"/login"}>
                        <button className="primary-button">
                            Login
                        </button>
                    </Link>
                    <button className="primary-button">
                        Sign Up
                    </button>
                </div>
                <ThemeToggleBtn />
            </div>
        </div>
    )
}