"use client"
import { useEffect, useState } from "react";


// TODO: store light/dark mode preference so doesnt get lost upon page refresh

export default function ThemeToggleBtn() {
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
    <button onClick={() => setIsDark(!isDark)}>
      {isDark ? "🌙 Dark" : "☀️ Light"}
    </button>
  );
}
