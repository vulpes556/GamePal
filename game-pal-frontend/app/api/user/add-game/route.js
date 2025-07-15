import { NextResponse } from "next/server";
import { getToken } from "next-auth/jwt";

const BACKEND_URL = process.env.BACKEND_URL;
const NEXTAUTH_SECRET = process.env.AUTH_SECRET;

export async function POST(request) {
  const token = await getToken({ req: request, secret: NEXTAUTH_SECRET });

  if (!token?.accessToken) {
    return NextResponse.json({ error: "Not authenticated" }, { status: 401 });
  }

  const body = await request.json();

  try {
    const apiRes = await fetch(`${BACKEND_URL}/user/add-game`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token.accessToken}`,
      },
      body: JSON.stringify(body),
    });

    const responseText = await apiRes.text();

    let parsed;
    try {
      parsed = JSON.parse(responseText);
    } catch {
      parsed = { error: responseText };
    }

    return NextResponse.json(parsed, { status: apiRes.status });
  } catch (error) {
    return NextResponse.json(
      { error: "Failed to connect to backend." },
      { status: 502 }
    );
  }
}
