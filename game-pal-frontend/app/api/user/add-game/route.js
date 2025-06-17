import { NextResponse } from "next/server";
import { getToken } from "next-auth/jwt";

const BACKEND_URL     = process.env.BACKEND_URL;
const NEXTAUTH_SECRET = process.env.NEXTAUTH_SECRET;

export async function POST(request) {
  const token = await getToken({ req: request, secret: NEXTAUTH_SECRET });
  if (!token?.accessToken) {
    return NextResponse.json({ error: "Not authenticated" }, { status: 401 });
  }

  const body = await request.json();
  const apiRes = await fetch(`${BACKEND_URL}/user/add-game`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token.accessToken}`,
    },
    body: JSON.stringify(body),
  });

  const data = await apiRes.json();
  return NextResponse.json(data, { status: apiRes.status });
}