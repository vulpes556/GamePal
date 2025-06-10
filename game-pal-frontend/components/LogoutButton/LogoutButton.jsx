"use client";

import { signOut, useSession } from "next-auth/react";
import { useRouter } from "next/navigation";

export default function LogoutButton() {
  const router = useRouter();



  const handleLogout = async () => {
    await signOut({redirectTo:"/"});
    router.replace("/");
  };

  return (
    <button onClick={handleLogout} className="primary-button">
      Sign out
    </button>
  );
}
