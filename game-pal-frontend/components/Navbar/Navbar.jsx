import ThemeToggleBtn from "../ThemeToggler/ThemeTogglerBtn";
import { SiYoutubegaming } from "react-icons/si";
import Link from "next/link";
import { signOut } from "@/auth.js"

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
                            Log In
                        </button>
                    </Link>
                    <Link href={"/signup"}>
                        <button className="primary-button">
                            Sign Up
                        </button>
                    </Link>
                </div>
                <form
                    action={async () => {
                        "use server"
                        await signOut()
                    }}
                >
                    <button className="primary-button" type="submit">Sign Out</button>
                </form>
                <ThemeToggleBtn />
            </div>
        </div>
    )
}