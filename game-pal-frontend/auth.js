import NextAuth from "next-auth";
import GitHub from "next-auth/providers/github";
import Credentials from "next-auth/providers/credentials";
import Google from "next-auth/providers/google";

const baseUrl = process.env.BACKEND_URL;
const SESSION_LIFETIME = 60 * 60; // 1 hour
const TOKEN_RENEWAL_INTERVAL = 5 * 60; // 5 minutes

const providers = [
  Credentials({
    credentials: {
      email: { label: "email", type: "text" },
      password: { label: "password", type: "password" },
    },
    async authorize(credentials) {
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

        const body = await res.json();

        if (body && body.id && body.token) {
          return {
            id: body.id,
            email: body.email,
            token: body.token,
          };
        }

        return null;
      } catch (err) {
        console.error("Authorize error:", err);
        return null;
      }
    },
  }),

  GitHub({
    clientId: process.env.GITHUB_ID,
    clientSecret: process.env.GITHUB_SECRET,
  }),

  Google({
    clientId: process.env.GOOGLE_ID,
    clientSecret: process.env.GOOGLE_SECRET,
  }),
];

export const providerMap = providers
  .map((provider) => {
    if (typeof provider === "function") {
      const p = provider();
      return { id: p.id, name: p.name };
    } else {
      return { id: provider.id, name: provider.name };
    }
  })
  .filter((p) => p.id !== "credentials");


export const { handlers, auth, signIn, signOut } = NextAuth({
  providers,

  session: {
    strategy: "jwt",
    maxAge: SESSION_LIFETIME,
    updateAge: TOKEN_RENEWAL_INTERVAL
  },

  secret: process.env.NEXTAUTH_SECRET,

  pages: {
    signIn: "/login",
  },

  callbacks: {

    async signIn({ user, account, profile }) {
      if (account.provider === "credentials") {
        return true;
      }

      try {
        // const idToken = account.id_token || account.access_token;
        const res = await fetch(`${baseUrl}/user/upsert`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            providerName: account.provider,
            providerAccountId: account.providerAccountId,
            email: user.email,
            name: user.name || null,
            image: user.image || null,
          }),
        });

        if (!res.ok) {
          console.error("Upsert failed:", await res.text());
          return false;
        }
        const body = await res.json();
        if (body.token) {
          user.token = body.token;
          return true;
        }
        if (!body.success) {
          console.error("Upsert returned success=false:", body);
          return false;
        }
        return true;
      } catch (err) {
        console.error("Error calling upsert endpoint:", err);
        return false;
      }
    },

    async jwt({ token, user, account }) {
      if (user) {
        token.id = user.id;
        token.email = user.email;
        token.accessToken = user.token;
      }
      return token;
    },


    async session({ session, token }) {
      session.accessToken = token.accessToken;
      session.user.id = token.id;
      session.user.email = token.email;
      return session;
    },
  },
});
