"use client"
import { useEffect, useState } from "react";
import { MdDarkMode, MdOutlineLightMode } from "react-icons/md";
import { IconContext } from "react-icons";
import { useAppContext } from "@/context/AppContext";




export default function ThemeTogglerBtn() {
    const { setTheme } = useAppContext();
    const [isDark, setIsDark] = useState(null);


    useEffect(() => {
        const defaultDarkState = localStorage.getItem("theme") === "dark-mode";
        setIsDark(defaultDarkState);
    }, [])

    function themeSwitcher() {
        setIsDark(!isDark);

        if (isDark) {
            setTheme("light-mode")
        } else {
            setTheme("dark-mode")
        }
    }

    return (
        <button className="theme-toggler-button" onClick={themeSwitcher}>
            {isDark ? <IconContext.Provider value={{ color: "white" }} >
                <MdOutlineLightMode className="icon" />
            </IconContext.Provider> : <MdDarkMode className="icon" />
            }
        </button>
    );
}
