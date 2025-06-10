"use client"
import { createContext, useContext, useEffect } from "react";

const AppContext = createContext();

export function AppProvider({ children }) {



    useEffect(() => {
        setTheme();
    }, [])


    function setTheme(selectedTheme = null) {
        const root = document.documentElement;
        root.classList.remove("light-mode", "dark-mode");
        let storedTheme = localStorage.getItem("theme");

        if (!selectedTheme) {

            if (storedTheme) {
                root.classList.add(storedTheme);

                //apply the default theme
            } else {
                root.classList.add("light-mode")
                localStorage.setItem("theme", "light-mode")
            }
            return;
        }

        localStorage.setItem("theme", selectedTheme);
        root.classList.add(selectedTheme);
    }



    return (
        <AppContext.Provider value={{ setTheme }}>
            {children}
        </AppContext.Provider>
    );
}

export function useAppContext() {
    return useContext(AppContext);
}