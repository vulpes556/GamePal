import NextAuth from "next-auth"
import GitHub from "next-auth/providers/github"
import Credentials from "next-auth/providers/credentials"

const baseUrl = process.env.BACKEND_URL;
// `${baseUrl}/user/login`
const providers = [
  Credentials({
    credentials: {
      email: { label: "email", type: "text" },
      password: { label: "password", type: "password" },
    },
    async authorize(credentials, req) {
      console.log("base url%%%%%%%%%%%%%:", baseUrl)
      try {
        const res = await fetch('http://localhost:5281/user/login', {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            EmailOrUsername: credentials.email,
            Password: credentials.password,
          }),
        });

        if (!res.ok) {
          console.error("Login failed:", res.status);
          return null;
        }

        const user = await res.json();

        if (user && user.id) {
          return user;
        }

        return null;
      } catch (err) {
        console.error("Authorize error:", err);
        return null;
      }
    },
  }),
  GitHub,
]

export const providerMap = providers
  .map((provider) => {
    if (typeof provider === "function") {
      const providerData = provider()
      return { id: providerData.id, name: providerData.name }
    } else {
      return { id: provider.id, name: provider.name }
    }
  })
  .filter((provider) => provider.id !== "credentials")

export const { handlers, auth, signIn, signOut } = NextAuth({
  providers,
  pages: {
    signIn: "/login",
  },
})