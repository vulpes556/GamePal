import Navbar from "@/components/Navbar/Navbar";
import "./globals.scss";
import { AppProvider } from "@/context/AppContext";



export const metadata = {
  title: "GamePal",
  description: "Made by Bálint",
};

export default function RootLayout({ children }) {
  return (
    <html suppressHydrationWarning={true} lang="en">
      <head>
        <script
          dangerouslySetInnerHTML={{
            __html: `
              (function() {
                const theme = localStorage.getItem("theme") || "light-mode";
                document.documentElement.classList.add(theme);
              })();
            `,
          }}
        />
      </head>
      <body className="layout">
        <AppProvider>
          <Navbar />
          {children}
        </AppProvider>
      </body>
    </html>
  );
}