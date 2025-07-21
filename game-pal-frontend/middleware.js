import { NextResponse } from "next/server";
import { getToken } from "next-auth/jwt";

const PROTECTED_PATHS = [
    "/add-game",
];

export async function middleware(request) {
    const token = await getToken({
        req: request,
        secret: process.env.AUTH_SECRET,
    });

    const { pathname } = request.nextUrl;

    if (PROTECTED_PATHS.some(path => pathname.startsWith(path))) {
        if (!token) {
            const loginUrl = new URL("/login", request.nextUrl.origin);

            // use it later
            loginUrl.searchParams.set("callbackUrl", pathname);
            return NextResponse.redirect(loginUrl);
        }
    }

    return NextResponse.next();
}

export const config = {
    matcher: [
        "/add-game/:path*",
    ],
};
