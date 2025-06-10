"use client"

import { getSession } from "next-auth/react"

export default function ProtectedButton() {
  const callProtectedEndpoint = async () => {
    const session = await getSession();

    if (!session?.accessToken) {
      alert('You are not logged in or missing access token');
      return;
    }

    try {
      const res = await fetch("/api/protected", {
        method: "GET",
        headers: {
          Authorization: `Bearer ${session.accessToken}`,
        },
      });

      if (res.ok) {
        alert("Access granted ✅");
      } else {
        alert(`Access denied ❌: ${res.status}`);
      }
    } catch (err) {
      console.error("Error calling protected endpoint:", err);
      alert("Network error or server unreachable.");
    }
  };

  return (
    <button onClick={callProtectedEndpoint} className="primary-button" type="button">
      Call Protected Endpoint
    </button>
  );
}
