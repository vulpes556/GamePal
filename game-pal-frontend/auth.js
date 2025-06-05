import NextAuth from "next-auth"
import GitHub from "next-auth/providers/github"
import Credentials from "next-auth/providers/credentials"
import Google from "next-auth/providers/google"

const baseUrl = process.env.BACKEND_URL;
const providers = [
  Credentials({
    credentials: {
      email: { label: "email", type: "text" },
      password: { label: "password", type: "password" },
    },
    async authorize(credentials, req) {
      try {
        const res = await fetch(`${baseUrl}/user/login`, {
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
  Google,
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
  callbacks: {
    async signIn({ user, account, profile }) {
      const providerName = account.provider;              // "github" or "google"
      const providerAccountId = account.providerAccountId; // e.g. "1234567"
      const email = user.email;                            // could be null from GitHub
      const name = user.name || null;
      const image = user.image || null;

      try {
        const res = await fetch(`${baseUrl}/api/user/upsert`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            provider: providerName,
            providerAccountId,
            email,
            name,
            image,
          }),
        });

        if (!res.ok) {
          console.error("Upsert endpoint returned error:", await res.text());
          return false; // Block sign-in if backend fails
        }

        const body = await res.json();
        if (body.success) {
          return true; // Allow NextAuth to finish sign-in
        } else {
          console.error("Upsert failed on backend:", body);
          return false;
        }
      } catch (err) {
        console.error("Error calling upsert endpoint:", err);
        return false;
      }
    },
  },
});
