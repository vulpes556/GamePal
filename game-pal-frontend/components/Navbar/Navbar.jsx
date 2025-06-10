import ThemeToggleBtn from "../ThemeToggler/ThemeTogglerBtn";
import { SiYoutubegaming } from "react-icons/si";
import Link from "next/link";
import LoggedInStatus from "../LoggedInStatus/LoggedInStatus";
import LogoutButton from "../LogoutButton/LogoutButton";
import { auth } from "@/auth.js"


export default async function Navbar() {
    const session = await auth()

    return (
        <div className="navbar">
            <div className="logo-name">
                <Link href={"/"}>
                    <h1> <SiYoutubegaming /> GamePal</h1>
                </Link>
            </div>
            <LoggedInStatus />
            <div className="navbar-right">
                {
                    !session?.user ?
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
                        </div> :
                        <LogoutButton />
                }

                <ThemeToggleBtn />
            </div>
        </div>
    )
}