"use client"
import { useEffect, useState } from "react";
import { MdDarkMode, MdOutlineLightMode } from "react-icons/md";
import { IconContext } from "react-icons";




export default function ThemeTogglerBtn() {
    const [isDark, setIsDark] = useState(false);

    useEffect(() => {
        const root = document.documentElement;
        if (isDark) {
            root.classList.add("dark-mode");
        } else {
            root.classList.remove("dark-mode");
        }
    }, [isDark]);

    return (
        <button className="theme-toggler-button" onClick={() => setIsDark(!isDark)}>
            {isDark ? <IconContext.Provider value={{ color: "white" }} >
                <MdOutlineLightMode className="icon" />
            </IconContext.Provider> : <MdDarkMode className="icon" />
            }
        </button>
    );
}
